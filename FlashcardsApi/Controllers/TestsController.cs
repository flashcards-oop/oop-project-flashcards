using System.Collections.Generic;
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
        public async Task<ActionResult> GenerateTest(TestDto test)
        {
            var collection = storage.FindCollection(test.CollectionId);
            if (collection is null)
            {
                return NotFound();
            }

            var authResult = await authorizationService.AuthorizeAsync(User, collection, Policies.ResourceAccess);
            if (!authResult.Succeeded)
                return Forbid();

            var exercises = new TestBuilder(collection.Cards, new RandomCardsSelector())
                .WithGenerator(new OpenQuestionExerciseGenerator(), test.OpenCnt)
                .WithGenerator(new MatchingQuestionExerciseGenerator(), test.MatchCnt)
                .Build();

            var testId = answersStorage.AddAnswers(exercises);

            return Ok(new Dictionary<string, object>{{"testId", testId}, {"exercises", exercises}});
        }
    }
}