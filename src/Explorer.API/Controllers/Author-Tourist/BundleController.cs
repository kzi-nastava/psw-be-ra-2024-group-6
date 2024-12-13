using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author_Tourist
{
    [Authorize(Policy = "touristOrAuthorPolicy")]
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
            long userId = User.UserId();
            var result = _bundleService.GetAll();
            return CreateResponse(result);
        }


        [HttpGet("author")]
        public ActionResult<List<BundleDto>> GetAllAuthor()
        {
            long userId = User.UserId();
            var result = _bundleService.GetAllByUserId(userId);
            return CreateResponse(result);
        }

        [HttpPost("buy")]
        public ActionResult<BundleDto> Buy([FromBody] BundleDto bundle)
        {
            var userId = User.UserId();
            var result = _bundleService.Buy(bundle,userId);
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
