using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class InternalPersonService : IInternalPersonService
    {
        private readonly IPersonService _personService;
        public InternalPersonService(IPersonService personService)
        {
            _personService = personService;
        }

        public Result<PersonDto> GetByUserId(int id)
        {
            return _personService.GetByUserId(id);
        }
    }
}
