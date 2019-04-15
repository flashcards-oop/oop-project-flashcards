﻿using System.Collections.Generic;
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

        [HttpGet("{id}")]
        public ActionResult<Card> GetById(string id)
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
                nameof(GetById),
                new { cardId = card.Id });
        }
    }
}