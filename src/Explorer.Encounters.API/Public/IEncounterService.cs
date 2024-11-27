using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        Result<EncounterCreateDto> Create(EncounterCreateDto encounterDto);

        Result<EncounterCreateDto> Update(EncounterCreateDto encounterDto);

        Result<List<EncounterReadDto>> GetPaged();

        Result<List<EncounterReadDto>> GetAllActiveEncounters();
        Result Delete(long id);
    }
}
