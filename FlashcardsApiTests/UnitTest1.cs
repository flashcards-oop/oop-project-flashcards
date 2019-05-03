using Xunit;
using FlashcardsApi.Controllers;
using FakeItEasy;
using Flashcards;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;

namespace FlashcardsApiTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var fakeStorage = A.Fake<IStorage>();
            A.CallTo(() => fakeStorage.GetAllCards()).Returns(
                new[]
                {
                    new Card("t1", "d1", "admin"),
                    new Card("t2", "d2", "admin")
                });

            var fakeAuth = A.Fake<IAuthorizationService>();
            A.CallTo(() => fakeAuth.AuthorizeAsync(new ClaimsPrincipal(), new object(), "Kek"))
                .WithAnyArguments()
                .Returns(Task.FromResult(AuthorizationResult.Success()));

            var cardsController = new CardsController(fakeStorage, fakeAuth);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "admin")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            cardsController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = claimsPrincipal
                }
            };

            var result = cardsController.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
