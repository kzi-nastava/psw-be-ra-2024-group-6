using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration;

public class ObjectService : CrudService<ObjectDto, Domain.Object> , IObjectService
{
    private readonly IObjectRepository _objectRepository;
    private readonly IMapper _mapper;

    public ObjectService(ICrudRepository<Domain.Object> crudRepository, IMapper mapper, IObjectRepository objectRepository) : base(crudRepository, mapper)
    {
        _mapper = mapper;
        _objectRepository = objectRepository;
    }

    Result<List<ObjectReadDto>> IObjectService.GetByTourId(long tourId)
    {
        try
        {
            List<ObjectReadDto> el = _objectRepository.GetAllByTourId(tourId).Select(x => _mapper.Map<ObjectReadDto>(x)).ToList();
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

