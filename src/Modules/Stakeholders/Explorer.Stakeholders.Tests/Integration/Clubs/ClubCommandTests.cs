using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Clubs
{
    [Collection("Sequential")]
    public class ClubCommandTests : BaseStakeholdersIntegrationTest
    {
        public ClubCommandTests(StakeholdersTestFactory factory) : base(factory)
        {
        }

        [Fact]
        public void Creates()
        {
            // Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newClub = new ClubDto
            {
                Name = "Planinarski klub Avantura",
                Description = "Klub za ljubitelje avantura u prirodi.",
                ImageId = null,
                OwnerId = -21
            };

            // Act
            var result = ((ObjectResult)controller.Create(newClub).Result)?.Value as ClubDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newClub.Name);


            // Assert - Database
            var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == newClub.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var newClub = new ClubDto()
            {
                Description = "Invalid data without a name"
            };

            // Act
            var result = (ObjectResult)controller.Create(newClub).Result;

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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedCLub = new ClubDto
            {
                Id = -10,
                Name = "Changed Name",
                Description = "Changed Description",
                ImageId = null,
                OwnerId = -21
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedCLub).Result)?.Value as ClubDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-10);
            result.Name.ShouldBe(updatedCLub.Name);
            result.Description.ShouldBe(updatedCLub.Description);

            // Assert - Database 
            var storedClub = dbContext.Clubs.FirstOrDefault(i => i.Name == updatedCLub.Name);
            storedClub.ShouldNotBeNull();
            storedClub.Description.ShouldBe(updatedCLub.Description);
            var oldClub = dbContext.Clubs.FirstOrDefault(i => i.Name == "Test Description 01");
            oldClub.ShouldBeNull();
        }


        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedClub = new ClubDto
            {
                Id = -1000, // non-existent club ID
                Name = "Test"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedClub).Result;

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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-20); 
            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedClub = dbContext.Clubs.FirstOrDefault(i => i.Id == -20);
            storedClub.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000); // non-existent club ID

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static ClubController CreateController(IServiceScope scope, string userId = "-21")
        {
            var controller = new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = BuildLocalContext(userId)
            };
            return controller;
        }

        private static ControllerContext BuildLocalContext(string userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", userId), 
                new Claim(ClaimTypes.Role, "tourist") 
            }, "mock"));

            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

    }
}
