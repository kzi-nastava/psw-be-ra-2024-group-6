using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Explorer.API.Controllers.Author;
using System.Diagnostics;
namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class CheckpointCommandTests : BaseToursIntegrationTest
    {
        public CheckpointCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new CheckpointDto
            {
                Name = "Obuća za grub teren",
                Description = "Patike sa tvrdim đonom i kramponima koje daju stabilnost na neravnom i rastresitom terenu.",
                LocationId = -1000,
                TourId = -3,
                ImageUrl = "neka/putanja"

            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CheckpointDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            // Assert - Database
            var storedEntity = dbContext.Checkpoints.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CheckpointDto
            {
                Description = "Test",

            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;
            
            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new CheckpointDto
            {
                Id = -2,
                Name = "Big",
                Description = "Predaleko je",
                LocationId = -2,
                TourId = -2,
                ImageUrl = "neka/putanja/doslike"

            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as CheckpointDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.Name.ShouldBe(updatedEntity.Name);
            result.Description.ShouldBe(updatedEntity.Description);

            // Assert - Database
            var storedEntity = dbContext.Checkpoints.FirstOrDefault(i => i.Name == "Big");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Checkpoints.FirstOrDefault(i => i.Name == "Promenada");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CheckpointDto
            {
                Id = -1000,
                Name = "Test",
                Description = "Test",
                LocationId = 0,
                TourId = 0,
                ImageUrl = "neka/putanja/doslike"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(-1000);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Equipment.FirstOrDefault(i => i.Id == -1000);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-2000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static CheckpointController CreateController(IServiceScope scope)
        {
            return new CheckpointController(scope.ServiceProvider.GetRequiredService<ICheckpointService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
