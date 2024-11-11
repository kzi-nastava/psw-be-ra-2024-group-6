using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Persons;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<RatingDto, Rating>().ReverseMap();
        CreateMap<PersonDto, Person>().ReverseMap();
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<FollowerCreateDto, Follower>().ReverseMap();
        CreateMap<FollowerReadDto, Follower>().ReverseMap();
        CreateMap<NotificationCreateDto, Notification>();
        CreateMap<NotificationDto, Notification>().ReverseMap();
    }
}