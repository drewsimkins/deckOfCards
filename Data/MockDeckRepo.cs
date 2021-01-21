using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using deckOfCards.Models;

namespace deckOfCards.Data
{
    public class MockDeckRepo : IDeckRepo
    {
        public IEnumerable<Card> CreateDeck()
        {
            List<Card> deck = new();

            for (int i = 0; i <= 12; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    Card card = new(i, j);
                    deck.Add(card);
                }
            }

            return deck;
        }

        public IEnumerable<Card> DealHand(int size)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> ResetDeck()
        {
            //Not required as we can just run create deck again. 
            //Method kept as future repositories might function differently
            return CreateDeck();
        }

        public void ShuffleDeck()
        {
            throw new NotImplementedException();
        }
    }
}