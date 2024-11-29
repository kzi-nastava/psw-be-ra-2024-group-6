using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Shopping
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shop")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPut("removeItem/{itemId:int}")]
        public ActionResult<ShoppingCartDto> RemoveItem(int itemId)
        {
            var userId = User.UserId();

            var result = _shoppingCartService.RemoveItem(userId, itemId);
            return CreateResponse(result);
        }

        [HttpPost("shoppingItem/{tourId:int}")]
        public ActionResult<ShoppingCartDto> AddItem(int tourId)
        {
            var userId = User.UserId();

            var result = _shoppingCartService.AddItem(userId, tourId);
            return CreateResponse(result);
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
