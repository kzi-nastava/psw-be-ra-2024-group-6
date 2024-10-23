using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Tours
{
    [Collection("Sequential")]
    public class TourQueryTests : BaseToursIntegrationTest
    {
        public TourQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            //Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            //Art 
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourDto>;

            //Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(4);
            result.TotalCount.ShouldBe(4);
        }
        [Fact]
        public void Create_tour()
        {
            //Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            TourCreateDto tourCreateDto = new TourCreateDto
            {
                Checkpoints = new List<CheckpointCreateDto>() {

                new CheckpointCreateDto
                {
                    Description = "Test",
                    Name = "Test",
                    ImageUrl = "Test",
                    Location= new LocationCreateDto()
                    {
                        Latitude = 202,
                        Longitude = 202
                    }
                },
            },
                Objects = new List<ObjectCreateDto>() {

                 new ObjectCreateDto {
                        Description = "Test",
                        Name = "Test",
                        ImageUrl = "Test",
            Category = "WC",
            Location = new LocationCreateDto {
                        Latitude = 202,
                        Longitude = 202
                    }
        }
                }, 
                 TourInfo = new TourInfoDto
                 {
                    Description = "Test",
                    Name = "Test",
                    Difficulty = Difficulty.Easy,
                    Tags = new List<string> { "Test" },
                    Cost = 0,
                    Status = Status.Draft,
                    AuthorId = -1000
                 } 
            };
            //Art 
            var result = ((ObjectResult)controller.CreateTour(tourCreateDto).Result)?.Value as TourCreateDto;

            //Assert
            result.ShouldNotBeNull();
            dbContext.Tours.Count().ShouldBe(4);
            dbContext.Tours.FirstOrDefault(x => x.Name == result.TourInfo.Name).ShouldNotBeNull();
            dbContext.Checkpoints.FirstOrDefault(x=>x.Name=="Test").ShouldNotBeNull();
            dbContext.Locations.FirstOrDefault(x => x.Latitude == 202).ShouldNotBeNull();
            dbContext.Objects.FirstOrDefault(x => x.Name == "Test").ShouldNotBeNull();
        }

        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
        
    }
}
