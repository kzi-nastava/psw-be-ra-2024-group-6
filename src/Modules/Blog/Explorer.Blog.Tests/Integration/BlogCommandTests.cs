using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author_Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.Blogs;
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
    public class BlogCommandTests : BaseBlogIntegrationTest
    {
        public BlogCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new BlogDto
            {
                Title = "Moji utisci o Rimu",
                Description = "Obožavam Rim. Najbolji grad ikada!!!!",
                UserId = -1,
                Status = "Published",
                CreatedAt = DateTime.Now.ToUniversalTime(), 
                Pictures = new List<BlogPictureDto>(),
                Tags = new List<string> { "Novi Sad", "Beograd", "Torino" }
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BlogDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Description.ShouldBe(newEntity.Description);
            result.Title.ShouldBe(newEntity.Title);
            result.Status.ShouldBe(newEntity.Status);
            result.UserId.ShouldBe(-1);
            result.Tags.ShouldNotBeNull();
            result.Tags.Count.ShouldBe(newEntity.Tags.Count);

            result.Tags[0].ShouldBe("Novi Sad");
            result.Tags[1].ShouldBe("Beograd");
            result.Tags[2].ShouldBe("Torino");

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Description == newEntity.Description);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new BlogDto
            {
                
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
            var updatedEntity = new BlogDto
            {
                Id = -1,
                Title = "Moji utisci o Rimu",
                Description = "Obožavam Rim. Najbolji grad ikada!!!!",
                UserId = 1,
                Status = "Closed",
                Pictures = new List<BlogPictureDto>()
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as BlogDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Title.ShouldBe(updatedEntity.Title);
            result.Description.ShouldBe(updatedEntity.Description);

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Id == -1 && i.Status == Status.Closed);
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Blogs.FirstOrDefault(i => i.Id == -1 && i.Status == Status.Draft);
            oldEntity.ShouldBeNull();
        }
        
        [Fact]
        public void GetOne()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = ((ObjectResult)controller.GetBlogDetails(-1).Result)?.Value as BlogDetailsDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Comments.ShouldNotBeEmpty();

        }

        [Fact]
        public void Retrieves_blogs_by_tag()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var tag = "TORINO"; // The tag we want to search for

            // Act
            var result = ((ObjectResult)controller.GetBlogsByTag(tag).Result)?.Value as List<BlogHomeDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0); // Ensure there are blogs with this tag
            result.All(blog => blog.Tags.Any(t => t.Equals(tag, StringComparison.OrdinalIgnoreCase))).ShouldBeTrue();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new BlogDto
            {
                Id = -10000,
                Title = "Moji utisci o Rimu",
                Description = "Obožavam Rim. Najbolji grad ikada!!!!",
                UserId = 1,
                Status = "Closed",
                Pictures = new List<BlogPictureDto>()
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        } 

        private static BlogController CreateController(IServiceScope scope)
        {
            return new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>())
            {
                ControllerContext = BuildContext("-1")
            };
        } 
    } 
}
