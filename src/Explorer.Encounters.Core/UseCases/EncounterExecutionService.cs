using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly IMapper mapper;

        public EncounterExecutionService(ICrudRepository<EncounterExecution> repository,IEncounterExecutionRepository encounterExecutionRepository, IMapper mapper) : base(repository, mapper)
        {
            _encounterExecutionRepository = encounterExecutionRepository;
            this.mapper = mapper;
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
            EncounterExecution execution = new EncounterExecution(encounterId, touristId);
            EncounterExecutionDto dto = MapToDto(execution);
            return Create(dto);
        }
        public Result<EncounterExecutionDto> FinishMiscExecution(long executionId)
        {
            EncounterExecution e = _encounterExecutionRepository.GetById(executionId);
            Debug.WriteLine("DABOOOOOOOOOOOOOOME" + e.Id + e.TouristId);
            e.Complete();
            return Update(e);
        }
        public Result<EncounterExecutionDto> GetById(long id)
        {
            var result = _encounterExecutionRepository.GetById(id);
            return Create(MapToDto(result));
        }


    }
}
