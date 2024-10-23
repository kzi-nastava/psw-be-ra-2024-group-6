using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class RequiredEquipmentQueryTests : BaseToursIntegrationTest
    {
        public RequiredEquipmentQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all_for_tour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAllByTourId(-1).Result)?.Value as ICollection<RequiredEquipmentDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        private static RequiredEquipmentController CreateController(IServiceScope scope)
        {
            return new RequiredEquipmentController(
                scope.ServiceProvider.GetRequiredService<IRequiredEquipmentService>(),
                scope.ServiceProvider.GetRequiredService<IEquipmentService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
