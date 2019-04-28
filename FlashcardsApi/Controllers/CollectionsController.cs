using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Flashcards;
using AutoMapper;
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
        public ActionResult<IEnumerable<CollectionInfoDto>> GetAll()
        {
            return Ok(storage.GetAllCollections()
                .Where(collection => collection.OwnerLogin == User.Identity.Name)
                .Select(Mapper.Map<CollectionInfoDto>));
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetCollectionById")]
        public async Task<ActionResult> GetById(string id)
        {
            var collection = storage.FindCollection(id);
            if (collection == null)
                return NotFound();

            var ownsResource = await IsUsersResource(User, collection);
            if (ownsResource)
                return Ok(Mapper.Map<CollectionDto>(collection));
            return Forbid();
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
        [HttpPost("{id}/add")]
        public async Task<ActionResult> AddCardToCollection(string id, [FromBody] string cardId)
        {
            try
            {
                if (await IsAuthorizedCollectionOperation(id, cardId))
                {
                    storage.AddCardToCollection(id, cardId);
                    return NoContent();
                }
                return Forbid();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost("{id}/remove")]
        public async Task<ActionResult> RemoveCardFromCollection(string id, [FromBody] string cardId)
        {
            try
            {
                if (await IsAuthorizedCollectionOperation(id, cardId))
                {
                    storage.RemoveCardFromCollection(id, cardId);
                    return NoContent();
                }
                return Forbid();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        private async Task<bool> IsAuthorizedCollectionOperation(string collectionId, string cardId)
        {
            var collection = storage.FindCollection(collectionId);
            var card = storage.FindCard(cardId);
            if (collection == null || card == null)
                throw new InvalidOperationException();

            var ownershipChecks = await Task.WhenAll(new []{ IsUsersResource(User, card), IsUsersResource(User, collection) });
            return ownershipChecks.All(check => check);
        }

        private async Task<bool> IsUsersResource(ClaimsPrincipal user, IOwnedResource resource)
        {
            var authResult = await authorizationService.AuthorizeAsync(user, resource, "ResourceAccess");
            return authResult.Succeeded;
        }
    }
}
