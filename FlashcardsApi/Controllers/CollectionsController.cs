using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Flashcards;
using AutoMapper;
using FlashcardsApi.Models;

namespace FlashcardsApi.Controllers
{
    [Route("api/[controller]")]
    public class CollectionsController : Controller
    {
        private IStorage storage;

        public CollectionsController(IStorage storage)
        {
            this.storage = storage;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<CollectionInfoDto>> GetAll()
        {
            return Ok(storage.GetAllCollections()
                .Select(collection => Mapper.Map<CollectionInfoDto>(collection)));
        }

        [HttpGet("{id}", Name = "GetCollectionById")]
        public ActionResult GetById(string id)
        {
            var collection = storage.FindCollection(id);
            if (collection == null)
                return NotFound();
            return Ok(Mapper.Map<CollectionDto>(collection));
        }

        [HttpPost("create")]
        public ActionResult AddCollection([FromBody] string name)
        {
            var newCollection = new Collection(name);
            storage.AddCollection(newCollection);

            return CreatedAtRoute(
                "GetCollectionById", new { id = newCollection.Id }, newCollection.Id);
        }

        [HttpPost("{id}/add")]
        public ActionResult AddCardToCollection(string id, [FromBody] string cardId)
        {
            try
            {
                storage.AddCardToCollection(id, cardId);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
