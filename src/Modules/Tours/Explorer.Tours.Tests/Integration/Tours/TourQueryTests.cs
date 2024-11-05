using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;
using Explorer.Tours.API.Dtos.TourDtos.DurationDtos;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.API.Dtos.TourDtos.PriceDtos;
using Explorer.Tours.Core.Domain.Tours;

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
                        Latitude = 20,
                        Longitude = 20,
                        City = "Test",
                        Country = "Test"
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
                        Latitude = 20,
                        Longitude = 20,
                        City = "Test",
                        Country = "Test"
                    }
        }
                }, 
                 TourInfo = new TourDto()
                 {
                    Description = "Test",
                    Name = "Test",
                    Difficulty ="Easy",
                    Tags = new List<string> { "Test" },
                    Price = new PriceDto()
                    {
                        Amount = 10
                    },
                    TotalLength = new DistanceDto()
                    {
                        Length = 10,
                        Unit = "Kilometers"
                    },
                    Status = "Draft",
                    AuthorId = -1000,
                    Durations = new List<TourDurationDto>()
                    {
                        new TourDurationDto()
                        {
                            Duration = TimeOnly.FromDateTime(DateTime.UtcNow),
                            TransportType = "Bike"
                        }
                    }
                 } 
            };
            //Art 
            var result = ((ObjectResult)controller.Create(tourCreateDto).Result)?.Value as TourCreateDto;

            //Assert
            result.ShouldNotBeNull();
            dbContext.Tours.Count().ShouldBe(4);
            dbContext.Tours.FirstOrDefault(x => x.Name == result.TourInfo.Name).ShouldNotBeNull();
            dbContext.Checkpoints.FirstOrDefault(x=>x.Name=="Test").ShouldNotBeNull();
            dbContext.Objects.FirstOrDefault(x => x.Name == "Test").ShouldNotBeNull();
        }
        [Fact]
        public void Get_tour_details()
        {
            //Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            //Art 
            var result = ((ObjectResult)controller.GetTourDetailsByTourId(-2).Result)?.Value as TourReadDto;

            //Assert
            result.ShouldNotBeNull();
            result.Checkpoints.Count.ShouldBe(2);
            result.Objects.Count.ShouldBe(1);
            foreach (var checkpoint in result.Checkpoints)
            {
                checkpoint.Location.ShouldNotBeNull();
                /*checkpoint.Location.Latitude.ShouldBe(202);
                checkpoint.Location.Longitude.ShouldBe(202);*/
            }
            foreach (var obj in result.Objects)
            {
                obj.Location.ShouldNotBeNull();
                /*obj.Location.Latitude.ShouldBe(202);
                obj.Location.Longitude.ShouldBe(202);*/
            }
        }

        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-2")
            };
        }
        
    }
}
