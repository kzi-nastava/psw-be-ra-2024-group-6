
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author_Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Blog.Tests.Integration
{
    [Collection("Sequential")]
    public class CommentCommandTests : BaseBlogIntegrationTest
    {
        public CommentCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new CommentDto
            {
                Text = "Solidno",
                UserId = 1,
                CreationDate = DateTime.Now.ToUniversalTime(),
                UpdateDate = DateTime.Now.ToUniversalTime(),


            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as CommentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Text.ShouldBe(newEntity.Text);
            result.CreationDate.ShouldBe(newEntity.CreationDate);
            result.UpdateDate.ShouldBe(newEntity.UpdateDate);
            result.UserId.ShouldBe(1);


            // Assert - Database
            var storedEntity = dbContext.Comment.FirstOrDefault(i => i.Text == newEntity.Text);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }


        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CommentDto
            {
                Text = ""
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
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var updatedEntity = new CommentDto
            {
                Id = 1,
                Text = "Lose",
                UserId = 1,
                UpdateDate = DateTime.Now.ToUniversalTime(),
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as CommentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
            result.Text.ShouldBe(updatedEntity.Text);
            result.UpdateDate.ShouldBe(updatedEntity.UpdateDate);

            // Assert - Database
            var storedEntity = dbContext.Comment.FirstOrDefault(i => i.Id == 1 && i.Text == "Lose");
            storedEntity.ShouldNotBeNull();
            storedEntity.CreationDate.ShouldBe(updatedEntity.CreationDate);
            var oldEntity = dbContext.Comment.FirstOrDefault(i => i.Id == 1 && i.Text == "Solidno");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new CommentDto
            {
                Id = -1,
                Text = "Solidno",
                UserId = 1,
                CreationDate = DateTime.Now.ToUniversalTime(),
                UpdateDate = DateTime.Now.ToUniversalTime(),
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        private static BlogController CreateController(IServiceScope scope)
        {
            return new BlogController(scope.ServiceProvider.GetRequiredService<ICommentService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
