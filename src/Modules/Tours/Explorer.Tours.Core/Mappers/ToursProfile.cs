using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;
using Explorer.Tours.API.Dtos.TourDtos.DurationDtos;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using Explorer.Tours.API.Dtos.TourDtos.PriceDtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Object = Explorer.Tours.Core.Domain.Tours.Object;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<ReviewDto, Review>().ReverseMap();
        CreateMap<EquipmentDto, Equipment>().ReverseMap();

        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<Tour, TourDto>().ForMember(dest => dest.IsPublished, opt => opt.MapFrom(src => !src.IsNotPublished()));

        CreateMap<TouristEquipmentManagerDto, TouristEquipmentManager>().ReverseMap();

        CreateMap<Tour, TourReadDto>().ReverseMap();
        CreateMap<Price,PriceDto>().ReverseMap();
        CreateMap<TourDuration, TourDurationDto>().ReverseMap();
        CreateMap<Distance,DistanceDto>().ReverseMap();
        //CreateMap<TourCardDto, Tour>().ReverseMap();
        CreateMap<TourReviewDto,Review>().ReverseMap();
        CreateMap<TourCardDto, Tour>()
            .ReverseMap().ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.TotalLength.Length + src.TotalLength.Unit.ToString()));

        CreateMap<TourExecutionDto, TourExecution>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Position,
                opt => opt.MapFrom(src => new TouristPosition(src.Longitude, src.Latitude)));
        CreateMap<TourExecution, TourExecutionDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Position.Longitude))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Position.Latitude));
    }
}