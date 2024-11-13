using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
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
                Id = -100,
                TouristId = 1,
                TourId = -1,
                Rating = 4,
                Comment = "Great experience!",
                TourDate = DateTime.UtcNow.AddDays(-10),
                ReviewDate = DateTime.UtcNow,
                Images = new List<string> { "image1.jpg", "image2.jpg" },
                Completion = 70
            };

            // Act
            var result = ((ObjectResult)controller.Create(reviewDto).Result)?.Value as ReviewDto;

            // Assert
            result.ShouldNotBeNull();
            result.Rating.ShouldBe(reviewDto.Rating);
            result.Comment.ShouldBe(reviewDto.Comment);
            result.TouristId.ShouldBe(reviewDto.TouristId);
        }

        [Theory]
        [InlineData(-200, 1, -1, 6, "Great experience!", -10, 0, 400)] // Invalid rating
        [InlineData(-300, 1, -1, 4, "", -10, 0, 400)] // Empty comment
        [InlineData(-400, 1, -1, 4, "Great experience!", 10, 0, 400)] // Future tour date
        [InlineData(-500, 1, -1, 4, "Great experience!", -10, -20, 400)] // Review date before tour date

        public void Create_review_with_invalid_data(
            int id, int touristId, int tourId, int rating, string comment,
            int tourDateOffset, int reviewDateOffset, int expectedStatusCode)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var reviewDto = new ReviewDto
            {
                Id = id,
                TouristId = touristId,
                TourId = tourId,
                Rating = rating,
                Comment = comment,
                TourDate = DateTime.UtcNow.AddDays(tourDateOffset),
                ReviewDate = DateTime.UtcNow.AddDays(reviewDateOffset),
                Images = new List<string> { "image1.jpg", "image2.jpg" },
                Completion = 70
            };

            // Act
            var result = (ObjectResult)controller.Create(reviewDto).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedStatusCode);
            result.Value.ShouldNotBeNull();  // Optionally check for error details if your controller provides them
        }

        private static ReviewController CreateController(IServiceScope scope)
        {
            return new ReviewController(scope.ServiceProvider.GetRequiredService<IReviewService>());
        }
    }
}
