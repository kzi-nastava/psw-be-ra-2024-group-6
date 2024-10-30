using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Shopping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Microsoft.AspNetCore.Components.Route("api/tourist/shop")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("user/")]
        public ActionResult<ShoppingCartDto> GetShoppingCartByUserId()
        {
            var userId = User.UserId();
            var result = _shoppingCartService.GetByUserId(userId);


            if (result == null)
            {
                return new ObjectResult(new { Message = "User does not have a cart.", StatusCode = 400 }) { StatusCode = 400 };
            }

            return CreateResponse(result);
        }

        [HttpPut("removeItem/{shoppingCartId:int}/{itemId:int}")]
        public ActionResult<ShoppingCartDto> RemoveItem(int shoppingCartId, int itemId)
        {
            var result = _shoppingCartService.RemoveItem(shoppingCartId, itemId);
            return CreateResponse(result);
        }

        [HttpPost("shoppingItem/{shoppingCartId:int}/{tourId:int}")]
        public ActionResult<ShoppingCartDto> AddItem(int shoppingCartId, int tourId)
        {
            var userId = User.UserId();
            var result = _shoppingCartService.AddItem(shoppingCartId, tourId, userId);
            return CreateResponse(result);
        }




    }
}
