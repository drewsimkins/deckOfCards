using System.Collections.Generic;
using deckOfCards.Data;
using deckOfCards.Models;
using Microsoft.AspNetCore.Mvc;

namespace deckOfCards.Controllers
{
    //Could have used wildcard [controller] but don't want endpoint to change if controller name alters in this instance
    [Route("api/deck")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IDeckRepo _repo;

        public DeckController(IDeckRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Card>> GetCurrentDeck()
        {
            var currentDeck = _repo.GetCurrentDeck();
            if (currentDeck == null)
            {
                return NotFound();
            }

            return Ok(currentDeck);
        }
        /// <summary>
        /// Will create a new deck of cards
        /// </summary>
        /// <returns>Action result along with a deck of cards</returns>
        [HttpGet("create")]
        public ActionResult<IEnumerable<Card>> CreateDeckOfCards()
        {
            var deckOfCards = _repo.CreateDeck();
            return Ok(deckOfCards);
        }

        [HttpGet("reset")]
        public ActionResult ResetDeckOfCards()
        {
            _repo.ResetDeck();
            return NoContent();
        }
    }
}