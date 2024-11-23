using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Persons;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases;

public class InternalTourPersonService : BaseService<PersonDto, Person>, IInternalTourPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;


    public InternalTourPersonService(IPersonRepository personRepository, IMapper mapper) : base(mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public Result<PersonDto> GetByUserId(int id)
    {
        try
        {
            var el = MapToDto(_personRepository.GetByUserId(id));
            return el;
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
}
