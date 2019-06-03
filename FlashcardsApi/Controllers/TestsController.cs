using System.Collections.Generic;
using System.Linq;
using Flashcards;
using FlashcardsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Flashcards.TestChecking;

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
        public async Task<ActionResult<TestDto>> GenerateTest(TestQueryDto testQueryDto)
        {
            var collection = await storage.FindCollection(testQueryDto.CollectionId);
            if (collection is null)
            {
                return NotFound();
            }

            if (!User.OwnsResource(collection))
                return Forbid();

            var cards = await storage.GetCollectionCards(testQueryDto.CollectionId);

            var testBuilder = new TestBuilder(cards, new RandomCardsSelector());
            foreach(var block in testQueryDto.Blocks)
                if (generatorsByCaption.ContainsKey(block.Type))
                    testBuilder = testBuilder.WithGenerator(generatorsByCaption[block.Type], block.Amount);

            var exercises = testBuilder.Build().ToList();
            var test = new Test(exercises, User.Identity.Name);
            await testStorage.AddTest(test);

            return Ok(new TestDto(test));
        }

        [HttpPost("check")]
        public async Task<ActionResult<TestResultsDto>> CheckAnswers(TestAnswersDto answers)
        {
            var test = await testStorage.FindTest(answers.TestId);
            if (test == null)
                return UnprocessableEntity("test with this id does not exist");
            if (!User.OwnsResource(test))
                return Forbid();

            var results = TestChecker.Check(answers, test);

            return Ok(results);
        }
    }
}