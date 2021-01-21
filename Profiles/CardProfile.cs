using AutoMapper;
using deckOfCards.Dtos;
using deckOfCards.Models;

namespace deckOfCards.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardReadDto>();
        }
    }
}