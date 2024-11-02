using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/author/object")]
public class ObjectController : BaseApiController
{
    private readonly IObjectService _objectService;

    public ObjectController(IObjectService objectService)
    {
        _objectService = objectService;
    }

    [HttpGet]
    public ActionResult<PagedResult<ObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _objectService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<ObjectDto> Create([FromBody] ObjectDto obj)
    {
        var result = _objectService.Create(obj);
        return CreateResponse(result);
    }

    [HttpPost("{tourId:long}")]
    public ActionResult<List<ObjectReadDto>> GetByTourId(long tourId)
    {
        var result = _objectService.GetByTourId(tourId);
        return CreateResponse(result);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ObjectDto> Update([FromBody] ObjectDto obj)
    {
        var result = _objectService.Update(obj);
        return CreateResponse(result);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var result = _objectService.Delete(id);
        return CreateResponse(result);
    }

}
