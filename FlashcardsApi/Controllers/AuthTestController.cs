using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok($"{User.Identity.Name}");
        }
    }
}
