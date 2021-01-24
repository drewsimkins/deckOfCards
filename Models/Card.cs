using System;
using System.ComponentModel.DataAnnotations;

namespace deckOfCards.Models
{
    public class Card
    {
        public int Id { get; set; }

        [Range(1, 13, ErrorMessage = "Card value must be between 1 & 13")]
        public int Value { get; set; }

        [Range(0, 3, ErrorMessage = "Card suit must be between 0 & 3")]
        public int Suit { get; set; }

        public string Name { get; private set; }

        public Card(int suit, int value)
        {
            Value = value;
            Suit = suit;
            Name = GenerateName();
        }

        private string GenerateName()
        {
            string value = (
                Value == 1 ? "Ace" :
                Value == 11 ? "Jack" :
                Value == 12 ? "Queen" :
                Value == 13 ? "King" : Value.ToString()
            );
            string suit = (
                Suit == 0 ? "Hearts" :
                Suit == 1 ? "Diamonds" :
                Suit == 2 ? "Clubs" : "Spades"
            );

            string name = $"{value} of {suit}";
            return name;
        }
    }
}