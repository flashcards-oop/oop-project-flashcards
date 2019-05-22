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

        public TestsController(IStorage storage, IAnswersStorage answersStorage, IAuthorizationService authorizationService)
        {
            this.storage = storage;
            this.answersStorage = answersStorage;
            this.authorizationService = authorizationService;
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

            var exercises = new TestBuilder(cards, new RandomCardsSelector())
                .WithGenerator(new OpenQuestionExerciseGenerator(), test.OpenCnt)
                .WithGenerator(new MatchingQuestionExerciseGenerator(), test.MatchCnt)
                .WithGenerator(new ChoiceQuestionExerciseGenerator(), test.ChoiceCnt)
                .Build();

            var testId = answersStorage.AddAnswers(exercises);

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