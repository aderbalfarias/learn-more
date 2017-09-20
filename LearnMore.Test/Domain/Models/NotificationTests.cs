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

            // Again, here, we have two assertions, but that doesn't mean we're
            // violating the single responsibility principle. We're verifying 
            // one logical fact: that upon calling Notification.EventCanceled()
            // we'll get a notification object for the canceled evt. This notification
            // object should be in the right state, meaning its type should be
            // EventCanceled and its evt should be the evt for each we created 
            // the notification. 

            notification.Type.Should().Be(NotificationType.EventCanceled);
            notification.Event.Should().Be(evt);
        }
    }
}
