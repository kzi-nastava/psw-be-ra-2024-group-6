using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Payments.Core.UseCases.Shopping;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Shopping
{
    [Route("api/shopping/sale")]
    public class SaleController : BaseApiController
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public ActionResult<SaleDto> Create([FromBody] SaleDto comment)
        {
            var result = _saleService.Create(comment);

            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<SaleDto> Update([FromBody] SaleDto comment)
        {
            var result = _saleService.Update(comment);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<SaleDto> Get([FromQuery] int id)
        {
            return CreateResponse(_saleService.Get(id));
        }

        [HttpGet]
        public ActionResult<PagedResult<SaleDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _saleService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<SaleDto> Delete([FromQuery] int id)
        {
            return CreateResponse(_saleService.Delete(id));
        }
    }
}
