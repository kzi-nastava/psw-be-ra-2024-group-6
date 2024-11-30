using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterExecutionService : CrudService<EncounterExecutionDto,EncounterExecution>, IEncounterExecutionService
    {
        private readonly IEncounterExecutionRepository _encounterExecutionRepository;
        private readonly IEncounterService _encounterService;
        private readonly IMapper mapper;

        public EncounterExecutionService(IEncounterService encounterService, ICrudRepository<EncounterExecution> repository,IEncounterExecutionRepository encounterExecutionRepository, IMapper mapper) : base(repository, mapper)
        {
            _encounterExecutionRepository = encounterExecutionRepository;
            this.mapper = mapper;
            _encounterService = encounterService;
        }
        public Result<EncounterExecutionDto> Create(EncounterExecutionDto encounterDto)
        {
            try
            {
                return MapToDto(_encounterExecutionRepository.Create(mapper.Map<EncounterExecution>(encounterDto)));
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result Delete(long id)
        {
            try
            {

                _encounterExecutionRepository.Delete(id);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.NotFound)
                    .WithError($"Encounter with ID {id} not found.");
            }
        }

        public Result<List<EncounterExecutionDto>> GetPaged()
        {

            return mapper.Map<List<EncounterExecutionDto>>(_encounterExecutionRepository.GetPagedEncounterExecutions());
        }

        public Result<EncounterExecutionDto> Update(EncounterExecution encounter)
        {
            try
            {
                var el = _encounterExecutionRepository.Update(encounter);
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

        public Result<EncounterExecutionDto> StartMiscExecution(long encounterId, int touristId)
        {
            EncounterReadDto encounter = _encounterService.GetById(encounterId).Value;
            if (!((TypeEncounter)Enum.Parse(typeof(TypeEncounter), encounter.TypeEncounter) == TypeEncounter.Misc))
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Encounter is not a misc encounter.");
            }

            EncounterExecution execution = new EncounterExecution(encounterId, touristId);
            EncounterExecutionDto dto = MapToDto(execution);
            return Create(dto);
        }
        public Result<EncounterExecutionDto> FinishMiscExecution(long executionId, int touristId)
        {
            EncounterExecution e = _encounterExecutionRepository.GetById(executionId);

            if (e.TouristId != touristId)
            {
                return Result.Fail(FailureCode.Forbidden).WithError("You are not allowed to finish this encounter.");
            }

            EncounterReadDto encounter = _encounterService.GetById(e.EncounterId).Value;

            if (!((TypeEncounter)Enum.Parse(typeof(TypeEncounter), encounter.TypeEncounter) == TypeEncounter.Misc))
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Encounter is not a misc encounter.");
            }

            e.Complete();
            return Update(e);
        }

        public Result<HiddenEncounterExecutionDto> StartHiddenExecution(long encounterId, int touristId)
        {

            EncounterReadDto encounter = _encounterService.GetById(encounterId).Value;

            if (!(encounter is HiddenEncounterReadDto))
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Encounter is not a hidden encounter.");
            }

            HiddenEncounterExecution execution = new HiddenEncounterExecution(encounterId, touristId);
            HiddenEncounterExecutionDto executionDto = mapper.Map<HiddenEncounterExecutionDto>(execution);



            return Create(executionDto).Map(result => (HiddenEncounterExecutionDto)result);
        }

        public Result<HiddenEncounterExecutionDto> ProcessHiddenExecution(long executionId, int touristId, LocationDto currentPosition)
        {
            EncounterExecution execution = _encounterExecutionRepository.GetById(executionId);

            if (execution.TouristId != touristId)
            {
                return Result.Fail(FailureCode.Forbidden).WithError("You are not allowed to finish this encounter.");
            }

            EncounterReadDto encounter = _encounterService.GetById(execution.EncounterId).Value;

            if(!(encounter is HiddenEncounterReadDto))
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Encounter is not a hidden encounter.");
            }

            HiddenEncounterReadDto hiddenEncounter = (HiddenEncounterReadDto)encounter;

            HiddenEncounterExecution hiddenExecution = (HiddenEncounterExecution)execution;
            
            hiddenExecution.AddTimePassed(currentPosition.Latitude, currentPosition.Longitude, hiddenEncounter.HiddenLocation.Latitude, hiddenEncounter.HiddenLocation.Longitude);

            if(hiddenExecution.Did30SecondsPass()) 
                hiddenExecution.Complete();

            var result = Update(hiddenExecution);

            return mapper.Map<HiddenEncounterExecutionDto>(result.Value);
        }


        public Result<EncounterExecutionDto> GetById(long id)
        {
            var result = _encounterExecutionRepository.GetById(id);
            return MapToDto(result);
        }


    }
}
