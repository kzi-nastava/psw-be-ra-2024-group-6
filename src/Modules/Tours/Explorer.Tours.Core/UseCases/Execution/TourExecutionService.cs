using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class TourExecutionService : BaseService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        private readonly ITourExecutionRepository _tourExecutionRepository;
        public TourExecutionService(ITourExecutionRepository tourExecutionRepository, IMapper mapper) : base(mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
        }

        public Result<TourExecutionDto> Create(TourExecutionDto tourExecution)
        {
            try
            {
                var result = _tourExecutionRepository.Create(MapToDomain(tourExecution));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourExecutionDto> FinalizeTourExecution(int tourExecutionId, string status, int touristId)
        {
            try
            {
                var tour = _tourExecutionRepository.GetByIdAndTouristId(tourExecutionId, touristId);
                tour.Finalize(status);
                _tourExecutionRepository.Update(tour);
                return MapToDto(tour);
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
