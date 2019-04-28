﻿using System.Collections.Generic;
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
        private readonly IAuthorizationService authorizationService;

        public CardsController(IStorage storage, IAuthorizationService authorizationService)
        {
            this.storage = storage;
            this.authorizationService = authorizationService;
        }

        [Authorize]
        [HttpGet("all")]
        public ActionResult<IEnumerable<Card>> GetAll()
        {
            return Ok(storage.GetAllCards().Where(card => card.OwnerLogin == User.Identity.Name));
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetCardById")]
        public async Task<ActionResult<Card>> GetById([FromRoute] string id)
        {
            var card = storage.FindCard(id);
            if (card == null)
                return NotFound();
            var authResult = await authorizationService.AuthorizeAsync(User, card, "ResourceAccess");

            if (authResult.Succeeded)
                return Ok(card);
            return Forbid();
        }

        [Authorize]
        [HttpPost("create")]
        public ActionResult CreateCard([FromBody] CardDto cardDto) 
        {
            var newCard = new Card(cardDto.Term, cardDto.Definition, User.Identity.Name);
            storage.AddCard(newCard);

            return CreatedAtRoute(
                "GetCardById", new { id = newCard.Id }, newCard.Id);
        }
    }
}
