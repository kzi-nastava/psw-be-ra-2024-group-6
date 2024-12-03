using Microsoft.Extensions.DependencyInjection;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;
using Explorer.API.Controllers.Stakeholders;
using System.Linq;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class NotificationTests : BaseStakeholdersIntegrationTest
    {
        public NotificationTests(StakeholdersTestFactory factory) : base(factory) { }


        [Fact]
        public void SendNotification_Success()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();


            // Priprema NotificationCreateDto objekta
            var notificationDto = new NotificationCreateDto
            {
                Content = "Test Notification",
                Type = "None" // Pretpostavljamo da je "Tour" validna vrednost za tip
            };

            // Act
            var result = (ObjectResult)controller.SendNotification(notificationDto);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldBe("Notifications sent successfully");

            // Provera u bazi da li je notifikacija dodata
            var addedNotifications = dbContext.Notifications.Where(n => n.Content == "Test Notification").ToList();
            addedNotifications.ShouldNotBeEmpty();
            addedNotifications.Count.ShouldBe(2);
            addedNotifications.All(n => n.SenderPersonId == -23).ShouldBeTrue();
            addedNotifications.All(n => n.Type == NotificationType.None).ShouldBeTrue(); // Provera tipa
        }
        [Fact]
        public void GetNotifications_ReturnsNotificationsForUser()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            int userId = -11; // Ovo je ID korisnika koji prima notifikaciju

            var result = ((ObjectResult)controller.GetNotifications(userId).Result)?.Value as List<NotificationDto>;

            result.ShouldNotBeNull();
            result.Count.ShouldBe(1); 

            // Provera sadržaja notifikacije
            var notification = result.First();
            notification.Content.ShouldBe("gas");
            notification.Type.ShouldBe("Tour"); 
            notification.LinkId.ShouldBe(2);
            notification.ReceiverPersonId.ShouldBe(-11);
            notification.SenderPersonId.ShouldBe(-12);
            notification.IsRead.ShouldBeFalse();
        }


        [Fact]
        public void MarkNotificationAsRead_Success()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            int notificationId = -1;

            var result = (ObjectResult)controller.MarkNotificationAsRead(notificationId);

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldBe("Notifikacija označena kao pročitana");

            var updatedNotification = dbContext.Notifications.FirstOrDefault(n => n.Id == notificationId);
            updatedNotification.ShouldNotBeNull();
            updatedNotification.IsRead.ShouldBeTrue();
        }



        private static NotificationController CreateController(IServiceScope scope)
        {
            return new NotificationController(scope.ServiceProvider.GetRequiredService<IPersonService>(),
                scope.ServiceProvider.GetRequiredService<INotificationService>())
            {
                ControllerContext = BuildContext("-23") 
            };
        }
    }
}