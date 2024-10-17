using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.RatingTesting;

[Collection("Sequential")]
public class RatingCommandTests : BaseStakeholdersIntegrationTest
{
    public RatingCommandTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var newEntity = new RatingDto
        {
            UserId = 1,
            StarRating = StarRating.Poor,
            Comment = "Turisticka agencija je ocajna.",
            PostedAt = DateTime.UtcNow
        };

        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as RatingDto;

        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.StarRating.ShouldBe(newEntity.StarRating);

        var storedEntity = dbContext.Ratings.FirstOrDefault(i => i.Id == result.Id);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new RatingDto
        {
            UserId = 1,
            StarRating = StarRating.Poor,
            Comment = "nista.",
            PostedAt = DateTime.UtcNow
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
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var updatedEntity = new RatingDto
        {
            Id = -1,
            UserId = 1,
            Comment = "glupost",
            PostedAt = DateTime.UtcNow,
            StarRating = StarRating.Poor
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as RatingDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Comment.ShouldBe(updatedEntity.Comment);
        result.StarRating.ShouldBe(updatedEntity.StarRating);

        // Assert - Database
        var storedEntity = dbContext.Ratings.FirstOrDefault(i => i.Comment == "glupost");
        storedEntity.ShouldNotBeNull();
        storedEntity.Comment.ShouldBe(updatedEntity.Comment);
        var oldEntity = dbContext.Ratings.FirstOrDefault(i => i.Comment == "nije to toliko strasno");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new RatingDto
        {
            Id = -1000,
            Comment = "Test"
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

     [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

        // Act
        var result = (OkResult)controller.Delete(-2);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.Ratings.FirstOrDefault(i => i.Id == -2);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static RatingController CreateController(IServiceScope scope)
    {
        return new RatingController(scope.ServiceProvider.GetRequiredService<IRatingService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}