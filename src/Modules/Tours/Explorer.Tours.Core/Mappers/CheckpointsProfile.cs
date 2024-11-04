using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class CheckpointsProfile : Profile
    {
        public CheckpointsProfile()
        {
            CreateMap<CheckpointDto, Checkpoint>().ReverseMap();
            CreateMap<Checkpoint, CheckpointReadDto>().ReverseMap();
            CreateMap<Location, LocationReadDto>().ReverseMap();
        }
    }
}
