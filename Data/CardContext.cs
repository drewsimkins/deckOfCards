using deckOfCards.Models;
using Microsoft.EntityFrameworkCore;

namespace deckOfCards.Data
{
    public class CardContext : DbContext
    {
        public CardContext(DbContextOptions<CardContext> opt) : base(opt)
        {

        }

        public DbSet<Card> Deck { get; set; }
    }
}