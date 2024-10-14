using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class RequiredEquipmentService : CrudService<RequiredEquipmentDto, RequiredEquipment>, IRequiredEquipmentService
    {
        public RequiredEquipmentService(ICrudRepository<RequiredEquipment> crudRepository, IMapper mapper) : base(crudRepository, mapper) {}
    }
}
