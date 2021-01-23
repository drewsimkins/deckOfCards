using System;
using System.Collections.Generic;
using deckOfCards.Models;

namespace deckOfCards.Data
{
    public interface IDeckRepo
    {
        //Get
        IList<Card> GetCurrentDeck();
        //Create
        IList<Card> CreateDeck();
        //Deal
        IList<Card> DealHand(int size);
        //Reset
        void ResetDeck();
        //Shuffle
        void ShuffleDeck();
        bool Save();
    }
}