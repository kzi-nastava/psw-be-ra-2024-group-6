using Microsoft.Extensions.DependencyInjection;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Explorer.API.Controllers.Stakeholders;
using System.Collections.Generic;
using Explorer.Blog.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public;

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
                ImageId = -1,

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
            result.ImageId.ShouldBe(updatedEntity.ImageId);

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
                ImageId = null,
                Email = "k@gmail.com"
            };

            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            //assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
            
           
        }

        [Fact]
        public void add_follower()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateFollowerController(scope,-11);
            var userId = -12;

            var result = (ObjectResult)controller.AddFollower(userId);

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }
        [Fact]
        public void get_followers()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateFollowerController(scope,-23);
            var userId = -12;

            var result = ((ObjectResult)controller.GetFollowers(-23).Result)?.Value as List<PersonDto>;
            //var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourDto>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
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

        [Fact]
        public void Retrieves_most_popular_authors()
        {
            // Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateLandingPageController(scope);

            // Act
            var result = ((ObjectResult)controller.GetMostPopularAuthors().Result)?.Value as List<PersonDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
        }

        private static PersonController CreateController(IServiceScope scope)
        {
            return new PersonController(scope.ServiceProvider.GetRequiredService<IPersonService>(),scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

        private static PersonController CreateFollowerController(IServiceScope scope,int id)
        {
            return new PersonController(scope.ServiceProvider.GetRequiredService<IPersonService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext(id.ToString())
            };
        }

        private static LandingPageController CreateLandingPageController(IServiceScope scope)
        {
            return new LandingPageController(scope.ServiceProvider.GetRequiredService<ITourService>(),
                scope.ServiceProvider.GetRequiredService<IAuthorService>(),
                scope.ServiceProvider.GetRequiredService<ICheckpointService>(),
                scope.ServiceProvider.GetRequiredService<IRatingService>(),
                scope.ServiceProvider.GetRequiredService<IBlogService>())
            {
                ControllerContext = BuildContext("-2")
            };
        }
    }
}
