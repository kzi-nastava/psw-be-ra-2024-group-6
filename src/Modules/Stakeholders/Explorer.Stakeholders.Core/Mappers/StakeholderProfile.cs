using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<RatingDto, Rating>().ReverseMap();
        CreateMap<PersonDto, Person>().ReverseMap();
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
    }
}