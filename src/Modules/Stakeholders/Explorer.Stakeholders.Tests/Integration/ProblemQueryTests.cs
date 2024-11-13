using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Tests;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Public;
using Explorer.API.Controllers.Stakeholders;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class ProblemQueryTests : BaseStakeholdersIntegrationTest
    {
        public ProblemQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateProblemController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ProblemDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        [Fact]
        public void SendMessageNotification()
        {
            int userId = -22;
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var problemController = CreateProblemController(scope);
            var notificationController = CreateNotificationController(scope);

            var problem = new ProblemWithMessageDto
            {
                Problem = new ProblemDto
                {
                    Id = 1,
                    Category = "category",
                    Priority = "priority",
                    Date = DateTime.Now,
                    Description = "description",
                    TourId = -1,
                    TouristId = userId,
                    IsClosed = false,
                    IsResolved = false,
                    DueDate = DateTime.Today.AddDays(5),
                    Messages = new List<ProblemMessageDto>
                    {
                        new ProblemMessageDto
                        {
                            Content = "content",
                            SenderId = userId,
                            CreationDate = DateTime.Now
                        }
                    }
                },
                Message = new ProblemMessageDto
                {
                    Content = "Bad tour",
                    SenderId = userId,
                    CreationDate = DateTime.Now
                }
            };

            // Act
            var problemResult = ((ObjectResult)problemController.SendMessage(problem).Result)?.Value as ProblemDto;
            var notificationResult = ((ObjectResult)notificationController.GetNotifications(userId).Result)?.Value as List<NotificationDto>;

            // Assert
            notificationResult[0].Content.ShouldBe("Bad tour");

        }

        private static ProblemController CreateProblemController(IServiceScope scope)
        {
            return new ProblemController(scope.ServiceProvider.GetRequiredService<IProblemService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
        private static NotificationController CreateNotificationController(IServiceScope scope)
        {
            return new NotificationController(scope.ServiceProvider.GetRequiredService<IPersonService>(), scope.ServiceProvider.GetRequiredService<INotificationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
