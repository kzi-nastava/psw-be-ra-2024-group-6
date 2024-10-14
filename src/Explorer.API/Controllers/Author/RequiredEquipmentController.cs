using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/requiredEquipment")]
    public class RequiredEquipmentController : BaseApiController
    {
        private readonly IRequiredEquipmentService _requiredEquipmentService;

        public RequiredEquipmentController(IRequiredEquipmentService requiredEquipmentService)
        {
            _requiredEquipmentService = requiredEquipmentService;
        }

        [HttpGet]
        public ActionResult<ICollection<RequiredEquipmentDto>> GetAllByTourId([FromQuery] int tourId)
        {
            var result = _requiredEquipmentService.GetAllByTour(tourId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<RequiredEquipmentDto> Create([FromBody] RequiredEquipmentDto requiredEquipment)
        {
            var result = _requiredEquipmentService.Create(requiredEquipment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _requiredEquipmentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
