using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using deckOfCards.Data;
using deckOfCards.Dtos;
using deckOfCards.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace deckOfCards.Controllers
{
    //Could have used wildcard [controller] but don't want endpoint to change if controller name alters in this instance
    [Route("api/deck")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private const int expectedDeckSize = 52;
        private readonly IDeckRepo _repo;
        private readonly IMapper _mapper;

        public DeckController(IDeckRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Will retrieve the current deck if it exists
        /// </summary>
        /// <returns>OK and items if it exists, otherwise 404</returns>
        [HttpGet(Name = "GetCurrentDeck")]
        public ActionResult<IList<CardReadDto>> GetCurrentDeck()
        {
            var currentDeck = _repo.GetCurrentDeck();
            if (currentDeck == null || !currentDeck.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IList<CardReadDto>>(currentDeck));
        }
        /// <summary>
        /// Will create a new deck of cards
        /// </summary>
        /// <returns>Action result along with a deck of cards</returns>
        [HttpPut]
        public ActionResult<IList<CardReadDto>> CreateDeckOfCards()
        {
            var createdDeck = _repo.CreateDeck();
            return CreatedAtRoute(nameof(GetCurrentDeck), _mapper.Map<IList<CardReadDto>>(createdDeck));
        }

        /// <summary>
        /// Resets the deck back to an ordered list of cards
        /// </summary>
        /// <returns>No content</returns>
        [HttpGet("reset")]
        public ActionResult ResetDeckOfCards()
        {
            _repo.ResetDeck();
            return NoContent();
        }

        /// <summary>
        /// Returns a list of cards
        /// </summary>
        /// <param name="size">Number of cards wanted</param>
        /// <returns>List of cards as long as the number requested is less than the size of the current deck.
        ///             Otherwise will return a bad request</returns>
        [HttpGet("{size}")]
        public ActionResult<IList<CardReadDto>> GetHand(int size)
        {
            var currentDeck = _repo.GetCurrentDeck();

            if (currentDeck == null)
            {
                return NotFound();
            }

            if (size > currentDeck.Count)
            {
                return BadRequest("Hand size larger than deck size");
            }

            var hand = _repo.DealHand(size);
            _repo.Save();
            return Ok(_mapper.Map<IList<CardReadDto>>(hand));
        }

        /// <summary>
        /// Shuffles the current deck if it's a whole deck
        /// </summary>
        /// <returns>No content if deck is of correct size, otherwise returns a BadRequest</returns>
        [HttpGet("shuffle")]
        public ActionResult ShuffleDeck()
        {
            //Validate current deck is of the correct size
            var currentDeck = _repo.GetCurrentDeck();
            if (currentDeck.Count != expectedDeckSize)
            {
                return BadRequest("Invalid deck size");
            }

            _repo.ShuffleDeck();
            _repo.Save();
            return NoContent();
        }
    }
}