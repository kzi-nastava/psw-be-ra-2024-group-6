using Explorer.Tours.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.Infrastructure.Database;
using Explorer.API.Controllers.Stakeholders;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace Explorer.Blog.Tests.Integration
{
    [Collection("Sequential")]
    public class BlogQueryTests : BaseBlogIntegrationTest
    {
        public BlogQueryTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_paged_blogs()
        {
            // Arange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateLandingPageController(scope);

            // Act
            var result = ((ObjectResult)controller.GetBlogs(1, 2).Result)?.Value as List<BlogHomeDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }

        private static LandingPageController CreateLandingPageController(IServiceScope scope)
        {
            return new LandingPageController(scope.ServiceProvider.GetRequiredService<ITourService>(),
                scope.ServiceProvider.GetRequiredService<IAuthorService>(),
                scope.ServiceProvider.GetRequiredService<ICheckpointService>(),
<<<<<<< HEAD
                scope.ServiceProvider.GetRequiredService<Stakeholders.Core.UseCases.RatingService>(),
=======
                scope.ServiceProvider.GetRequiredService<IRatingService>(),
>>>>>>> development
                scope.ServiceProvider.GetRequiredService<IBlogService>(),scope.ServiceProvider.GetRequiredService<ITourSearchService>())
            {
                ControllerContext = BuildContext("-2")
            };
        }
    }
}
