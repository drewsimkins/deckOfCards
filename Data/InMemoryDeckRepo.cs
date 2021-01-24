using AutoMapper;
using deckOfCards.Models;
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
            var deck = _context.Deck.OrderBy(c => c.Order).ToList();
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
                deck.ForEach(c => c.Order = c.Id);
                _context.SaveChanges();
            }
        }

        void IDeckRepo.ShuffleDeck()
        {
            var deck = _context.Deck.ToList();

            if (deck.Any())
            {
                System.Random rand = new();
                var randomDeck = deck.Select(x => new { value = x, order = rand.Next() })
                    .OrderBy(x => x.order).Select(x => x.value).ToList();

                for(int i = 0; i < randomDeck.Count; i++)
                {
                    var card = randomDeck[i];
                    card.Order = i;
                }

                deck = randomDeck;
                _context.SaveChanges();
            }
        }

        bool IDeckRepo.Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}