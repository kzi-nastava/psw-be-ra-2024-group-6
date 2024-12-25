using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Internal;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Encounters.Core.UseCases
{
    public class InternalEncounterService : CrudService<EncounterCreateDto, Encounter>, IInternalEncounterService
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IMapper mapper;

        public InternalEncounterService(IEncounterRepository encounterRepository, IMapper mapper, ICrudRepository<Encounter> repository) : base(repository, mapper)
        {
            this._encounterRepository = encounterRepository;
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
    }
}
