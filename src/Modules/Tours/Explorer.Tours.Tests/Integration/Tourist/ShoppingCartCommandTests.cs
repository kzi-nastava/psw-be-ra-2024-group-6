using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Shopping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Explorer.Tours.Tests.Integration.Tourist
{
    [Collection("Sequential")]
    public class ShoppingCartCommandTests : BaseToursIntegrationTest
    {
        public ShoppingCartCommandTests(ToursTestFactory factory) : base(factory) { }

        private static ShoppingCartController CreateController(IServiceScope scope, string userId = "-1")
        {
            var controller = new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
            {
                ControllerContext = BuildLocalContext(userId)
            };
            return controller;
        }

        private static ControllerContext BuildLocalContext(string userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", userId), // Mock user ID claim
                new Claim(ClaimTypes.Role, "tourist") // Mock user role claim
            }, "mock"));

            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Theory]
        [InlineData(-1, -1, 1, "-1")]
        [InlineData(-1, -1, 1, "-1")]
        [InlineData(-1, -3, 0, "-1")]
        public  void AddsItem(int shoppingCartId, int tourId, int expectedOrderItemCount, string userId)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);

            var result = controller.AddItem(shoppingCartId, tourId);
            var shoppingCartDto = ((ObjectResult)result.Result).Value as ShoppingCartDto;

            shoppingCartDto.ShouldNotBeNull();
            shoppingCartDto.OrderItems.Count.ShouldBe(expectedOrderItemCount);
        }

        [Theory]
        [InlineData(-1, -1, 0, "-1")]
        public  void RemovesItem(int shoppingCartId, int itemId, int expectedOrderItemCount, string userId)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);

            var result = controller.RemoveItem(shoppingCartId, itemId);
            var shoppingCartDto = ((ObjectResult)result.Result).Value as ShoppingCartDto;

            shoppingCartDto.ShouldNotBeNull();
            shoppingCartDto.OrderItems.ShouldNotContain(item => item.Id == itemId);
            shoppingCartDto.OrderItems.Count.ShouldBe(expectedOrderItemCount);
        }

        public static IEnumerable<object[]> AddItemTestData()
        {
            return new List<object[]>
            {

                new object[] { -1, -1, 1, "-1" }, // shoppingCartId, tourId, expectedOrderItemCount, userId
                new object[] { -1, -3, 1, "-1" } // tour that is not published, count stays the same

            };
        }

        public static IEnumerable<object[]> RemoveItemTestData()
        {
            return new List<object[]>
            {
                new object[] { -1, -1, 0, "-1" }, // shoppingCartId, itemId, expectedOrderItemCount, userId
            };
        }
    }
}
