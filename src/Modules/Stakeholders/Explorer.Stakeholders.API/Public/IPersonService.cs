using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;


namespace Explorer.Stakeholders.API.Public;

public interface IPersonService
{
    public Result<PersonDto> GetByUserId(int id);

    public Result<PersonDto> Update(PersonDto person);
        
}
