using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class ProblemCommandTest : BaseStakeholdersIntegrationTest
    {
        public ProblemCommandTest(StakeholdersTestFactory factory) : base(factory) { }
        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new ProblemDto
            {
                Id = 50,
                Category = "jej",
                Priority = "top",
                Description = "ide gas crni bmw",
                Date = new DateTime(),
                TourId = 0,
                TouristId = -21
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ProblemDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Description.ShouldBe(newEntity.Description);

            // Assert - Database
            var storedEntity = dbContext.Problems.FirstOrDefault(i => i.Description == newEntity.Description);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ProblemDto
            {
                
                Description = "Test"
                
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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new ProblemDto
            {
                Id = -1,
                Category = "jej",
                Priority = "top",
                Description = "kvakvakva",
                Date = new DateTime(),
                TourId = 0,
                TouristId = -2
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ProblemDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);

            result.Description.ShouldBe(updatedEntity.Description);
            result.Category.ShouldBe(updatedEntity.Category);
            result.Date.ShouldBe(updatedEntity.Date);
            result.Priority.ShouldBe(updatedEntity.Priority);
            result.TouristId.ShouldBe(updatedEntity.TouristId);
            result.TourId.ShouldBe(updatedEntity.TourId);


            // Assert - Database
            var storedEntity = dbContext.Problems.FirstOrDefault(i => i.Category == "jej");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Problems.FirstOrDefault(i => i.Priority == "prvi");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ProblemDto
            {
                Id = -1000,
                Category = "jej",
                Priority = "top",
                Description = "kvakvakva",
                Date = new DateTime(),
                TourId = 0,
                TouristId = 0
                
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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-2);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Problems.FirstOrDefault(i => i.Id == -2);
            storedCourse.ShouldBeNull();
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

        private static ProblemController CreateController(IServiceScope scope)
        {
            return new ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
