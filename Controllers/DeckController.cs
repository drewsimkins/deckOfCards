using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using deckOfCards.Data;
using deckOfCards.Dtos;
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
        private readonly IMapper _mapper;

        public DeckController(IDeckRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCurrentDeck")]
        public ActionResult<IEnumerable<CardReadDto>> GetCurrentDeck()
        {
            var currentDeck = _repo.GetCurrentDeck();
            if (currentDeck == null || !currentDeck.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CardReadDto>>(currentDeck));
        }
        /// <summary>
        /// Will create a new deck of cards
        /// </summary>
        /// <returns>Action result along with a deck of cards</returns>
        [HttpGet("create")]
        public ActionResult<IEnumerable<CardReadDto>> CreateDeckOfCards()
        {
            var createdDeck = _repo.CreateDeck();
            return CreatedAtRoute(nameof(GetCurrentDeck), _mapper.Map<IEnumerable<CardReadDto>>(createdDeck));
        }

        [HttpGet("reset")]
        public ActionResult ResetDeckOfCards()
        {
            _repo.ResetDeck();
            return NoContent();
        }
    }
}