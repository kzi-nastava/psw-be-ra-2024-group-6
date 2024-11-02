using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration;

public class ObjectService : CrudService<ObjectDto, Domain.Tours.Object> , IObjectService
{
    private readonly IObjectRepository _objectRepository;
    private readonly IMapper _mapper;

    public ObjectService(ICrudRepository<Domain.Tours.Object> crudRepository, IMapper mapper, IObjectRepository objectRepository) : base(crudRepository, mapper)
    {
        _mapper = mapper;
        _objectRepository = objectRepository;
    }

    public Result<ObjectDto> Create(ObjectCreateDto objectCreateDto)
    {
        return MapToDto(CrudRepository.Create(_mapper.Map<Domain.Tours.Object>(objectCreateDto)));
    }

    public Result<List<ObjectReadDto>> GetByTourId(long tourId)
    {
        try
        {
            List<ObjectReadDto> tourObjects = _objectRepository.GetAllByTourId(tourId).Select(x => _mapper.Map<ObjectReadDto>(x)).ToList();
            return tourObjects;
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

