using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace Explorer.API.Controllers.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shop")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICouponService _couponService;
        private readonly ITourService _tourService;

        public ShoppingCartController(IShoppingCartService shoppingCartService,ICouponService couponService,ITourService tourService)
        {
            _shoppingCartService = shoppingCartService;
            _couponService = couponService;
            this._tourService = tourService;

        }

        [HttpPut("removeItem/{resourceId:int}")]
        public ActionResult<ShoppingCartDto> RemoveItem(int resourceId)
        {
            var userId = User.UserId();

            var result = _shoppingCartService.RemoveItem(userId, resourceId);
            return CreateResponse(result);
        }

        [HttpPost("shoppingItem/{resourceId:long}/{resourceTypeId:long}")]
        public ActionResult<ShoppingCartDto> AddItem(int resourceId, int resourceTypeId)
        {
            var userId = User.UserId();

            var result = _shoppingCartService.AddItem(userId, resourceId, resourceTypeId);
            return CreateResponse(result);
        }

        [HttpGet("coupon/{code}")]
        public ActionResult<CouponDto> CheckCoupon(string code)
        {
            var res = _couponService.GetByCode(code);
            if (res == null)
            {
                return null;
            }

            var coupon = res.Value;

            if(coupon.Used)
            {
                return null;
            }
            
            var userId = User.UserId();

            // Retrieve the shopping cart
            var shop = _shoppingCartService.GetByUserId(userId).Value;
            if (shop == null)
            {
                return BadRequest("Shopping cart not found.");
            }

            var sc = shop.OrderItems;
            var totalPrice = 0;
            bool couponApplied = false;
            bool first = false;
            OrderItemDto max = new OrderItemDto();

            foreach (var item in sc)
            {
                if (!first)
                {
                    max = item;
                    first = true;
                }

                if (item.Price > max.Price)
                    max = item;

                if (coupon.DiscountPercentage > 0)
                {
          
                    if (coupon.TourId == item.Product.ResourceId && !couponApplied)
                    {
                        item.Product.Price = item.Product.Price * (1 - coupon.DiscountPercentage / 100);
                        item.Price = item.Product.Price;
                        couponApplied = true;
                        coupon.Used = true;
                    }

                    
                }
            }

            foreach (var item in sc)
            {

                if (coupon.DiscountPercentage > 0)
                {
                    var coupA = coupon.AuthorId;
                    var currA = _tourService.GetById((long)item.Product.ResourceId).Value.AuthorId;
                    if (coupon.TourId == null && !couponApplied && item == max && currA == coupA)
                    {
                        item.Product.Price = item.Product.Price * (1 - coupon.DiscountPercentage / 100);
                        item.Price = item.Product.Price;
                        couponApplied = true;
                        coupon.Used = true;
                    }

                    
                }
            }

            foreach (var item in sc)
            {
                    totalPrice += (int)item.Price;
            }


            shop.Price = totalPrice;

            
            
            _shoppingCartService.Update(shop);
            _couponService.Update(coupon,userId);

            return CreateResponse(res);
        }


        [HttpPost("checkout")]
        public ActionResult<CheckoutResultDto> CheckoutCart()
        {
            var userId = User.UserId();

            var result = _shoppingCartService.Checkout(userId);

            if (result.IsSuccess)
            {
                var checkout = result.Value;
                var tokensResult = Result.Ok(checkout.PurchaseTokens);
                return CreateResponse(tokensResult);
            }

            return CreateResponse(result);
        }

        [HttpGet("user")]
        public ActionResult<ShoppingCartDto> GetShoppingCartForUser()
        {
            var userId = User.UserId();
            var result = _shoppingCartService.GetByUserId(userId);

            if (result == null)
            {
                return new ObjectResult(new { Message = "User does not have a cart.", StatusCode = 400 }) { StatusCode = 400 };
            }

            return CreateResponse(result);
        }
    }
}
