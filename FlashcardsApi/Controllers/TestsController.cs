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
            var collection = storage.FindCollection(test.CollectionId);
            if (collection is null)
            {
                return NotFound();
            }

            var authResult = await authorizationService.AuthorizeAsync(User, collection, Policies.ResourceAccess);
            if (!authResult.Succeeded)
                return Forbid();

            var builder = new TestBuilder(collection.Cards);
            builder.GenerateTasks(test.OpenCnt, typeof(OpenAnswerQuestion));
            builder.GenerateTasks(test.ChoiceCnt, typeof(ChoiceQuestion));
            builder.GenerateTasks(test.MatchCnt, typeof(MatchingQuestion));

            var exercises = builder.Build();
            var testId = answersStorage.AddAnswers(exercises);

            return Ok(new Dictionary<string, object>{{"testId", testId}, {"exercises", exercises}});
        }

        [HttpPost("check")]
        public ActionResult CheckAnswers(TestAnswersDto answers)
        {
            var correctAnswers = answersStorage.FindAnswers(answers.TestId);
            var counter = 
                (from answer in correctAnswers 
                let userAnswer = answers.Answers.First(a => a.Id == answer.Id) 
                where answer.IsTheSameAs(userAnswer) 
                select answer).Count();

            return Ok($"You scored {counter}.");
        }
    }
}