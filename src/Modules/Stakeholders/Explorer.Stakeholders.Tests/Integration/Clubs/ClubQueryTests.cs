using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Clubs
{
    [Collection("Sequential")]
    public class ClubQueryTests : BaseStakeholdersIntegrationTest
    {
        public ClubQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();


            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ClubDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(4);  
            result.TotalCount.ShouldBe(4);     
        }

        private static ClubController CreateController(IServiceScope scope)
        {
            return new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
