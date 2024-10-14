using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist;

[Authorize(Policy = "touristPolicy")]
[Route("api/administration/tourist-equipment-manager")]
public class TouristEquipmentManagerController : BaseApiController
{
    private readonly ITouristEquipmentManagerService _touristEquipmentManagerService;

    public TouristEquipmentManagerController(ITouristEquipmentManagerService touristEquipmentManagerService)
    {
        _touristEquipmentManagerService = touristEquipmentManagerService;
    }

    [HttpGet]
    public ActionResult<List<TouristEquipmentManagerDto>> GetTouristEquipment([FromQuery] int touristId)
    {
        var result = _touristEquipmentManagerService.GetTouristEquipment(touristId);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<TouristEquipmentManagerDto> Create([FromBody] TouristEquipmentManagerDto equipment)
    {
        var result = _touristEquipmentManagerService.Create(equipment);
        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(Create), new { id = result.Value.Id }, result.Value);
        }

        return BadRequest(result.Errors.FirstOrDefault()?.Message);
    }

    [HttpDelete("{touristId:int}/{equipmentId:int}")]
    public ActionResult Delete(int touristId, int equipmentId)
    {
        var result = _touristEquipmentManagerService.Delete(touristId, equipmentId);
        if (result.IsSuccess)
        {
            return Ok(result.Successes.FirstOrDefault()?.Message ?? "Deleted successfully");
        }

        return BadRequest(result.Errors.FirstOrDefault()?.Message);
    }
}
