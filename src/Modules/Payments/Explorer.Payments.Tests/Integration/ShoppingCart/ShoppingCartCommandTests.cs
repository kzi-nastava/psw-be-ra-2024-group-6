using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Shopping;
using Explorer.Payments.API.Dtos;
using IShoppingCartService = Explorer.Payments.API.Public.IShoppingCartService;

namespace Explorer.Payments.Tests.Integration.ShoppingCart
{
    [Collection("Sequential")]
    public class ShoppingCartCommandTests : BasePaymentsIntegrationTests
    {
        public ShoppingCartCommandTests(PaymentsTestFactory factory) : base(factory) { }

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
        [InlineData(-1, 1, "-1000")]
        [InlineData(-100, 0, "-1001")]
        public void AddsItem(int tourId, int expectedOrderItemCount, string userId)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);

            var result = controller.AddItem(tourId);
            var shoppingCartDto = ((ObjectResult)result.Result).Value as ShoppingCartDto;

            shoppingCartDto.ShouldNotBeNull();
            shoppingCartDto.OrderItems.Count.ShouldBe(expectedOrderItemCount);
        }

        [Theory]
        [InlineData(-5, -5, 0, "-5")]
        public void RemovesItem(int shoppingCartIdint, int itemId, int expectedOrderItemCount, string userId)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);

            var result = controller.RemoveItem(itemId);
            var shoppingCartDto = ((ObjectResult)result.Result).Value as ShoppingCartDto;

            shoppingCartDto.ShouldNotBeNull();
            shoppingCartDto.OrderItems.ShouldNotContain(item => item.Id == itemId);
            shoppingCartDto.OrderItems.Count.ShouldBe(expectedOrderItemCount);
        }

        [Fact]
        public void ChecksOutCart()
        {
            // Arrange
            string userId = "-30";
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);

            // Act
            var result = controller.CheckoutCart().Result as ObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            var tokens = result.Value as List<PurchaseTokenDto>;
            tokens.ShouldNotBeNull();
            tokens.Count.ShouldBe(1);
        }
        [Fact]
        public void ChecksOutCart_fail()
        {
            // Arrange
            string userId = "-2";
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);

            // Act
            var result = controller.CheckoutCart().Result as ObjectResult;

            // Assert
            result.StatusCode.ShouldBe(500);
        }
        [Fact]
        public void ChecksOutCart_already_has_purchaseToken()
        {
            // Arrange
            string userId = "-21";
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, userId);

            // Act
            var result = controller.CheckoutCart().Result as ObjectResult;

            // Assert
            result.StatusCode.ShouldBe(500);
        }
    }
}
