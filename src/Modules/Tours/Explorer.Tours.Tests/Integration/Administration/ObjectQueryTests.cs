using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos.ObjectDtos;
using Explorer.Tours.API.Public.Administration;
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
public class ObjectQueryTests : BaseToursIntegrationTest
{
    public ObjectQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ObjectDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(2);
        result.TotalCount.ShouldBe(2);
    }

    private static ObjectController CreateController(IServiceScope scope)
    {
        return new ObjectController(scope.ServiceProvider.GetRequiredService<IObjectService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
