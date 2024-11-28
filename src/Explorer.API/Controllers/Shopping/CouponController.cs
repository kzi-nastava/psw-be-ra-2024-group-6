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
        public ActionResult<CouponDto> Get(long id)
        {
            var result = _couponService.GetById(id);
            return CreateResponse(result);
        }

        [HttpGet("author/")]
        public ActionResult<List<CouponDto>> GetCouponsByAuthor()
        {
            var userId = User.UserId();
            var result = _couponService.GetAllByAuthorId(userId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CouponDto> Create([FromBody] CouponDto couponDto)
        {
            var userId = User.UserId();
            couponDto.AuthorId = userId;
            var result = _couponService.Create(couponDto);
            return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<CouponDto> Update([FromBody] CouponDto couponDto)
        {
            var userId = User.UserId();
            var result = _couponService.Update(couponDto, userId);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            var userId = User.UserId(); 

            var result = _couponService.Delete(id, userId);

            return CreateResponse(result);
        }
    }
}