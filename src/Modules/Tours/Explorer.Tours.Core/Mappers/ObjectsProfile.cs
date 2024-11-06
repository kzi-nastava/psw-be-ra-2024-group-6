using AutoMapper;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers;

public class ObjectsProfile : Profile
{
    public ObjectsProfile()
    {
        CreateMap<ObjectDto, Domain.Tours.Object>().ReverseMap();
        CreateMap<Domain.Tours.Object, ObjectReadDto>().ReverseMap();
        CreateMap<Domain.Tours.Object, ObjectCreateDto>().ReverseMap();
    }
}
