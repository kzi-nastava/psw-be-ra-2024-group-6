using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Explorer.Tours.Tests.Integration.Reviews
{
    [Collection("Sequential")]
    public class ReviewCommandTests : BaseToursIntegrationTest
    {
        public ReviewCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Create_review()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var reviewDto = new ReviewDto
            {
                TouristId = -10,
                Rating = 4,
                Comment = "Great experience!",
                TourDate = DateTime.UtcNow.AddDays(-10),
                ReviewDate = DateTime.UtcNow,
                Images = new List<string> { "image1.jpg", "image2.jpg" }
            };

            // Act
            var result = ((ObjectResult)controller.Create(reviewDto).Result)?.Value as ReviewDto;

            // Assert
            result.ShouldNotBeNull();
            dbContext.Reviews.FirstOrDefault(r => r.TouristId == reviewDto.TouristId && r.Comment == reviewDto.Comment).ShouldNotBeNull();
        }

        [Fact]
        public void Create_review_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var reviewDto = new ReviewDto { Rating = 6 }; // Invalid rating

            // Act
            var result = (ObjectResult)controller.Create(reviewDto).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        private static ReviewController CreateController(IServiceScope scope)
        {
            return new ReviewController(scope.ServiceProvider.GetRequiredService<IReviewService>());
        }
    }
}
