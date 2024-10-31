using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Mappers
{
    public class ProblemsProfile : Profile
    {
        public ProblemsProfile() { 

            CreateMap<ProblemDto, Problem>().ReverseMap();
            CreateMap<ProblemMessageDto, ProblemMessage>().ReverseMap();
        }
    }
}
