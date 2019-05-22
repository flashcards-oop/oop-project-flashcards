using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Flashcards;
using FlashcardsApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FlashcardsApi.Controllers
{
    [Route("api/[controller]")]
    public class CollectionsController : Controller
    {
        private readonly IStorage storage;
        private readonly IAuthorizationService authorizationService;

        public CollectionsController(IStorage storage, IAuthorizationService authorizationService)
        {
            this.storage = storage;
            this.authorizationService = authorizationService;
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetAll()
        {
            return Ok(await Task.FromResult(storage.GetAllCollections()
                .Where(collection => collection.OwnerLogin == User.Identity.Name)));
        }
        
        [Authorize]
        [HttpPost("create")]
        public ActionResult AddCollection([FromBody] string name)
        {
            var newCollection = new Collection(name, User.Identity.Name);
            storage.AddCollection(newCollection);

            return CreatedAtRoute(
                "GetCollectionById", new { id = newCollection.Id }, newCollection.Id);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetCollectionById")]
        public async Task<ActionResult> GetCollection(string id)
        {
            var collection = await storage.FindCollection(id);
            if (collection == null)
                return NotFound();

            var ownsResource = await IsUsersResource(User, collection);
            if (ownsResource)
                return Ok(collection);
            return Forbid();
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetCollectionCards")]
        public async Task<ActionResult> GetCollectionCards(string id)
        {
            var collection = await storage.FindCollection(id);
            if (collection == null)
                return NotFound();

            var ownsResource = await IsUsersResource(User, collection);
            if (ownsResource)
                return Ok(await storage.GetCollectionCards(id));
            return Forbid();
        }

        [HttpDelete("delete")]
        public ActionResult DeleteCollection([FromBody] string id)
        {
            storage.DeleteCollection(id);
            return Ok("Collection deleted");
        }

        private async Task<bool> IsUsersResource(ClaimsPrincipal user, IOwnedResource resource)
        {
            var authResult = await authorizationService.AuthorizeAsync(user, resource, Policies.ResourceAccess);
            return authResult.Succeeded;
        }
    }
}
