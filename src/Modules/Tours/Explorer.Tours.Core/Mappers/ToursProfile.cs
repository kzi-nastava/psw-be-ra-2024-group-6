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

        CreateMap<TourInfoDto, Tour>().ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name));

        CreateMap<CheckpointCreateDto,Checkpoint>().ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.Location.Id))
            .ForMember(dest => dest.TourId, opt => opt.MapFrom(src => src.TourId))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest=>dest.Location,opt=>opt.Ignore());
        CreateMap<ObjectCreateDto,Object>().ReverseMap();
        CreateMap<LocationCreateDto,Location>().ReverseMap();
        CreateMap<LocationDto, Location>().ReverseMap();
        CreateMap<CheckpointCreateDto, Checkpoint>().ReverseMap();
        CreateMap<ObjectCreateDto, Object>().ReverseMap();
        CreateMap<LocationCreateDto, Location>().ReverseMap();



        CreateMap<ObjectCreateDto, Object>().ReverseMap();


    }
}