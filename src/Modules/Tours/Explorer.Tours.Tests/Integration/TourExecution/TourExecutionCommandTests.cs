using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.TourExecutions;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.TourExecution
{
    public class TourExecutionCommandTests : BaseToursIntegrationTest
    {
        public TourExecutionCommandTests(ToursTestFactory factory) : base(factory) {}

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourExecutionDto
            {
                TourId = -1,
                TouristId = -1,
                Longitude = -20,
                Latitude = 50
            };

            // Act
            var result = ((ObjectResult)controller.CreateTourExecution(newEntity).Result)?.Value as TourExecutionDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.TourId.ShouldNotBe(0);
            result.TourId.ShouldBe(-1);
            result.TouristId.ShouldNotBe(0);
            result.TouristId.ShouldBe(-1);

            // Assert - Database
            var storedEntity = dbContext.TourExecutions.FirstOrDefault(i => i.TourId == newEntity.TourId && i.TouristId == newEntity.TouristId);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Creates_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourExecutionDto
            {
                TourId = -1,
                TouristId = -1,
                Longitude = -200,
                Latitude = -200
            };

            // Act
            var result = (ObjectResult)controller.CreateTourExecution(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Finalizes_tour_execution()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            int id = -3;
            string status = "COMPLETED";
            var oldStatus = TourExecutionStatus.ONGOING;

            // Act
            var result = ((ObjectResult)controller.FinalizeTourExecution(id, status).Result)?.Value as TourExecutionDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Status.ShouldBe("COMPLETED");

            // Assert - Database
            var storedEntity = dbContext.TourExecutions.FirstOrDefault(i => i.Id == id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(status);
            var oldEntity = dbContext.TourExecutions.FirstOrDefault(i => i.Status == oldStatus);
            oldEntity.ShouldBeNull();
        }

        private static TourExecutionController CreateController(IServiceScope scope)
        {
            return new TourExecutionController(scope.ServiceProvider.GetRequiredService<ITourExecutionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
