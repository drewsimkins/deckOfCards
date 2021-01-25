using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
