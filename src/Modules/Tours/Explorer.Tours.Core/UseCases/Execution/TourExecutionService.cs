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
        private readonly IPurchaseTokenRepository _purchaseTokenRepository;
        public TourExecutionService(ITourExecutionRepository tourExecutionRepository, IPurchaseTokenRepository _tokenRepository, IMapper mapper) : base(mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
            _purchaseTokenRepository = _tokenRepository;

        }

        public Result<TourExecutionDto> Create(TourExecutionDto tourExecution)
        {
            try
            {
                var existingTourExecution = _tourExecutionRepository.GetByTourIdAndTouristId(tourExecution.TourId, tourExecution.TouristId);
                if (existingTourExecution != null)
                {
                    return Result.Fail(FailureCode.Forbidden).WithError("Tourist already started this tour.");
                }


                if (!checkIfTouristBoughtTour(tourExecution))
                {
                    return Result.Fail(FailureCode.Forbidden).WithError("Tourist did not buy this tour.");
                }

                var result = _tourExecutionRepository.Create(MapToDomain(tourExecution));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }


        private bool checkIfTouristBoughtTour(TourExecutionDto tourExecution)
        {
            return _purchaseTokenRepository.GetByUserAndTour(tourExecution.TouristId, tourExecution.TourId) != null;
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

        public Result<ICollection<TourExecutionDto>> GetByTouristId(int touristId)
        {
            try
            {
                var tourExecutions = _tourExecutionRepository.GetByTouristId(touristId);
                return tourExecutions.Select(tourExecution => MapToDto(tourExecution)).ToList();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
