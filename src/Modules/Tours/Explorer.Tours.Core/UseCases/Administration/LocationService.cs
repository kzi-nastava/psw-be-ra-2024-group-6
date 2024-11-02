using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class LocationService : CrudService<LocationDto,Location>, ILocationService
    {
        private readonly IMapper mapper;
        public LocationService(ICrudRepository<Location> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {

            this.mapper = mapper;
        }
        public LocationDto Create(LocationCreateDto location)
        {
            return MapToDto(CrudRepository.Create(mapper.Map<Location>(location)));
        }
    }
}
