using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IRequiredEquipmentService
    {
        public Result<List<RequiredEquipmentDto>> GetAllByTourId(int tourId);
        public Result<RequiredEquipmentDto> Create(RequiredEquipmentDto requiredEquipment);
        public Result Delete(int id);
    }
}
