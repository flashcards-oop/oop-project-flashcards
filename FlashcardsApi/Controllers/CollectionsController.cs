using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Flashcards;
using FlashcardsApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Threading;
using Flashcards.Storages;

namespace FlashcardsApi.Controllers
{
    [Route("api/[controller]")]
    public class CollectionsController : Controller
    {
        private readonly IStorage storage;

        public CollectionsController(IStorage storage)
        {
            this.storage = storage;
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetAll(CancellationToken token)
        {
            return Ok((await storage.GetAllCollections(token))
                .Where(collection => User.OwnsResource(collection)));
        }
        
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> AddCollection([FromBody] string name, CancellationToken token)
        {
            var newCollection = new Collection(name, User.Identity.Name);
            await storage.AddCollection(newCollection, token);

            return CreatedAtRoute(
                "GetCollectionById", new { id = newCollection.Id }, newCollection.Id);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetCollectionById")]
        public async Task<ActionResult> GetCollection(Guid id, CancellationToken token)
        {
            var collection = await storage.FindCollection(id, token);
            if (collection == null)
                return NotFound();

            if (User.OwnsResource(collection))
                return Ok(collection);
            return Forbid();
        }

        [Authorize]
        [HttpGet("{id}/cards", Name = "GetCollectionCards")]
        public async Task<ActionResult> GetCollectionCards(Guid id, CancellationToken token)
        {
            var collection = await storage.FindCollection(id, token);
            if (collection == null)
                return NotFound();

            if (User.OwnsResource(collection))
                return Ok(await storage.GetCollectionCards(id, token));
            return Forbid();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteCollection([FromBody] Guid id, CancellationToken token)
        {
            var collection = await storage.FindCollection(id, token);
            if (collection == null)
                return NotFound();

            if (!User.OwnsResource(collection))
                return Forbid();

            await storage.DeleteCollection(id, token);
            return Ok("Collection deleted");
        }
    }
}
