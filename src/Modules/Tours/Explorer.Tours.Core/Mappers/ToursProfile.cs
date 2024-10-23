using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Object = Explorer.Tours.Core.Domain.Object;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();

        CreateMap<TourDto, Tour>().ReverseMap();

        CreateMap<RequiredEquipmentDto, RequiredEquipment>().ReverseMap();
        CreateMap<TouristEquipmentManagerDto, TouristEquipmentManager>().ReverseMap();

        CreateMap<TourInfoDto, Tour>().ReverseMap();

        CreateMap<CheckpointCreateDto,Checkpoint>().ReverseMap();
        CreateMap<ObjectCreateDto,Object>().ReverseMap();
        CreateMap<LocationCreateDto,Location>().ReverseMap();
        CreateMap<LocationDto, Location>().ReverseMap();
        CreateMap<CheckpointCreateDto, Checkpoint>().ReverseMap();
        CreateMap<ObjectCreateDto, Object>().ReverseMap();
        CreateMap<LocationCreateDto, Location>().ReverseMap();



        CreateMap<ObjectCreateDto, Object>().ReverseMap();


    }
}