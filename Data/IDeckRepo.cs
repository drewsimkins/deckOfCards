using System;
using System.Collections.Generic;
using deckOfCards.Models;

namespace deckOfCards.Data
{
    public interface IDeckRepo
    {
        //Create
        IEnumerable<Card> CreateDeck();
        //Deal
        IEnumerable<Card> DealHand(int size);
        //Reset
        IEnumerable<Card> ResetDeck();
        //Shuffle
        void ShuffleDeck();
    }
}