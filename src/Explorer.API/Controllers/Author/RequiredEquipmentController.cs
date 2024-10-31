using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/required-equipment")]
    public class RequiredEquipmentController : BaseApiController
    {
        private readonly IRequiredEquipmentService _requiredEquipmentService;
        private readonly IEquipmentService _equipmentService;

        public RequiredEquipmentController(IRequiredEquipmentService requiredEquipmentService, IEquipmentService equipmentService)
        {
            _requiredEquipmentService = requiredEquipmentService;
            _equipmentService = equipmentService;
        }

        [HttpGet("all-equipments")]
        public ActionResult GetAllEquipments()
        {
            var result = _equipmentService.GetPaged(0, 0);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<ICollection<RequiredEquipmentDto>> GetAllByTourId([FromQuery] int tourId)
        {
            var result = _requiredEquipmentService.GetAllByTourId(tourId);
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
