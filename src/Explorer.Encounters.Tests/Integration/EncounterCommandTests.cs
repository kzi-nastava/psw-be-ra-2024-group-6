using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Stakeholders.API.Public.Administration;

namespace Explorer.Encounters.Tests.Integration
{
    public class EncounterCommandTests : BaseEncountersIntegrationTest
    {
        public EncounterCommandTests(EncountersTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
            var newEncounter = new EncounterCreateDto()
            {
                Id = 50,
                Name = "Hidden Treasure",
                Description = "Find the hidden treasure in the forest",
                Location = new LocationDto()
                {
                    Latitude = 19.042,
                    Longitude = 18.025
                },
                Xp = 100,
                Status = "Active", 
                TypeEncounter = "Location"
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEncounter).Result)?.Value as EncounterCreateDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEncounter.Name);
            result.Description.ShouldBe(newEncounter.Description);
            result.Xp.ShouldBe(newEncounter.Xp);
            result.Status.ShouldBe(newEncounter.Status);
            result.TypeEncounter.ShouldBe(newEncounter.TypeEncounter);

            // Assert - Database
            var storedEntity = dbContext.Encounters.FirstOrDefault(i => i.Name == newEncounter.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var newEncounter = new EncounterCreateDto()
            {
                Description = "Invalid encounter without name"
            };

            // Act
            var result = (ObjectResult)controller.Create(newEncounter).Result;

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
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();
            var updatedEncounter = new EncounterCreateDto()
            {
                Id = -1,
                Name = "Hidden Treasure",
                Description = "Find the hidden treasure in the forest",
                Location = new LocationDto()
                {
                    Latitude = 19.042,
                    Longitude = 18.025
                },
                Xp = 100,
                Status = "Active",
                TypeEncounter = "Location"
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEncounter).Result)?.Value as EncounterCreateDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Name.ShouldBe(updatedEncounter.Name);
            result.Description.ShouldBe(updatedEncounter.Description);
            result.Xp.ShouldBe(updatedEncounter.Xp);

            // Assert - Database
            var storedEncounter = dbContext.Encounters.FirstOrDefault(i => i.Id == -1);
            storedEncounter.ShouldNotBeNull();
            storedEncounter.Name.ShouldBe(updatedEncounter.Name);
            storedEncounter.Description.ShouldBe(updatedEncounter.Description);
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEncounter = new EncounterCreateDto()
            {
                Id = -1000,
                Name = "Hidden Treasure",
                Description = "Find the hidden treasure in the forest",
                Location = new LocationDto()
                {
                    Latitude = 19.042,
                    Longitude = 18.025
                },
                Xp = 100,
                Status = "Active",
                TypeEncounter = "Location"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEncounter).Result;

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
            var dbContext = scope.ServiceProvider.GetRequiredService<EncountersContext>();

            // Act
            var result = (OkResult)controller.Delete(-1); // Assuming you have seed data with this ID

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEncounter = dbContext.Encounters.FirstOrDefault(i => i.Id == -1);
            storedEncounter.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000); 

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        private static EncounterController CreateController(IServiceScope scope)
        {
            return new EncounterController(scope.ServiceProvider.GetRequiredService<IEncounterService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
