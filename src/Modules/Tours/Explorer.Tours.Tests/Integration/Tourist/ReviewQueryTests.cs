using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Explorer.Tours.Tests.Integration.Reviews
{
    [Collection("Sequential")]
    public class ReviewQueryTests : BaseToursIntegrationTest
    {
        public ReviewQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all_reviews()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = ((ObjectResult)controller.GetAll().Result)?.Value as IEnumerable<ReviewDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count().ShouldBe(3);
        }

        private static ReviewController CreateController(IServiceScope scope)
        {
            return new ReviewController(scope.ServiceProvider.GetRequiredService<IReviewService>());
        }
    }
}
