using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Shopping
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/coupons")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet("{id:long}")]
        public ActionResult<CouponDTO> GetCoupon(long id)
        {
            var result = _couponService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet("author/{authorId:long}")]
        public ActionResult<List<CouponDTO>> GetCouponsByAuthor(long authorId)
        {
            var userId = User.UserId();
            var result = _couponService.GetAllByAuthorId(authorId, userId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CouponDTO> CreateCoupon([FromBody] CouponDTO couponDto)
        {
            var userId = User.UserId();
            couponDto.AuthorId = userId;
            var result = _couponService.Create(couponDto);
            return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<CouponDTO> UpdateCoupon([FromBody] CouponDTO couponDto)
        {
            var userId = User.UserId();
            var result = _couponService.Update(couponDto, userId);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public IActionResult DeleteCoupon(long id)
        {
            var userId = User.UserId();

            var result = _couponService.Delete(id, userId);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            return CreateResponse(result);
        }
    }
}