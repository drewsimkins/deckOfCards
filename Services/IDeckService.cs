using deckOfCards.Models;
using System.Collections.Generic;

namespace deckOfCards.Services
{
    public interface IDeckService
    {
        IList<Card> GetDeck();
        IList<Card> CreateDeck();
        IList<Card> DealHand(int s);
        void ResetDeck();
        void ShuffleDeck();
        
    }
}
