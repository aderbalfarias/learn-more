using FluentAssertions;
using LearnMore.Mvc.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearnMore.Test.Domain.Models
{
    [TestClass]
    public class NotificationTests
    {
        [TestMethod]
        public void EventCanceled_WhenCalled_ShouldReturnedANotificationForACanceledEvent()
        {
            var evt = new Event();

            var notification = Notification.EventCanceled(evt);

            notification.Type.Should().Be(NotificationType.EventCanceled);
            notification.Event.Should().Be(evt);
        }
    }
}
