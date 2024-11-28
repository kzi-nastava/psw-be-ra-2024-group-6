using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Shopping
{
    [Route("api/shopping/sale")]
    public class CommentController : BaseApiController
    {
        private readonly ISaleService _commentService;

        public CommentController(ISaleService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public ActionResult<SaleDto> Create([FromBody] SaleDto comment)
        {
            var result = _commentService.Create(comment);

            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<SaleDto> Update([FromBody] SaleDto comment)
        {
            var result = _commentService.Update(comment);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<SaleDto> Get([FromQuery] int id)
        {
            return CreateResponse(_commentService.Get(id));
        }

        [HttpGet]
        public ActionResult<PagedResult<SaleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _commentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<SaleDto> Delete([FromQuery] int id)
        {
            return CreateResponse(_commentService.Delete(id));
        }
    }
}
