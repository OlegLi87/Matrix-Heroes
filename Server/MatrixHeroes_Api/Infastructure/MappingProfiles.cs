using System;
using AutoMapper;
using MatrixHeroes_Api.Core.Models.Domain;
using MatrixHeroes_Api.Core.Models.Dtos;

namespace MatrixHeroes_Api.Infastructure
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<HeroInputDto, Hero>()
                   .ForMember(dest => dest.CurrentPower, opts => opts.MapFrom(src => src.StartingPower));

            CreateMap<Hero, HeroOutputDto>()
                   .ForMember(dest => dest.AbilityName, opts => opts.MapFrom(src => src.Ability))
                   .ForMember(dest => dest.AbilityId, opts => opts.MapFrom(src => src.Ability));

            CreateMap<Ability, string>()
                   .ConvertUsing(src => src.Name);

            CreateMap<Ability, Guid>()
                   .ConvertUsing(src => src.Id);

            CreateMap<Ability, AbilityDto>();
        }
    }
}