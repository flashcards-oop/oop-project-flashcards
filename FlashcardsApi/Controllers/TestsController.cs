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
        private readonly ITestStorage testStorage;
        private readonly Dictionary<string, IExerciseGenerator> generatorsByCaption;

        public TestsController(IStorage storage, 
            ITestStorage testStorage,
            IEnumerable<IExerciseGenerator> generators)
        {
            this.storage = storage;
            this.testStorage = testStorage;

            generatorsByCaption = new Dictionary<string, IExerciseGenerator>();
            foreach (var generator in generators)
                generatorsByCaption[generator.GetTypeCaption()] = generator;
        }

        [Authorize]
        [HttpPost("generate")]
        public async Task<ActionResult<Test>> GenerateTest(TestDto testDto)
        {
            var collection = await storage.FindCollection(testDto.CollectionId);
            if (collection is null)
            {
                return NotFound();
            }

            if (!User.OwnsResource(collection))
                return Forbid();

            var cards = await storage.GetCollectionCards(testDto.CollectionId);

            var testBuilder = new TestBuilder(cards, new RandomCardsSelector());
            foreach(var block in testDto.blocks)
                if (generatorsByCaption.ContainsKey(block.Type))
                    testBuilder = testBuilder.WithGenerator(generatorsByCaption[block.Type], block.Amount);

            var exercises = testBuilder.Build().ToList();
            var test = new Test(exercises, User.Identity.Name);
            await testStorage.AddTest(test);

            return Ok(test);
        }

        [HttpPost("check")]
        public async Task<ActionResult> CheckAnswers(TestAnswersDto answers)
        {
            var test = await testStorage.FindTest(answers.TestId);
            if (test == null)
                return UnprocessableEntity("test with this id does not exist");
            if (!User.OwnsResource(test))
                return Forbid();

            var correctAnswers = test.Exercises.Select(exercise => exercise.Answer);
            var counter = 
                (from answer in correctAnswers 
                let userAnswer = answers.Answers.First(a => a.Id == answer.Id) 
                where answer.IsTheSameAs(userAnswer) 
                select answer).Count();

            return Ok($"You scored {counter}.");
        }
    }
}