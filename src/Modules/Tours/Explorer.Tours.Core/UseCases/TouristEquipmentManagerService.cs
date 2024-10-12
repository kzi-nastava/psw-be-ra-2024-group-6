using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases;

public class TouristEquipmentManagerService : CrudService<TouristEquipmentManagerDto, TouristEquipmentManager>, ITouristEquipmentManagerService
{
    public TouristEquipmentManagerService(ICrudRepository<TouristEquipmentManager> repository, IMapper mapper) : base(repository, mapper) { }

}
