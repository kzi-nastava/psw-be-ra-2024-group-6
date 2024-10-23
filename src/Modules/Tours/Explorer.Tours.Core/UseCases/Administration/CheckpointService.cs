using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Public.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System.Diagnostics;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class CheckpointService : CrudService<CheckpointDto, Checkpoint>,ICheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IMapper mapper;
        public CheckpointService(ICrudRepository<Checkpoint> crudRepository,ICheckpointRepository checkpointRepository, IMapper mapper) : base(crudRepository, mapper){

            _checkpointRepository = checkpointRepository;
            this.mapper = mapper;
        }


        Result<List<CheckpointReadDto>> ICheckpointService.GetByTourId(long tourId)

        {
                List<CheckpointReadDto> el = _checkpointRepository.GetByTourId(tourId).Select(mapper.Map<CheckpointReadDto>).ToList();
                return el;
        }
    }
}
