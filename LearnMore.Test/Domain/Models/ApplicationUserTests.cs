using FluentAssertions;
using LearnMore.Mvc.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LearnMore.Test.Domain.Models
{
    [TestClass]
    public class ApplicationUserTests
    {
        [TestMethod]
        public void Notify_WhenCalled_ShouldAddTheNotification()
        {
            var user = new ApplicationUser();
            var notification = Notification.EventCanceled(new Event());

            user.Notify(notification);

            user.UserNotifications.Count.Should().Be(1);

            var userNotification = user.UserNotifications.First();
            userNotification.Notification.Should().Be(notification);
            userNotification.User.Should().Be(user);
        }
    }
}
