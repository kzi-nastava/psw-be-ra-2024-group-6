using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Public.Administration;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Explorer.API.Controllers.Author;
using Explorer.Stakeholders.Infrastructure.Authentication;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "authorOrAdministratorOrTouristPolicy")]
    [Route("api/tourist/problem")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }
        [HttpGet]
        public ActionResult<PagedResult<ProblemDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var userId = User.UserId();
            var result = _problemService.GetAll(userId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ProblemDto> Create([FromBody] ProblemDto problem)
        {
            Debug.WriteLine(problem.Id);
            var result = _problemService.Create(problem);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProblemDto> Update([FromBody] ProblemDto problem)
        {
            var userId = User.UserId();
            var result = _problemService.Update(problem, userId);
            return CreateResponse(result);
        }
        [HttpPut("sendMessage")]
        public ActionResult<ProblemDto> SendMessage([FromBody] ProblemWithMessageDto problemWithMessageDto)
        {
            var userId = User.UserId();
            var result = _problemService.SendMessage(userId, problemWithMessageDto.Problem, problemWithMessageDto.Message);
            return CreateResponse(result);
           return Ok();
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            Debug.WriteLine(id);
            var result = _problemService.Delete(id);
            return CreateResponse(result);
        }
    }
}
