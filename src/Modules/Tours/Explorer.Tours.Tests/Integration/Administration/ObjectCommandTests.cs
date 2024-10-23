using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
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

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Sequential")]
public class ObjectCommandTests : BaseToursIntegrationTest
{
    public ObjectCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new ObjectDto
        {
            Id = 50,
            Name = "Women only WC",
            ImageUrl = "image/url",
            Description = "You can relax here ladies",
            Category = "WC",
            LocationId = 3,
            TourId = -2
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ObjectDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);

        // Assert - Database
        var storedEntity = dbContext.Objects.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new ObjectDto
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
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedEntity = new ObjectDto
        {
            Id = 1,
            Name = "Zar Mance",
            ImageUrl = "/putanja",
            Description = "Predaleko je",
            Category = "Restaurant",
            LocationId = 1,
            TourId = -2
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ObjectDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Name.ShouldBe(updatedEntity.Name);
        result.ImageUrl.ShouldBe(updatedEntity.ImageUrl);
        result.Description.ShouldBe(updatedEntity.Description);
        result.Category.ShouldBe(updatedEntity.Category);
        result.LocationId.ShouldBe(updatedEntity.LocationId);
        result.TourId.ShouldBe(updatedEntity.TourId);

        // Assert - Database
        var storedEntity = dbContext.Objects.FirstOrDefault(i => i.Name == "Zar Mance");
        storedEntity.ShouldNotBeNull();
        storedEntity.Description.ShouldBe(updatedEntity.Description);
        var oldEntity = dbContext.Objects.FirstOrDefault(i => i.Name == "Objekat1");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new ObjectDto
        {
            Id = -1000,
            Name = "Test",
            ImageUrl = "/test",
            Description = "test",
            Category = "WC",
            LocationId = 4,
            TourId = 5
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Gets_ByTour_Id()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        //Act
        var result = ((ObjectResult)controller.GetByTourId(-3).Result)?.Value as List<ObjectReadDto>;

        //Assert
        result.Count.ShouldBe(1);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(2);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.Objects.FirstOrDefault(i => i.Id == 2);
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

    private static ObjectController CreateController(IServiceScope scope)
    {
        return new ObjectController(scope.ServiceProvider.GetRequiredService<IObjectService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
