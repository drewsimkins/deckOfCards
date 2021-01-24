using AutoMapper;
using deckOfCards.Models;
using deckOfCards.Tools;
using System.Collections.Generic;
using System.Linq;

namespace deckOfCards.Data
{
    public class InMemoryDeckRepo : IDeckRepo
    {
        private readonly CardContext _context;
        private readonly IMapper _mapper;

        public InMemoryDeckRepo(CardContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        IList<Card> IDeckRepo.GetCurrentDeck()
        {
            return _context.Deck.OrderBy(c => c.Order).ToList();
        }

        public IList<Card> CreateDeck()
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    var order = i * 13 + j;
                    Card card = new(i, j, order);
                    _context.Deck.Add(card);
                }
                _context.SaveChanges();
            }

            return _context.Deck.OrderBy(c => c.Order).ToList();
        }

        IList<Card> IDeckRepo.DealHand(int size)
        {
            var deck = _context.Deck.ToList();
            var hand = deck.Take(size).ToList();

            foreach (Card c in hand)
            {
                _context.Deck.Remove(c);
            }

            return hand;
        }

        void IDeckRepo.ResetDeck()
        {
            var deck = _context.Deck.ToList();

            if (deck.Any())
            {
                foreach (Card c in deck)
                {
                    _context.Remove(c);
                }
                _context.SaveChanges();
            }

            CreateDeck();
        }

        void IDeckRepo.ShuffleDeck()
        {
            var deck = _context.Deck.ToList();

            if (deck.Any())
            {
                deck.Shuffle();

                foreach (Card c in deck)
                {
                    _context.Deck.Remove(c);
                }
                _context.SaveChanges();

                foreach (Card c in deck)
                {
                    _context.Deck.Add(c);
                }
                _context.SaveChanges();
            }
        }

        bool IDeckRepo.Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}