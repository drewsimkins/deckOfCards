using System;
using System.Collections.Generic;
using deckOfCards.Models;

namespace deckOfCards.Data
{
    public interface IDeckRepo
    {
        //Get
        IEnumerable<Card> GetCurrentDeck();
        //Create
        IEnumerable<Card> CreateDeck();
        //Deal
        IEnumerable<Card> DealHand(int size);
        //Reset
        void ResetDeck();
        //Shuffle
        void ShuffleDeck();
        bool Save();
    }
}