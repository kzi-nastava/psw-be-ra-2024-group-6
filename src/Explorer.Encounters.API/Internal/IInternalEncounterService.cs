using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Internal
{
    public interface IInternalEncounterService
    {
        public Result<EncounterCreateDto> Create(EncounterCreateDto encounterDto);
    }
}
