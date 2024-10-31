using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public;

public interface ITouristEquipmentManagerService
{
    Result<List<TouristEquipmentManagerDto>> GetAllTouristEquipments(int touristId);
    Result<TouristEquipmentManagerDto> Create(TouristEquipmentManagerDto equipment);
    Result Delete(int touristId, int equipmentId);
}
