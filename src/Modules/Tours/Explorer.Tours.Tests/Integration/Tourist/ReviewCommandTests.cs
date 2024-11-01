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

            // Test 1: Invalid Rating
            var reviewDtoInvalidRating = new ReviewDto
            {
                TouristId = 1,
                Rating = 6, // Invalid rating (should be between 1 and 5)
                Comment = "Valid comment",
                TourDate = DateTime.Now.AddDays(-1),
                ReviewDate = DateTime.Now,
                Images = new List<string> { "url1", "url2" }
            };
            var resultInvalidRating = (ObjectResult)controller.Create(reviewDtoInvalidRating).Result;

            // Assert
            resultInvalidRating.ShouldNotBeNull();
            resultInvalidRating.StatusCode.ShouldBe(400);

            // Test 2: Invalid Comment (empty)
            var reviewDtoInvalidComment = new ReviewDto
            {
                TouristId = 1,
                Rating = 3,
                Comment = "", // Invalid comment (empty)
                TourDate = DateTime.Now.AddDays(-1),
                ReviewDate = DateTime.Now,
                Images = new List<string> { "url1" }
            };
            var resultInvalidComment = (ObjectResult)controller.Create(reviewDtoInvalidComment).Result;

            // Assert
            resultInvalidComment.ShouldNotBeNull();
            resultInvalidComment.StatusCode.ShouldBe(400);

            // Test 3: Invalid Tour Date (future date)
            var reviewDtoInvalidTourDate = new ReviewDto
            {
                TouristId = 1,
                Rating = 3,
                Comment = "Valid comment",
                TourDate = DateTime.Now.AddDays(1), // Invalid tour date (in the future)
                ReviewDate = DateTime.Now,
                Images = new List<string> { "url1" }
            };
            var resultInvalidTourDate = (ObjectResult)controller.Create(reviewDtoInvalidTourDate).Result;

            // Assert
            resultInvalidTourDate.ShouldNotBeNull();
            resultInvalidTourDate.StatusCode.ShouldBe(400);

            // Test 4: Invalid Review Date (before Tour Date)
            var reviewDtoInvalidReviewDate = new ReviewDto
            {
                TouristId = 1,
                Rating = 3,
                Comment = "Valid comment",
                TourDate = DateTime.Now.AddDays(-1),
                ReviewDate = DateTime.Now.AddDays(-2), // Invalid review date (before tour date)
                Images = new List<string> { "url1" }
            };
            var resultInvalidReviewDate = (ObjectResult)controller.Create(reviewDtoInvalidReviewDate).Result;

            // Assert
            resultInvalidReviewDate.ShouldNotBeNull();
            resultInvalidReviewDate.StatusCode.ShouldBe(400);
        }

        private static ReviewController CreateController(IServiceScope scope)
        {
            return new ReviewController(scope.ServiceProvider.GetRequiredService<IReviewService>());
        }
    }
}
