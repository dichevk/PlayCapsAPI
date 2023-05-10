using PlayCapsViewer.Models;
using AutoMapper;
using PlayCapsViewer.DTO;
using System;

namespace PlayCapsViewer.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<PlayCap, PlayCapDTO>();
            CreateMap<Player, PlayerDTO>();
            CreateMap<Country, CountryDTO>();
            CreateMap<Reviewer, ReviewerDTO>();
            CreateMap<Review, ReviewDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<PlayCapDTO, PlayCap>();
            CreateMap<PlayerDTO, Player>();
            CreateMap<CountryDTO, Country>();
            CreateMap<ReviewerDTO, Reviewer>();
            CreateMap<ReviewDTO, Review>();
        }
    }
}
