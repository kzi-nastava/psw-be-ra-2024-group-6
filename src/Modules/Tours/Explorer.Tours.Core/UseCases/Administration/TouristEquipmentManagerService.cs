using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TouristEquipmentManagerService : BaseService<TouristEquipmentManagerDto, TouristEquipmentManager>, ITouristEquipmentManagerService
{
    private readonly ITouristEquipmentManagerRepository _touristEquipmentManagerRepository;

    public TouristEquipmentManagerService(IMapper mapper, ITouristEquipmentManagerRepository touristEquipmentManagerRepository) : base(mapper)
    {
        _touristEquipmentManagerRepository = touristEquipmentManagerRepository;
    }

    public Result<List<TouristEquipmentManagerDto>> GetTouristEquipment(int touristId)
    {

        return MapToDto(_touristEquipmentManagerRepository.GetAllByTouristId(touristId));
    }
    public Result<TouristEquipmentManagerDto> Create(TouristEquipmentManagerDto equipmentDto)
    {
        try
        {
            var equipmentEntity = MapToDomain(equipmentDto);

            _touristEquipmentManagerRepository.Create(equipmentEntity);


            var createdDto = MapToDto(equipmentEntity);
            return createdDto;
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
    public Result Delete(int touristId, int equipmentId)
    {
        try
        {
            _touristEquipmentManagerRepository.Delete(touristId, equipmentId);

            return Result.Ok();
        }
        catch(ArgumentNullException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }

    }


}
