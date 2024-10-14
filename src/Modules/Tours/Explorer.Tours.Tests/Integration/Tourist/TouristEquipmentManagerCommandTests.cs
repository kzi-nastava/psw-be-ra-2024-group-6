using Explorer.API.Controllers.Tourist;
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

namespace Explorer.Tours.Tests.Integration.Tourist;

[Collection("Sequential")]
public class TouristEquipmentManagerCommandTests : BaseToursIntegrationTest
{
    public TouristEquipmentManagerCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TouristEquipmentManagerDto
        {
            TouristId = 2,
            EquipmentId = -3
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TouristEquipmentManagerDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.EquipmentId.ShouldBe(-3);

        // Assert - Database
        var storedEntity = dbContext.TouristEquipmentManagers.FirstOrDefault(i => i.TouristId == newEntity.TouristId && i.EquipmentId == newEntity.EquipmentId);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkObjectResult)controller.Delete(2, -1);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.TouristEquipmentManagers.FirstOrDefault(i => i.TouristId == 2 && i.EquipmentId == -1);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(1000, -1);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    private static TouristEquipmentManagerController CreateController(IServiceScope scope)
    {
        return new TouristEquipmentManagerController(scope.ServiceProvider.GetRequiredService<ITouristEquipmentManagerService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
