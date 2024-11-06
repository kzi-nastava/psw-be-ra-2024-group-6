using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Author;
using Shouldly;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.DistanceDtos;
using Explorer.Tours.API.Dtos.TourDtos.DurationDtos;
using Explorer.Tours.API.Dtos.TourDtos.PriceDtos;

namespace Explorer.Tours.Tests.Integration.Tours;

[Collection("Sequential")]
    public class TourCommandTests : BaseToursIntegrationTest
    {
    public TourCommandTests(ToursTestFactory factory) : base(factory) { }

   

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedTour = new TourDto
        {
            Id = -2,
            Name = "Promjenjeno ime",
            Description = "Promjenjena deskripcija.",
            Difficulty = "Hard",
            Tags = new List<string> { "gas","aa" },
            Price = new PriceDto()
            {
                Amount = 10
            },
            Durations = new List<TourDurationDto>()
            {
                new TourDurationDto()
                {
                    Duration = TimeOnly.FromDateTime(DateTime.UtcNow),
                    TransportType = "Bike"
                }
            },
            TotalLength = new DistanceDto()
            {
                Length = 10,
                Unit = "Kilometers"
            },
            Status = "Draft",
            AuthorId = -1,
            
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedTour).Result)?.Value as TourDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-2);
        result.Name.ShouldBe(updatedTour.Name);
        result.Description.ShouldBe(updatedTour.Description);
        result.Difficulty.ShouldBe(updatedTour.Difficulty);
        result.Tags.ShouldBe(updatedTour.Tags);
        result.Price.Amount.ShouldBe(updatedTour.Price.Amount);
        result.Status.ShouldBe(updatedTour.Status);

        // Assert - Database 
        var storedTour = dbContext.Tours.FirstOrDefault(i => i.Name == updatedTour.Name);
        storedTour.ShouldNotBeNull();
        storedTour.Description.ShouldBe(updatedTour.Description);
        var oldTour = dbContext.Tours.FirstOrDefault(i => i.Name == "Test Description 01");
        oldTour.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedTour = new TourDto
        {
            Id = -1000, // non-existent club ID
            Name = "Test",
            Description = "Promjenjena deskripcija.",
            Difficulty = "Hard",
            Tags = new List<string>(){"gas"},
            Price =new PriceDto()
            {
                Amount = 10
            },
            Durations = new List<TourDurationDto>()
            {
                new TourDurationDto()
                {
                    Duration = TimeOnly.FromDateTime(DateTime.UtcNow),
                    TransportType = "Bike"
                }
            },
            TotalLength = new DistanceDto()
            {
                Length = 10,
                Unit = "Kilometers"
            },
            Status = "Draft",
            AuthorId = -1
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedTour).Result;

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
        var result = (OkResult)controller.Delete(-1);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedTour = dbContext.Tours.FirstOrDefault(i => i.Id == -1);
        storedTour.ShouldBeNull();

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





    private static TourController CreateController(IServiceScope scope, string userId = "-21")
    {
        var controller = new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
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
                new Claim(ClaimTypes.Role, "author")
        }, "mock"));

        return new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }



}
