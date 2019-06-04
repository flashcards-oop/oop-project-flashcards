using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Flashcards;
using FlashcardsApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

namespace FlashcardsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private readonly IStorage storage;
        private readonly FilterGenerator filterGenerator;

        public CardsController(IStorage storage, FilterGenerator filterGenerator)
        {
            this.storage = storage;
            this.filterGenerator = filterGenerator;
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Card>>> GetAll()
        {
            return Ok((await storage.GetAllCards()).Where(card => User.OwnsResource(card)));
        }

        [Authorize]
        [HttpPost("all")]
        public async Task<ActionResult<IEnumerable<Card>>> GetAllFiltered([FromBody] FilterDto filterDto)
        {
            var cards = (await storage.GetAllCards()).Where(card => User.OwnsResource(card));
            
            var filter = filterGenerator.GetFilter(filterDto);
            if (filter != null)
                cards = filter(cards);
            return Ok(cards);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetCardById")]
        public async Task<ActionResult<Card>> GetById([FromRoute] string id)
        {
            var card = await storage.FindCard(id);
            if (card == null)
                return NotFound();
            
            if (User.OwnsResource(card))
                return Ok(card);
            return Forbid();
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> CreateCard([FromBody] CardDto cardDto) 
        {
            var newCard = new Card(cardDto.Term, cardDto.Definition, User.Identity.Name, cardDto.CollectionId);
            var collection = await storage.FindCollection(cardDto.CollectionId);

            if (collection == null)
                return UnprocessableEntity("collection does not exist");
            if (!User.OwnsResource(collection))
                return Forbid();

            await storage.AddCard(newCard);
            return CreatedAtRoute(
                "GetCardById", new { id = newCard.Id }, newCard.Id);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteCard([FromBody] string id)
        {
            var card = await storage.FindCard(id);
            if (card == null)
                return NotFound();
            
            if (!User.OwnsResource(card))
                return Forbid();

            await storage.DeleteCard(id);
            return Ok("Card deleted");
        }
    }
}
