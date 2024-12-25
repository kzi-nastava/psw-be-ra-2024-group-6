using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/public-checkpoints")]
    public class PublicCheckpointController : BaseApiController
    {
        private readonly ICheckpointService _checkpointService;

        public PublicCheckpointController(ICheckpointService checkpointService)
        {
            _checkpointService = checkpointService;
        }

        [HttpGet]
        public ActionResult<List<CheckpointReadDto>> GetAll()
        {
            var result = _checkpointService.GetPendingPublicCheckpoints();
            return CreateResponse(result);
        }

        [HttpPost("{checkpointId:long}/approve")]
        public ActionResult<CheckpointReadDto> ApproveCheckpoint(long checkpointId)
        {
            var userId = User.UserId();
            var result = _checkpointService.ApproveCheckpointRequest(checkpointId, userId);
            return CreateResponse(result);
        }


        [HttpPost("{checkpointId:long}/reject")]
        public ActionResult<CheckpointReadDto> RejectCheckpoint(long checkpointId, [FromBody] AdminCommentRejectDto rejectDto)
        {
            if (string.IsNullOrWhiteSpace(rejectDto.Comment))
                return BadRequest("Comment is required to reject a public checkpoint request.");

            var userId = User.UserId();

            var result = _checkpointService.RejectCheckpointRequest(checkpointId, rejectDto.Comment, userId);
            return CreateResponse(result);
        }
    }
}
