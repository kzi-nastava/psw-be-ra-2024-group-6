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
        public Result<PagedResult<RequiredEquipmentDto>> GetPaged(int page, int pageSize);
        public Result<RequiredEquipmentDto> Get(int id);
        public Result<RequiredEquipmentDto> Create(RequiredEquipmentDto requiredEquipment);
        public Result<RequiredEquipmentDto> Update(RequiredEquipmentDto requiredEquipment);
        public Result Delete(int id);
    }
}
