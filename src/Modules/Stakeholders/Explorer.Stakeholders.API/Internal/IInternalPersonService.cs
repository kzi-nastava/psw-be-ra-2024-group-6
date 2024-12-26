using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalPersonService
    {
        Result<PersonDto> GetByUserId(int id);
    }
}
