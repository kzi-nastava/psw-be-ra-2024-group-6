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

namespace Explorer.Tours.Tests.Integration.Tours;

[Collection("Sequential")]
    public class TourCommandTests : BaseToursIntegrationTest
    {
    public TourCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourDto
        {
            Name = "Pariske tura",
            Description = "Tura kroz Pariz.",
            Difficulty = "Medium",
            Tags = new List<string> { "priroda", "drustvo" },
            Cost = 0.0,
            Status = "Draft",
            AuthorId = -21
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var newEntity = new TourDto()
        {
            Description = "Invalid data without a name"
        };
        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result);


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
        var updatedTour = new TourDto
        {
            Id = -2,
            Name = "Promjenjeno ime",
            Description = "Promjenjena deskripcija.",
            Difficulty = "Hard",
            Tags = { "promjenjen1", "promjenjen2" },
            Cost = 5.0,
            Status = "Closed",
            AuthorId = -1
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
        result.Cost.ShouldBe(updatedTour.Cost);
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
            Tags = { "promjenjen1", "promjenjen2" },
            Cost = 5.0,
            Status = "Closed",
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
