using System;
using System.ComponentModel.DataAnnotations;

namespace deckOfCards.Models
{
    public class Card
    {
        public enum Suites
        {
            Hearts = 0,
            Diamonds,
            Spades,
            Clubs
        }

        [Range(1, 13, ErrorMessage = "Card value must be between 1 & 13")]
        public int Value { get; set; }

        [Range(0, 3, ErrorMessage = "Card suit must be between 0 & 3")]
        public Suites Suit { get; set; }

        public string GetValue(Card card)
        {
            string value = string.Empty;

            value = (
                card.Value == 1 ? "Ace" :
                card.Value == 11 ? "Jack" :
                card.Value == 12 ? "Queen" :
                card.Value == 13 ? "King" : value.ToString()
            );

            value += (" of {0}", card.Suit);
            return value;
        }

        public Card(int value, int suit)
        {
            Value = value;
            Suit = (Suites)suit;
        }
    }
}