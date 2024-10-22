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

        Result<List<CheckpointDto>> ICheckpointService.GetByTourId(int tourId)
        {
            try
            {
                var el = MapToDto(_checkpointRepository.GetByTourId(tourId));
                return el;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
