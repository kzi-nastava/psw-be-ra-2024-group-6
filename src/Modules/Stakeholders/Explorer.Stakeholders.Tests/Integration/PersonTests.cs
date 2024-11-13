using Microsoft.Extensions.DependencyInjection;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Explorer.API.Controllers.Stakeholders;

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

            var storedEntity = dbContext.People.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Email.ShouldBe(updatedEntity.Email);
            storedEntity.Name.ShouldBe(updatedEntity.Name);
            var oldEntity = dbContext.People.FirstOrDefault(i => i.Name == "Ana");
            oldEntity.ShouldBeNull();
            
        }


        [Fact]
        public void Update_fails_bad_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new PersonDto
            {
                Id = -1000,
                UserId = -11, 
                Name = "tfw",
                Description = "tfw",
                Surname = "ship it",
                Motto   = "ok",
                PictureURL = "pic",
                Email = "k@gmail.com"
            };

            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            //assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
            
           
        }

        [Fact]
        public void gets_bad_user_id()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var userId = 0;

            var result = (ObjectResult)controller.GetByUserId(userId).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        

        private static PersonController CreateController(IServiceScope scope)
        {
            return new PersonController(scope.ServiceProvider.GetRequiredService<IPersonService>(),scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
