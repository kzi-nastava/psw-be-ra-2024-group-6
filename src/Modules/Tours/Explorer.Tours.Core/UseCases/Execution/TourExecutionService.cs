using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
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
        private readonly IInternalPurchaseTokenService _purchaseTokenService;
        public TourExecutionService(ITourExecutionRepository tourExecutionRepository, IInternalPurchaseTokenService _token, IMapper mapper) : base(mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
            _purchaseTokenService = _token;

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
                return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid argument exception");
            }
        }


        private bool checkIfTouristBoughtTour(TourExecutionDto tourExecution)
        {
            return _purchaseTokenService.GetByUserAndTour(tourExecution.TouristId, tourExecution.TourId).Value != null;
        }

        public Result<TourExecutionDto> FinalizeTourExecution(int tourExecutionId, string status, int touristId)
        {
            try
            {
                var tourExecution = _tourExecutionRepository.GetByIdAndTouristId(tourExecutionId, touristId);
                tourExecution.Finalize(status);
                var updatedTourExecution = _tourExecutionRepository.Update(tourExecution);
                return MapToDto(updatedTourExecution);
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
        public Result<TourExecutionDto> GetMostCompleted(int touristId, int tourId)
        {
            try
            {
                var tourExecutionsByUser = _tourExecutionRepository.GetByTouristId(touristId);
                var specificTourExecutions = tourExecutionsByUser.Where(t => t.TourId == tourId);

                var highestCompletionTourExecution = specificTourExecutions.FirstOrDefault();

                if (highestCompletionTourExecution != null)
                {
                    foreach (TourExecution t in specificTourExecutions)
                    {
                        if (t.Completion > highestCompletionTourExecution.Completion)
                        {
                            highestCompletionTourExecution = t;
                        }
                    }
                    return MapToDto(highestCompletionTourExecution);
                }
                return Result.Fail(FailureCode.NotFound).WithError("This user has no tour executions");
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

        }
        public Result<TourExecutionDto> GetById(int id)
        {
            try
            {
                var tourExecution = _tourExecutionRepository.Get(id);
                return Result.Ok(MapToDto(tourExecution));
            }
            catch(Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourExecutionDto> CompleteCheckpoint(int tourExecutionId,int checkpointId,int checkpointNum)
        {
            try
            {
                var tourExecution = _tourExecutionRepository.Get(tourExecutionId);
                tourExecution.CompleteCheckpoint(checkpointId, checkpointNum);
                var result = _tourExecutionRepository.Update(tourExecution);
                return MapToDto(result);

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

        public Result<TourExecutionDto> UpdateTourist(TourExecutionDto tourExecution)
        {
            try
            {
                var tourExecutionNew = _tourExecutionRepository.Get(tourExecution.Id);
                tourExecutionNew.SetLastActivity(tourExecution.Longitude,tourExecution.Latitude);
                var result = _tourExecutionRepository.Update(tourExecutionNew);
                return MapToDto(result);

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

        public Result<TourExecutionDto> Update(TourExecutionDto tourExecution)
        {
            try
            {
                var result = _tourExecutionRepository.Update(MapToDomain(tourExecution));
                return MapToDto(result);

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message); 
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.Forbidden).WithError(e.Message);
            }
        }
    }
}
