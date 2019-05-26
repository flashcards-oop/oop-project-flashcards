using System.Collections.Generic;
using System.Linq;
using Flashcards;
using FlashcardsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace FlashcardsApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : Controller
    {
        private readonly IStorage storage;
        private readonly IAnswersStorage answersStorage;
        private readonly IAuthorizationService authorizationService;
        private readonly Dictionary<string, IExerciseGenerator> generatorsByCaption;

        public TestsController(IStorage storage, 
            IAnswersStorage answersStorage,
            IEnumerable<IExerciseGenerator> generators,
            IAuthorizationService authorizationService)
        {
            this.storage = storage;
            this.answersStorage = answersStorage;
            this.authorizationService = authorizationService;

            generatorsByCaption = new Dictionary<string, IExerciseGenerator>();
            foreach (var generator in generators)
                generatorsByCaption[generator.GetTypeCaption()] = generator;
        }

        [Authorize]
        [HttpPost("generate")]
        public async Task<ActionResult<Dictionary<string, object>>> GenerateTest(TestDto test)
        {
            var collection = await storage.FindCollection(test.CollectionId);
            if (collection is null)
            {
                return NotFound();
            }

            var authResult = await authorizationService.AuthorizeAsync(User, collection, Policies.ResourceAccess);
            if (!authResult.Succeeded)
                return Forbid();

            var cards = await storage.GetCollectionCards(test.CollectionId);

            var testBuilder = new TestBuilder(cards, new RandomCardsSelector());
            foreach(var block in test.blocks)
                if (generatorsByCaption.ContainsKey(block.Type))
                    testBuilder = testBuilder.WithGenerator(generatorsByCaption[block.Type], block.Amount);

            var exercises = testBuilder.Build().ToList();
            var testId = await answersStorage.AddAnswers(exercises);

            return Ok(new Dictionary<string, object>{{"testId", testId}, {"exercises", exercises}});
        }

        [HttpPost("check")]
        public async Task<ActionResult> CheckAnswers(TestAnswersDto answers)
        {
            var correctAnswers = await answersStorage.FindAnswers(answers.TestId);
            var counter = 
                (from answer in correctAnswers 
                let userAnswer = answers.Answers.First(a => a.Id == answer.Id) 
                where answer.IsTheSameAs(userAnswer) 
                select answer).Count();

            return Ok($"You scored {counter}.");
        }
    }
}