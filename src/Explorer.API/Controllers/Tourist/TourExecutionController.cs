using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.Execution;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-execution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;
        private readonly ICheckpointService _checkpointService;
        private readonly IObjectService _objectService;
        private readonly ITourService _tourService;
        private readonly IPurchaseTokenService _purchaseTokenService;

        public TourExecutionController(ITourExecutionService tourExecutionService,ICheckpointService checkpointService,ITourService tourService,IPurchaseTokenService purchaseTokenService,IObjectService objectService)
        {
            _tourExecutionService = tourExecutionService;
            _checkpointService = checkpointService;
            _tourService = tourService;
            _purchaseTokenService = purchaseTokenService;
            _objectService = objectService;
        }

        [HttpPost]
        public ActionResult<TourExecutionDto> CreateTourExecution([FromBody] TourExecutionDto tourExecution)
        {
            tourExecution.TouristId = User.UserId();
            var result = _tourExecutionService.Create(tourExecution);
            // Uncomment if it's made that you can purchase the same tour multiple times
            /*
            if (result.IsSuccess)
            {
                PurchaseTokenDto purchaseToken = _purchaseTokenService.GetByUserAndTour(tourExecution.TouristId, tourExecution.TourId).Value;

                if (purchaseToken != null)
                {
                    purchaseToken.isExpired = true;
                    _purchaseTokenService.Update(purchaseToken);
                }
            }*/

            return CreateResponse(result);
        }
        [HttpGet]
        public ActionResult<TourExecutionDto> GetById([FromQuery] int tourExecutionId)
        {
            var result = _tourExecutionService.GetById(tourExecutionId);
            return CreateResponse(result);
        }
        [HttpGet("mostCompleted")]
        public ActionResult<TourExecutionDto> GetMostCompletedByTourId([FromQuery] int tourId)
        {
            var touristId = User.UserId();
            var result = _tourExecutionService.GetMostCompleted(touristId, tourId);
            return CreateResponse(result);
        }


        [HttpPut("complete/checkpoint")]
        public ActionResult<TourExecutionDto> CompleteCheckpoint([FromQuery] int tourExecutionId, [FromQuery] int checkpointId, [FromQuery] int checkpointNum)
        {
            //var touristId = User.UserId();
            var result = _tourExecutionService.CompleteCheckpoint(tourExecutionId, checkpointId, checkpointNum);
            return CreateResponse(result);
        }

        [HttpPut("finalize")]
        public ActionResult<TourExecutionDto> FinalizeTourExecution([FromQuery] int tourExecutionId, [FromQuery] string status)
        {
            var touristId = User.UserId();
            var result = _tourExecutionService.FinalizeTourExecution(tourExecutionId, status, touristId);
           
            return CreateResponse(result);
        }

        [HttpPut("update")]
        public ActionResult<TourExecutionDto> UpdateTourist([FromBody] TourExecutionDto tourExecution)
        {
            // var touristId = User.UserId();
            var result = _tourExecutionService.UpdateTourist(tourExecution);
            return CreateResponse(result);
        }

        [HttpGet("checkpoints/{tourId:long}")]
        public ActionResult<List<CheckpointReadDto>> GetCheckpointsByTourId(long tourId)
        {
            var result = _checkpointService.GetByTourId(tourId);
            return CreateResponse(result);
        }

        [HttpGet("objects/{tourId:long}")]
        public ActionResult<List<ObjectReadDto>> GetObjectsByTourId(long tourId)
        {
            var result = _objectService.GetByTourId(tourId);
            return CreateResponse(result);
        }

        [HttpGet("options")]
        public ActionResult<PagedResult<TourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] int userId)
        {
            var toursResult = _tourService.GetPaged(page, pageSize); //returns all tours
            var purchaseTokensResult = _purchaseTokenService.GetByUserId(userId);   //returns all userId purchases

            var allTours = toursResult.Value;

            var purchasedTourIds = purchaseTokensResult.Value.Where(pt => !pt.isExpired).Select(pt => pt.TourId).ToList();

            var filteredTours = new List<TourDto>();

            foreach (var tour in allTours.Results)
            {
                if (purchasedTourIds.Contains((int)tour.Id))
                {
                    filteredTours.Add(tour);
                }
            }
            var a = new PagedResult<TourDto>(filteredTours, filteredTours.Count);


            return CreateResponse(Result.Ok(a));
        }

        [HttpGet("get-by-tourist")]
        public ActionResult<PagedResult<TourExecutionDto>> GetAll()
        {
            var touristId = User.UserId();
            var result = _tourExecutionService.GetByTouristId(touristId);
            return CreateResponse(result);
        }

        [HttpPut("update-tourist-location")]
        public ActionResult<TourExecutionDto> Update([FromBody] TourExecutionDto tourExecution)
        {
            tourExecution.TouristId = User.UserId();
            var result = _tourExecutionService.Update(tourExecution);
            return CreateResponse(result);
        }

        [HttpGet("secret/{checkpointId:long}")]
        public ActionResult<CheckpointDto> GetSecret(int checkpointId)
        {
            var res = _checkpointService.Get(checkpointId);
            return CreateResponse(res);
        }



    }
}
