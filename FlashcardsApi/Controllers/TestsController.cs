using System.Collections.Generic;
using System.Linq;
using Flashcards;
using FlashcardsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Threading;
using Flashcards.TestProcessing;

namespace FlashcardsApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : Controller
    {
        private readonly IStorage storage;
        private readonly ITestStorage testStorage;
        private readonly Dictionary<string, IExerciseGenerator> generatorsByCaption;
        private readonly ITestBuilderFactory testBuilderFactory;
        private readonly FilterGenerator filterGenerator;
        
        public TestsController(IStorage storage, 
            ITestStorage testStorage,
            ITestBuilderFactory testBuilderFactory,
            IEnumerable<IExerciseGenerator> generators,
            FilterGenerator filterGenerator)
        {
            this.storage = storage;
            this.testStorage = testStorage;
            this.testBuilderFactory = testBuilderFactory;
            this.filterGenerator = filterGenerator;

            generatorsByCaption = new Dictionary<string, IExerciseGenerator>();
            foreach (var generator in generators)
                generatorsByCaption[generator.GetTypeCaption()] = generator;
        }

        [Authorize]
        [HttpPost("generate")]
        public async Task<ActionResult<TestDto>> GenerateTest(TestQueryDto testQueryDto, CancellationToken token)
        {
            var collection = await storage.FindCollection(testQueryDto.CollectionId, token);
            if (collection is null)
                return NotFound();
            
            if (!User.OwnsResource(collection))
                return Forbid();

            var cards = await storage.GetCollectionCards(testQueryDto.CollectionId, token);
            if (testQueryDto.Filter != null)
            {
                var filter = filterGenerator.GetFilter(testQueryDto.Filter);
                cards = filter(cards).ToList();
            }

            var testBuilder = testBuilderFactory.GetBuilder(cards, new RandomCardsSelector());
            foreach(var block in testQueryDto.Blocks)
                if (generatorsByCaption.ContainsKey(block.Type))
                    testBuilder = testBuilder.WithGenerator(generatorsByCaption[block.Type], block.Amount);

            var exercises = testBuilder.Build().ToList();
            var test = new Test(exercises, User.Identity.Name);
            await testStorage.AddTest(test, token);

            return Ok(new TestDto(test));
        }

        [HttpPost("check")]
        public async Task<ActionResult<TestResultsDto>> CheckAnswers(TestAnswersDto answers, CancellationToken token)
        {
            var test = await testStorage.FindTest(answers.TestId, token);
            if (test == null)
                return UnprocessableEntity("test with this id does not exist");
            if (!User.OwnsResource(test))
                return Forbid();

            var userAnswers = new Dictionary<string, IAnswer>();
            foreach (var answer in answers.Answers)
            {
                if (!userAnswers.ContainsKey(answer.Id))
                {
                    userAnswers.Add(answer.Id, answer.Answer);
                }
            }
            var verdicts = TestChecker.Check(userAnswers, test);
            
            var results = new TestResultsDto();
            foreach (var (key, _) in verdicts)
            {
                var exercise = test.Exercises.First(e => e.Id == key);
                results.Answers.Add(key, new ExerciseVerdictDto(verdicts[key], exercise.Answer));
                await storage.UpdateCardsAwareness(exercise.UsedCardsIds, verdicts[key] ? 3 : -3, token);
            }

            return Ok(results);
        }
    }
}