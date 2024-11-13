using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Mappers
{
    internal class LocationsProfile : Profile
    {
        public LocationsProfile()
        {
            CreateMap<Location, LocationReadDto>().ReverseMap();
            CreateMap<Location, LocationCreateDto>().ReverseMap();
        }
    }
}
