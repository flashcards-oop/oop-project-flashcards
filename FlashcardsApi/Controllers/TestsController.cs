using System.Collections.Generic;
using Flashcards;
using FlashcardsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : Controller
    {
        private readonly IStorage storage;
        private readonly IAnswersStorage answersStorage;

        public TestsController(IStorage storage, IAnswersStorage answersStorage)
        {
            this.storage = storage;
            this.answersStorage = answersStorage;
        }

        [HttpPost("generate")]
        public ActionResult GenerateTest(TestDto test)
        {
            var collection = storage.FindCollection(test.CollectionId);
            if (collection is null)
            {
                return NotFound();
            }
            
            var builder = new TestBuilder(collection.Cards);
            builder.GenerateTasks(test.OpenCnt, typeof(OpenAnswerQuestion));
            builder.GenerateTasks(test.ChoiceCnt, typeof(ChoiceQuestion));
            builder.GenerateTasks(test.MatchCnt, typeof(MatchingQuestion));

            var exercises = builder.Build();
            var testId = answersStorage.AddAnswers(exercises);

            return Ok(new Dictionary<string, object>{{"testId", testId}, {"exercises", exercises}});
        }
    }
}