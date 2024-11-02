using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Object = Explorer.Tours.Core.Domain.Tours.Object;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();

        CreateMap<TourDto, Tour>().ReverseMap();

        CreateMap<TouristEquipmentManagerDto, TouristEquipmentManager>().ReverseMap();

        CreateMap<Tour, TourReadDto>().ReverseMap();
    }
}