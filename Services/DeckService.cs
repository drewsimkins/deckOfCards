using deckOfCards.Data;
using deckOfCards.Exceptions;
using deckOfCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deckOfCards.Services
{
    public class DeckService : IDeckService
    {
        private IDeckRepo _deckRepo;
        private ILoggerManager _logger;
        private const int expectedDeckSize = 52;

        public DeckService(IDeckRepo deckRepo, ILoggerManager logger)
        {
            _deckRepo = deckRepo;
            _logger = logger;
        }
        
        public void ShuffleDeck()
        {
            var deck = _deckRepo.GetCurrentDeck();
            if(deck == null || deck.Count != expectedDeckSize)
            {
                _logger.LogError("Invalid deck size");
                throw new InvalidDeckException("Invalid deck size");
            }

            _deckRepo.ShuffleDeck();
            _deckRepo.Save();
        }

        IList<Card> IDeckService.GetDeck()
        {
            var deck = _deckRepo.GetCurrentDeck();

            if(deck == null || !deck.Any())
            {
                _logger.LogError("Deck not found");
                throw new InvalidDeckException("Deck not found");
            }

            return deck;
        }

        IList<Card> IDeckService.CreateDeck()
        {
            return _deckRepo.CreateDeck();
        }

        void IDeckService.ResetDeck()
        {
            _deckRepo.ResetDeck();
        }

        IList<Card> IDeckService.DealHand(int size)
        {
            var deck = _deckRepo.GetCurrentDeck();
            if(deck == null || size > deck.Count)
            {
                _logger.LogError("Invalid hand size");
                throw new InvalidDeckException("Invalid hand size");
            }

            var hand = _deckRepo.DealHand(size);
            _deckRepo.Save();

            return hand;
        }
    }
}
