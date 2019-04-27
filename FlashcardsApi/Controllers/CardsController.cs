using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Flashcards;

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

        [HttpGet("all")]
        public ActionResult<IEnumerable<Card>> GetAll()
        {
            return Ok(storage.GetAllCards());
        }

        [HttpGet("{id}", Name = "GetCardById")]
        public ActionResult<Card> GetById([FromRoute] string id)
        {
            var card = storage.FindCard(id);
            if (card == null)
                return NotFound();
            return Ok(card);
        }

        [HttpPost("create")]
        public ActionResult CreateCard([FromBody] Card card)
        {
            storage.AddCard(card);

            return CreatedAtRoute(
                "GetCardById", new { id = card.Id }, card.Id);
        }

        [HttpDelete("delete")]
        public ActionResult DeleteCard([FromBody] string id)
        {
            storage.DeleteCard(id);
            return Ok("Card deleted");
        }
    }
}
