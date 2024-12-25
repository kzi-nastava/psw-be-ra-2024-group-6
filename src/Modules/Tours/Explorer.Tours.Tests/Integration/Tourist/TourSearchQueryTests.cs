using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class TourSearchControllerTests : BaseToursIntegrationTest
    {
        public TourSearchControllerTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void SearchToursNearby_ReturnsToursWithinRadius()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            double latitude = 10.98;
            double longitude = 38.20;
            double radius = 5;

            // Act
            var result = controller.SearchToursNearby(latitude, longitude, radius);
            var okResult = result.Result as OkObjectResult;
            var tours = okResult?.Value as List<TourCardDto>;

            // Assert
            okResult.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(200);
            tours.ShouldNotBeNull();
            tours.Count.ShouldBeGreaterThan(0);

            // Check if each tour is within the specified radius
            foreach (var tour in tours)
            {
                var tourEntity = scope.ServiceProvider.GetRequiredService<ToursContext>().Tours
                    .Find(tour.Id);
                tourEntity.ShouldNotBeNull();

                bool isNearby = tourEntity.Checkpoints.Any(checkpoint =>
                    checkpoint.Location.CalculateDistance(latitude, longitude) <= radius);

                isNearby.ShouldBeTrue();
            }
        }

        private static TourSearchController CreateController(IServiceScope scope)
        {
            return new TourSearchController(scope.ServiceProvider.GetRequiredService<ITourSearchService>());
        }
    }
}
