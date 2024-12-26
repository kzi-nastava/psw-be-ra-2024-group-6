using AutoMapper;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointDtos;

namespace Explorer.Tours.Core.Mappers
{
    public class CheckpointsProfile : Profile
    {
        public CheckpointsProfile()
        {
            CreateMap<CheckpointDto, Checkpoint>().ReverseMap();
            CreateMap<Checkpoint, CheckpointCreateDto>().ReverseMap();
            CreateMap<Checkpoint, CheckpointHoverMapDto>().ReverseMap();
            CreateMap<Checkpoint, CheckpointReadDto>()
                .ForMember(dest => dest.PublicRequest, opt => opt.MapFrom(src => src.PublicRequest))
                .ReverseMap();

            // Prilagođena konverzija za PublicCheckpointRequestDto -> PublicCheckpointRequest
            CreateMap<PublicCheckpointRequestDto, PublicCheckpointRequest>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MapStatus(src.Status)))
                .ForMember(dest => dest.AdminComment, opt => opt.MapFrom(src => src.AdminComment));

            CreateMap<PublicCheckpointRequest, PublicCheckpointRequestDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.AdminComment, opt => opt.MapFrom(src => src.AdminComment));
        }

        private static PublicCheckpointStatus MapStatus(string? status)
        {
            return Enum.TryParse<PublicCheckpointStatus>(status, true, out var parsedStatus)
                ? parsedStatus
                : PublicCheckpointStatus.Pending;
        }
    }

}
