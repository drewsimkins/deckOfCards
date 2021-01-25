using deckOfCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
