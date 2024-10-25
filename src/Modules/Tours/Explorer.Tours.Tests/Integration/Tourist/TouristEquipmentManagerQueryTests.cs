using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
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
public class TouristEquipmentManagerQueryTests : BaseToursIntegrationTest
{
    public TouristEquipmentManagerQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all_by_tourist_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetTouristEquipments(2).Result)?.Value as List<TouristEquipmentManagerDto>;
        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);

    }

    private static TouristEquipmentManagerController CreateController(IServiceScope scope)
    {
        return new TouristEquipmentManagerController(scope.ServiceProvider.GetRequiredService<ITouristEquipmentManagerService>(), scope.ServiceProvider.GetService<IEquipmentService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
