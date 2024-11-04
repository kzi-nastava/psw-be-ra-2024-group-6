using AutoMapper;
using Explorer.Tours.API.Dtos;
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
        CreateMap<Location, LocationReadDto>().ReverseMap();
    }
}
