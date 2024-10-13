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

namespace Explorer.Tours.Core.UseCases.TouristEquipmentManagement;

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


            // Map the created entity back to DTO and return success
            var createdDto = MapToDto(equipmentEntity);
            return Result.Ok(equipmentDto).WithSuccess("Creation successful");
        }
        catch (Exception ex)
        {
            return Result.Fail<TouristEquipmentManagerDto>($"Error creating TouristEquipmentManager: {ex.Message}");
        }
    }
    public Result Delete(int touristId, int equipmentId)
    {
        try
        {
            var success = _touristEquipmentManagerRepository.Delete(touristId, equipmentId);

            if (!success)
            {
                return Result.Fail("TouristEquipmentManager not found.");
            }

            return Result.Ok().WithSuccess("Deletion successful.");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error deleting TouristEquipmentManager: {ex.Message}");
        }
    }


}
