using System;

namespace deckOfCards.Exceptions
{
    public class InvalidHandException : Exception
    {
        public InvalidHandException(string message) : base(message)
        {
        }

        public int Status { get; set; } = 400;
    }
}
