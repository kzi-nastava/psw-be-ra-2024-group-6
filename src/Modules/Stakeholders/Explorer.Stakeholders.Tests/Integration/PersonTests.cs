using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Stakeholders;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class PersonTests : BaseStakeholdersIntegrationTest
    {
        public PersonTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]

        public void Updates()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new PersonDto
            {
                Id = -11,
                UserId = -11,
                Name = "Name updated",
                Surname = "Surname updated",
                Description = "Description updated",
                Motto = "Motto updated",
                Email = "updated@gmail.com",
                PictureURL = "updatedpicture.png"

            };

            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as PersonDto;

            result.ShouldNotBeNull();
            result.Id.ShouldBe(-11);
            result.UserId.ShouldBe(-11);
            result.Name.ShouldBe(updatedEntity.Name);
            result.Description.ShouldBe(updatedEntity.Description);
            result.Surname.ShouldBe(updatedEntity.Surname);
            result.Email.ShouldBe(updatedEntity.Email);
            result.Motto.ShouldBe(updatedEntity.Motto);
            result.PictureURL.ShouldBe(updatedEntity.PictureURL);

            
        }

        private static PersonController CreateController(IServiceScope scope)
        {
            return new PersonController(scope.ServiceProvider.GetRequiredService<IPersonService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
