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

        public int Value { get; set; }

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
    }
}