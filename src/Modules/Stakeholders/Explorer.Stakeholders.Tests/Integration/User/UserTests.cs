using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.User
{
    [Collection("Sequential")]
    public class UserTests : BaseStakeholdersIntegrationTest
    {
        public UserTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void GetPaged_users_sucessfull()
        {

            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetPaged().Result)?.Value as PagedResult<UserDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(7);
            result.TotalCount.ShouldBe(7);
        }
        [Fact]
        public void Update_sucessfull()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new UserDto
            {
                Id = -1,
                Username = "Zdravko",
                Password = "Dren",
                Role = "Administrator",
                IsActive = true,
                IsBlocked = false
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as UserDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Username.ShouldBe(updatedEntity.Username);
            result.Password.ShouldBe(updatedEntity.Password);
            result.Role.ShouldBe(updatedEntity.Role);
            result.IsActive.ShouldBe(updatedEntity.IsActive);
            result.IsBlocked.ShouldBe(updatedEntity.IsBlocked);

            // Assert - Database
            var storedEntity = dbContext.Users.FirstOrDefault(i => i.Username == "Zdravko");
            storedEntity.ShouldNotBeNull();
            storedEntity.Password.ShouldBe(updatedEntity.Password);
            var oldEntity = dbContext.Users.FirstOrDefault(i => i.Username == "admin@gmail.com");
            oldEntity.ShouldBeNull();
        }

        private static UserController CreateController(IServiceScope scope)
        {
            return new UserController(scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }

}
