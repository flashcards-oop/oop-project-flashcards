using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Flashcards;
using FlashcardsApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace FlashcardsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private readonly IStorage storage;

        public CardsController(IStorage storage)
        {
            this.storage = storage;
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Card>>> GetAll(CancellationToken token)
        {
            return Ok((await storage.GetAllCards(token)).Where(card => User.OwnsResource(card)));
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetCardById")]
        public async Task<ActionResult<Card>> GetById([FromRoute] string id, CancellationToken token)
        {
            var card = await storage.FindCard(id, token);
            if (card == null)
                return NotFound();
            
            if (User.OwnsResource(card))
                return Ok(card);
            return Forbid();
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> CreateCard([FromBody] CardDto cardDto, CancellationToken token) 
        {
            var newCard = new Card(cardDto.Term, cardDto.Definition, User.Identity.Name, cardDto.CollectionId);
            var collection = await storage.FindCollection(cardDto.CollectionId, token);

            if (collection == null)
                return UnprocessableEntity("collection does not exist");
            if (!User.OwnsResource(collection))
                return Forbid();

            await storage.AddCard(newCard, token);
            return CreatedAtRoute(
                "GetCardById", new { id = newCard.Id }, newCard.Id);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteCard([FromBody] string id, CancellationToken token)
        {
            var card = await storage.FindCard(id, token);
            if (card == null)
                return NotFound();
            
            if (!User.OwnsResource(card))
                return Forbid();

            await storage.DeleteCard(id, token);
            return Ok("Card deleted");
        }
    }
}
