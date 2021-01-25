using AutoMapper;
using deckOfCards.Data;
using deckOfCards.Dtos;
using deckOfCards.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace deckOfCards.Controllers
{
    //Could have used wildcard [controller] but don't want endpoint to change if controller name alters in this instance
    [Route("api/deck")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDeckService _service;
        private readonly ILoggerManager _logger;

        public DeckController(IMapper mapper, IDeckService service, ILoggerManager logger)
        {
            _mapper = mapper;
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Will retrieve the current deck if it exists
        /// </summary>
        /// <returns>OK and items if it exists, otherwise 404</returns>
        [HttpGet(Name = "GetCurrentDeck")]
        public ActionResult<IList<CardReadDto>> GetCurrentDeck()
        {
            var currentDeck = _service.GetDeck();
            if (currentDeck == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IList<CardReadDto>>(currentDeck));
        }
        /// <summary>
        /// Will create a new deck of cards
        /// </summary>
        /// <returns>Action result along with a deck of cards</returns>
        [HttpPost]
        public ActionResult<IList<CardReadDto>> CreateDeckOfCards()
        {
            var createdDeck = _service.CreateDeck();
            return CreatedAtRoute(nameof(GetCurrentDeck), _mapper.Map<IList<CardReadDto>>(createdDeck));
        }

        /// <summary>
        /// Resets the deck back to an ordered list of cards
        /// </summary>
        /// <returns>No content</returns>
        [HttpGet("reset")]
        public ActionResult ResetDeckOfCards()
        {
            _service.ResetDeck();
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
            var hand = _service.DealHand(size);
            return Ok(_mapper.Map<IList<CardReadDto>>(hand));
        }

        /// <summary>
        /// Shuffles the current deck if it's a whole deck
        /// </summary>
        /// <returns>No content if deck is of correct size, otherwise returns a BadRequest</returns>
        [HttpGet("shuffle")]
        public ActionResult ShuffleDeck()
        {
            _service.ShuffleDeck();
            return NoContent();
        }
    }
}