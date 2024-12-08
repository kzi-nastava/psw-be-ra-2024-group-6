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
using Explorer.Stakeholders.API.Internal;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterService : CrudService<EncounterCreateDto,Encounter>, IEncounterService
    {

        private readonly IEncounterRepository _encounterRepository;
        private readonly IMapper mapper;
        private readonly ITouristRankService _touristRankService;
        private readonly IInternalUserService _internalUserService;
    
        public EncounterService(IMapper mapper,IEncounterRepository encounterRepository, ICrudRepository<Encounter> repository, ITouristRankService touristRankService, IInternalUserService internalUserService) :base(repository, mapper) 
        {
            _encounterRepository = encounterRepository;
            this.mapper = mapper;
            _touristRankService = touristRankService;
            _internalUserService = internalUserService;
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



        public Result<EncounterByTouristReadDto> CreateByTourist(EncounterCreateDto encounterDto, int creatorId)
        {
            try
            {
                if (!_touristRankService.CanCreateEncounter(creatorId).Value)
                    return Result.Fail(FailureCode.Forbidden).WithError("Tourist is not eligible to create encounter.");
                var encounter = mapper.Map<Encounter>(encounterDto);
                encounter.CreatorId = creatorId;
                encounter.Status = Status.Draft;
                var result = _encounterRepository.Create(encounter);
                return mapper.Map<EncounterByTouristReadDto>(result);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<EncounterByTouristReadDto> CreateSocialEncounterByTourist(SocialEncounterCreateDto encounterDto, int creatorId)
        {
            try
            {
                if (!_touristRankService.CanCreateEncounter(creatorId).Value)
                    return Result.Fail(FailureCode.Forbidden).WithError("Tourist is not eligible to create encounter.");
                var encounter = mapper.Map<SocialEncounter>(encounterDto);
                encounter.CreatorId = creatorId;
                encounter.Status = Status.Draft;
                var result = _encounterRepository.Create(encounter);
                return mapper.Map<EncounterByTouristReadDto>(result);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<EncounterReadDto>> GetAllActiveTouristsEncounters()
        {
            try
            {
                var activeEncounters = _encounterRepository.GetAllActiveEncounters();
                var encounters = new List<EncounterReadDto>();
                foreach (var encounter in activeEncounters)
                {
                    if (_internalUserService.IsUserAuthor(encounter.CreatorId))
                        continue;
                    encounters.Add(mapper.Map<EncounterReadDto>(encounter));
                }

                return encounters;
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public bool IsUserInSocialEncounterRange(long encounterId, LocationDto location)
        {
            try
            {
                var socialEncounter = _encounterRepository.GetSocialEncounterById(encounterId);
                return socialEncounter.IsUserInRadius(mapper.Map<Location>(location));
            }
            catch (KeyNotFoundException e)
            {
                return false;
            }
        }


        public Result Delete(long id)
        {
            try
            {

                _encounterRepository.Delete(id);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.NotFound)
                    .WithError($"Encounter with ID {id} not found.");
            }
        }

        public Result<List<EncounterReadDto>> GetPaged()
        {
            
                return mapper.Map<List<EncounterReadDto>>(_encounterRepository.GetPagedEncounters());
            
            
        }

        public Result<List<EncounterReadDto>> GetAllActiveEncounters()
        {
            
            return mapper.Map<List<EncounterReadDto>> (_encounterRepository.GetAllActiveEncounters());
            
        }
        public Result<List<SocialEncounterReadDto>> GetAllActiveSocialEncounters()
        {
            var activeEncounters = _encounterRepository.GetAllActiveEncounters()
                .Cast<SocialEncounter>()
                .ToList();

            return mapper.Map<List<SocialEncounterReadDto>>(activeEncounters);
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

        public Result<EncounterReadDto> GetById(long id)
        {
            var result = _encounterRepository.GetById(id);
            return mapper.Map<EncounterReadDto>(result);
        }
    }
}

