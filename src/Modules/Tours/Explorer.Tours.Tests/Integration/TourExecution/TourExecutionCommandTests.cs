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
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.TourExecutions;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Shopping;

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
                TouristId = -21,
                Longitude = -20,
                Latitude = 50,
                Status = "ONGOING",
                Completion = 0
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
        public void Create_fails_tour_not_bought()
        {
            {
                // Arrange
                using var scope = Factory.Services.CreateScope();
                var controller = CreateController(scope);
                var updatedEntity = new TourExecutionDto
                {
                    TourId = -3,
                    TouristId = -1,
                    Longitude = -20,
                    Latitude = 50
                };

                // Act
                var result = (ObjectResult)controller.CreateTourExecution(updatedEntity).Result;

                // Assert
                result.ShouldNotBeNull();
                result.StatusCode.ShouldBe(403);
            }
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

        [Fact]
        public void Updates_tourist_location_on_tour_execution()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updateEntity = new TourExecutionDto()
            {
                Id = -2,
                TourId = -1,
                TouristId = -1,
                Status = "ONGOING",
                Longitude = 50,
                Latitude = 42,
                Completion = 0
            };

            var oldStatus = TourExecutionStatus.ONGOING;

            // Act
            var result = ((ObjectResult)controller.Update(updateEntity).Result)?.Value as TourExecutionDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Longitude.ShouldBe(50);
            result.Latitude.ShouldBe(42);

            // Assert - Database
            var storedEntity = dbContext.TourExecutions.FirstOrDefault(i => i.Id == updateEntity.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Position.Longitude.ShouldBe(50);
            storedEntity.Position.Latitude.ShouldBe(42);
            var oldEntity = dbContext.TourExecutions.FirstOrDefault(i => i.Position == new TouristPosition(5, 2));
            oldEntity.ShouldBeNull();
        }

        private static TourExecutionController CreateController(IServiceScope scope)
        {
            return new TourExecutionController(scope.ServiceProvider.GetRequiredService<ITourExecutionService>(),
           scope.ServiceProvider.GetRequiredService<ICheckpointService>(),
           scope.ServiceProvider.GetRequiredService<ITourService>(),
           scope.ServiceProvider.GetRequiredService<IPurchaseTokenService>(),
           scope.ServiceProvider.GetRequiredService<IObjectService>())
            {
                ControllerContext = BuildContext("-21")
            };
        }
    }
}
