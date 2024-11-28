using Explorer.Stakeholders.Infrastructure.Authentication;
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

        [HttpGet("author")]
        public ActionResult<List<BundleDto>> GetAllAuthor()
        {
            long userId = User.UserId();
            var result = _bundleService.GetAllByUserId(userId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<BundleDto> Create([FromBody] BundleDto bundle)
        {
            var result = _bundleService.Create(bundle);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<BundleDto> Update([FromBody] BundleDto bundle)
        {
            var result = _bundleService.Update(bundle);
            return CreateResponse(result);

        }
        
 


    }
}
