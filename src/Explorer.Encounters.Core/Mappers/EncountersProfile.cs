using AutoMapper;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Mappers
{
    public class EncountersProfile : Profile
    {
        public EncountersProfile() 
        {

            //CreateMap<EncounterCreateDto, Encounter>().ReverseMap();
            CreateMap<EncounterReadDto,Encounter>()
                .Include<HiddenEncounterReadDto, HiddenEncounter>()
                .Include<SocialEncounterReadDto, SocialEncounter>().ReverseMap();
            CreateMap<SocialEncounterCreateDto, SocialEncounter>().ReverseMap();
            CreateMap<HiddenEncounterDto, HiddenEncounter>().ReverseMap();
            CreateMap<HiddenEncounterReadDto, HiddenEncounter>().ReverseMap();
            CreateMap<SocialEncounterReadDto, SocialEncounter>().ReverseMap();

            CreateMap<EncounterCreateDto, Encounter>()
                .Include<HiddenEncounterDto, HiddenEncounter>()
                .Include<SocialEncounterCreateDto, SocialEncounter>().ReverseMap();


            CreateMap<EncounterByTouristReadDto, Encounter>().ReverseMap();


            CreateMap<HiddenEncounterExecutionDto, HiddenEncounterExecution>().ReverseMap();

            CreateMap<EncounterExecutionDto, EncounterExecution>()
                .Include<HiddenEncounterExecutionDto, HiddenEncounterExecution>().ReverseMap();
        }
        
    }
}
