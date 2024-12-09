using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterExecutionService
    {
        Result<EncounterExecutionDto> Create(EncounterExecutionDto encounterDto);

        Result<EncounterExecutionDto> Update(EncounterExecutionDto encounterDto);

        Result<List<EncounterExecutionDto>> GetPaged();

        Result Delete(long id);
        //Result<EncounterExecutionDto> GetEncounterExecutionByTouristId(int id);
        public Result<EncounterExecutionDto> StartMiscExecution(long encounterId, int touristId);
        public Result<EncounterExecutionDto> GetById(long id);
        public Result<EncounterExecutionDto> FinishMiscExecution(long executionId, int touristId);

        public Result<HiddenEncounterExecutionDto> StartHiddenExecution(long encounterId, int touristId);

        public Result<HiddenEncounterExecutionDto> ProcessHiddenExecution(long executionId, int touristId, LocationDto currentPosition);
        public Result<EncounterExecutionDto> StartSocialEncounterExecution(long encounterId, int touristId);
        //public Result<int> UpdateSocialExecutionLocation(long encounterExecutionId, LocationDto location, int userId);

    }
}
