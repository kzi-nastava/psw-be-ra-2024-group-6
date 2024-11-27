using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Shopping;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration.ShoppingCart
{
    [Collection("Sequential")]
    public class CouponCommandTests : BasePaymentsIntegrationTests
    {
        public CouponCommandTests(PaymentsTestFactory factory) : base(factory)
        {
        }

        private static CouponController createController(IServiceScope scope, string userId = "-11")
        {
            var controller = new CouponController(scope.ServiceProvider.GetRequiredService<ICouponService>())
            {
                ControllerContext = BuildLocalContext(userId)
            };

            return controller;
        }

        [Theory]
        [InlineData("12345678", 20, -11, -1, "2025-12-12", "-11", true)]
        [InlineData("11111111", 20, -11, -2, "2025-12-12", "-11", false)] // not author's tour
        public void Creates(string code, double discount, long authorId, long tourId, DateTime expiresDate, string userId, bool success)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = createController(scope, userId);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();


            var dto = new CouponDto()
            {
                AuthorId = authorId,
                TourId = tourId,
                ExpiresDate = expiresDate,
                Code = code,
                DiscountPercentage = discount
            };

            if (success)
            {
                var result = ((ObjectResult)controller.Create(dto).Result)?.Value as CouponDto;
                result.ShouldNotBeNull();
                result.Id.ShouldNotBe(0);
                result.Code.ShouldBe(dto.Code);

                var storedEntity = dbContext.Coupons.FirstOrDefault(i => i.Code == result.Code);
                storedEntity.ShouldNotBeNull();
                storedEntity.Id.ShouldBe(result.Id);
            }
            else
            {
                var result = (ObjectResult)controller.Create(dto).Result;
                result.ShouldNotBeNull();
                result.StatusCode.ShouldBe(403);
            }

        }

        [Theory]
        [InlineData("BBBBBBBB", 25, -11, -1, "2025-12-12", "-11", true)]
        [InlineData("ABBBBBBB", 10, -11, -2, "2025-12-12", "-22", false)] // Not authorized user
        public void Updates(string code, double discount, long authorId, long tourId, DateTime expiresDate, string userId, bool success)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = createController(scope, userId);

            var dto = new CouponDto()
            {
                Id = -1,
                AuthorId = authorId,
                TourId = tourId,
                ExpiresDate = expiresDate,
                Code = code,
                DiscountPercentage = discount
            };


            if (success)
            {
                var result = controller.Update(dto);
                var updatedCoupon = ((ObjectResult)result.Result).Value as CouponDto;
                updatedCoupon.ShouldNotBeNull();
                updatedCoupon.Code.ShouldBe(dto.Code);
                updatedCoupon.DiscountPercentage.ShouldBe(dto.DiscountPercentage);
            }
            else
            {
                var result = (ObjectResult)controller.Update(dto).Result;
                result.ShouldNotBeNull();
                result.StatusCode.ShouldBe(403);
            }
        }

        [Theory]
        [InlineData(-1, "-11", true)] // Valid delete
        [InlineData(-2, "-22", false)] // Unauthorized delete
        public void Deletes(long couponId, string userId, bool success)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = createController(scope, userId);



            if (success)
            {
                var result = controller.Delete(couponId);
                result.ShouldBeOfType<NoContentResult>();
                var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
                dbContext.Coupons.FirstOrDefault(c => c.Id == couponId).ShouldBeNull();
            }
            else
            {
                var result = (ObjectResult)controller.Delete(couponId);
                result.ShouldNotBeNull();
                result.StatusCode.ShouldBe(403);
            }
        }

        [Theory]
        [InlineData(-11, "-11", true)] // Valid author and user
        public void GetAllByAuthor(long authorId, string userId, bool success)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = createController(scope, userId);
            var result = controller.GetCouponsByAuthor();
            var coupons = ((ObjectResult)result.Result).Value as List<CouponDto>;
            coupons.ShouldNotBeNull();
            coupons.All(c => c.AuthorId == authorId).ShouldBeTrue();
        }


        private static ControllerContext BuildLocalContext(string userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", userId), // 
                new Claim(ClaimTypes.Role, "author") 
            }, "mock"));

            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

    }
}
