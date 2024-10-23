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

        //CreateMap<TourInfoDto, Tour>().ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name));
        CreateMap<TourCreateDto, Tour>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TourInfo.Name))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.TourInfo.AuthorId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TourInfo.Status))
            .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.TourInfo.Difficulty))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TourInfo.Description))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.TourInfo.Tags));



        CreateMap<CheckpointCreateDto,Checkpoint>().ReverseMap();
        CreateMap<ObjectCreateDto,Object>().ReverseMap();
        
    }
}