using System;

namespace deckOfCards.Exceptions
{
    public class InvalidDeckException : Exception
    {
        public InvalidDeckException(string message) : base(message)
        {
        }

        public int Status { get; set; } = 500;
    }
}
