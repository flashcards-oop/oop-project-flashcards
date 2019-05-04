using System;
using System.Collections.Generic;
using FakeItEasy;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FlashcardsApi;
using Microsoft.Extensions.Options;

namespace FlashcardsApiTests
{
    public class ControllerTestsHelper
    {
        public static IAuthorizationService GetStandardAuthorizationService(bool successful = true)
        {
            var fakeAuth = A.Fake<IAuthorizationService>();
            A.CallTo(() => fakeAuth.AuthorizeAsync(new ClaimsPrincipal(), new object(), "Any"))
                .WithAnyArguments()
                .Returns(Task.FromResult(successful ? AuthorizationResult.Success() : AuthorizationResult.Failed()));
            return fakeAuth;
        }

        public static void AttachUserToControllerContext(Controller controller, string userName)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userName)
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = claimsPrincipal
                }
            };
        }
    }
}
