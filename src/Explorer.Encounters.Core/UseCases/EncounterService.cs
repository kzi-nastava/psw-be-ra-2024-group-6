using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterCreateDto,Encounter>,IEncounterService
    {

        private readonly IEncounterRepository _encounterRepository;
        private readonly IMapper mapper;
    
        public EncounterService(IMapper mapper,IEncounterRepository encounterRepository, ICrudRepository<Encounter> repository) :base(repository, mapper) 
        {
            _encounterRepository = encounterRepository;
            this.mapper = mapper;
        }

        public Result<EncounterCreateDto> Create(EncounterCreateDto encounterDto)
        {
            try
            {
                return MapToDto(_encounterRepository.Create(mapper.Map<Encounter>(encounterDto)));
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result Delete(long id)
        {
            var existingEntity = _encounterRepository.GetEncounter(id);
            if (existingEntity == null)
            {
                return Result.Fail(FailureCode.NotFound).WithError($"Encounter with ID {id} not found.");
            }
            _encounterRepository.Delete(id);
            return Result.Ok();
        }

        public Result<List<EncounterReadDto>> GetPaged()
        {
            
                return mapper.Map<List<EncounterReadDto>>(_encounterRepository.GetPagedEncounters());
            
            
        }

        public Result<List<EncounterReadDto>> GetAllActiveEncounters()
        {
            
            return mapper.Map<List<EncounterReadDto>> (_encounterRepository.GetAllActiveEncounters());
            
        }

        public Result<EncounterCreateDto> Update(EncounterCreateDto encounterDto)
        {
            try
            {
                var el= _encounterRepository.Update(MapToDomain(encounterDto));
                return MapToDto(el);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);

            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);

            }
        }
    }
}
