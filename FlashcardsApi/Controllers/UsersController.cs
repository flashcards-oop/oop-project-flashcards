using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Flashcards.Storage;
using System.Security.Claims;
using Flashcards;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace FlashcardsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        IUserStorage userStorage;

        public UsersController(IUserStorage userStorage)
        {
            this.userStorage = userStorage;
        }

        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] string login)
        {
            userStorage.AddUser(new User(login));
            return NoContent();
        }

        [HttpPost("token")]
        public IActionResult Token([FromBody] string login)
        {
            var identity = GetIdentity(login);
            if (identity == null)
                return BadRequest();

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(string login)
        {
            User user = userStorage.FindUserByLogin(login);
            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            return claimsIdentity;
        }
    }
}
