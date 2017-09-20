using FluentAssertions;
using LearnMore.Mvc.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LearnMore.Test.Domain.Models
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCanceledToTrue()
        {
            var evt = new Event();

            evt.Cancel();

            evt.IsCanceled.Should().BeTrue();
        }

        [TestMethod]
        public void Cancel_WhenCalled_EachAttendeeShouldHaveANotification()
        {
            var evt = new Event();
            evt.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "1" } });

            evt.Cancel();

            var attendees = evt.Attendances.Select(a => a.Attendee).ToList();
            attendees[0].UserNotifications.Count.Should().Be(1);
        }
    }
}
