using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.API.Controllers.Stakeholders;
using Explorer.Blog.API.Public;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class CheckpointQueryTests : BaseToursIntegrationTest
    {
        public CheckpointQueryTests(ToursTestFactory factory) : base(factory) { }
        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<CheckpointDto>;
            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }
        [Fact]
        public void Retrieves_all_for_tour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            // Act
            var result = ((ObjectResult)controller.GetByTourId(-2).Result)?.Value as List<CheckpointReadDto>;
            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }

        [Fact]
        public void Retrieves_most_popular_destinations()
        {
            // Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateLandingPageController(scope);

            // Act
            var result = ((ObjectResult)controller.GetMostPopularDestinations().Result)?.Value as List<DestinationDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }

        private static CheckpointController CreateController(IServiceScope scope)
        {
            return new CheckpointController(scope.ServiceProvider.GetRequiredService < ICheckpointService>())
            {
                ControllerContext = BuildContext("-2")
            };
        }

        private static LandingPageController CreateLandingPageController(IServiceScope scope)
        {
            return new LandingPageController(scope.ServiceProvider.GetRequiredService<ITourService>(),
                scope.ServiceProvider.GetRequiredService<IAuthorService>(),
                scope.ServiceProvider.GetRequiredService<ICheckpointService>(),
                scope.ServiceProvider.GetRequiredService<IRatingService>(),
                scope.ServiceProvider.GetRequiredService<IBlogService>())
            {
                ControllerContext = BuildContext("-2")
            };
        }
    }
}
