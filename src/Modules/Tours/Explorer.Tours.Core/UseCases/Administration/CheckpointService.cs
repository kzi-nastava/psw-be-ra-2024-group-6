using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System.Diagnostics;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class CheckpointService : CrudService<CheckpointDto, Checkpoint>,ICheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IMapper mapper;
        public CheckpointService(ICrudRepository<Checkpoint> crudRepository,ICheckpointRepository checkpointRepository, IMapper mapper) : base(crudRepository, mapper)
        {

            _checkpointRepository = checkpointRepository;
            this.mapper = mapper;
        }


        Result<List<CheckpointReadDto>> ICheckpointService.GetByTourId(long tourId)

        {
            List<CheckpointReadDto> el = _checkpointRepository.GetByTourId(tourId).Select(mapper.Map<CheckpointReadDto>).ToList();
            return el;
        }
        public CheckpointDto Create(CheckpointCreateDto checkpointCreateDto)
        {
            return MapToDto(CrudRepository.Create(mapper.Map<Checkpoint>(checkpointCreateDto)));
        }
    }
}
