using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/bundles")]
    public class BundleController : BaseApiController
    {

        private readonly IBundleService _bundleService;

        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService;
        }

        [HttpGet]
        public ActionResult<List<BundleDto>> GetAll()
        {
            var result = _bundleService.GetAll();
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<BundleDto> Create([FromBody] BundleDto bundle)
        {
            var result = _bundleService.Create(bundle);
            return CreateResponse(result);
        }



    }
}
