
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.Core.Domain;
using Shouldly;
using Xunit;

namespace Explorer.Payments.Tests.Integration.Tourist
{
    public class ShoppingCartUnitTests
    {
        [Theory]
        [InlineData(-1, -101, "Tour A", 50.0)]
        public void AddsItem(long userId, long tourId, string tourName, double price)
        {
            // Arrange
            var shoppingCart = new Core.Domain.ShoppingCart(userId, new Price(0));

            // Act
            var result = shoppingCart.AddTour(tourId, tourName, price);

            // Assert
            result.ShouldNotBeNull();
            shoppingCart.OrderItems.Count.ShouldBe(1);
            shoppingCart.TotalPrice.Amount.ShouldBe(price);
        }

        [Fact]
        public void CalculatesTotalPrice()
        {
            // Arrange
            var shoppingCart = new Core.Domain.ShoppingCart(1, new Price(0));
            shoppingCart.AddTour(101, "Tour A", 50.0);
            shoppingCart.AddTour(102, "Tour B", 30.0);

            // Assert Total Price after additions
            shoppingCart.TotalPrice.Amount.ShouldBe(80.0);

            // Act
            var itemToRemove = shoppingCart.OrderItems.FirstOrDefault();
            if (itemToRemove != null)
            {
                shoppingCart.RemoveItem((int)itemToRemove.Id);
            }

            // Assert Total Price after removal
            shoppingCart.TotalPrice.Amount.ShouldBe(30.0);
        }
    }
}