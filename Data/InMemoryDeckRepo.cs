using System;
using System.Collections.Generic;
using System.Linq;
using deckOfCards.Models;

namespace deckOfCards.Data
{
    public class InMemoryDeckRepo : IDeckRepo
    {
        private readonly CardContext _context;

        public InMemoryDeckRepo(CardContext context)
        {
            _context = context;
        }
        IEnumerable<Card> IDeckRepo.GetCurrentDeck()
        {
            return _context.Deck.ToList();
        }

        public IEnumerable<Card> CreateDeck()
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    Card card = new(i, j);
                    _context.Deck.Add(card);

                }
            }
            _context.SaveChanges();

            return _context.Deck.ToList();
        }

        IEnumerable<Card> IDeckRepo.DealHand(int size)
        {
            var deck = _context.Deck.ToList();
            var hand = deck.Take(size);

            foreach (Card c in hand)
            {
                _context.Deck.Remove(c);
            }
            _context.SaveChanges();

            return hand;
        }

        void IDeckRepo.ResetDeck()
        {
            var deck = _context.Deck.ToList();

            foreach (Card c in deck)
            {
                _context.Remove(c);
            }
            _context.SaveChanges();

            CreateDeck();
        }

        void IDeckRepo.ShuffleDeck()
        {
            throw new NotImplementedException();
        }

        bool IDeckRepo.Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}