using FluentAssertions;
using LearnMore.Mvc.Core.Interfaces.Generics;
using LearnMore.Mvc.Core.Models;
using LearnMore.Mvc.Persistence.Repositories;
using LearnMore.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace LearnMore.Test.Persistence.Repositories
{
    [TestClass]
    public class EventRepositoryTests
    {
        private EventRepository _repository;
        private Mock<DbSet<Event>> _mockEvents;
        private Mock<DbSet<Attendance>> _mockAttendances;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockEvents = new Mock<DbSet<Event>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Events).Returns(_mockEvents.Object);
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);

            _repository = new EventRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingEventsByOwner_EventIsInThePast_ShouldNotBeReturned()
        {
            var evt = new Event() { DateTime = DateTime.Now.AddDays(-1), OwnerId = "1" };

            _mockEvents.SetSource(new[] { evt });

            var evts = _repository.GetUpcomingEventsByOwner("1");

            evts.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingEventsByOwner_EventIsCanceled_ShouldNotBeReturned()
        {
            var evt = new Event() { DateTime = DateTime.Now.AddDays(1), OwnerId = "1" };
            evt.Cancel();

            _mockEvents.SetSource(new[] { evt });

            var evts = _repository.GetUpcomingEventsByOwner("1");

            evts.Should().BeEmpty();

        }

        [TestMethod]
        public void GetUpcomingEventsByOwner_EventIsForADifferentOwner_ShouldNotBeReturned()
        {
            var evt = new Event() { DateTime = DateTime.Now.AddDays(1), OwnerId = "1" };

            _mockEvents.SetSource(new[] { evt });

            var evts = _repository.GetUpcomingEventsByOwner(evt.OwnerId + "-");

            evts.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingEventsByOwner_EventIsForTheGivenOwnerAndIsInTheFuture_ShouldBeReturned()
        {
            var evt = new Event() { DateTime = DateTime.Now.AddDays(1), OwnerId = "1" };

            _mockEvents.SetSource(new[] { evt });

            var evts = _repository.GetUpcomingEventsByOwner(evt.OwnerId);

            evts.Should().Contain(evt);

        }

        // This test helped me catch a bug in GetEventsUserAttending() method. 
        // It used to return evts from the past. 
        [TestMethod]
        public void GetEventsUserAttending_EventIsInThePast_ShouldNotBeReturned()
        {
            var evt = new Event() { DateTime = DateTime.Now.AddDays(-1) };
            var attendance = new Attendance { Event = evt, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var evts = _repository.GetEventsUserAttending(attendance.AttendeeId);

            evts.Should().BeEmpty();
        }

        [TestMethod]
        public void GetEventsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
        {
            var evt = new Event() { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Event = evt, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var evts = _repository.GetEventsUserAttending(attendance.AttendeeId + "-");

            evts.Should().BeEmpty();
        }

        [TestMethod]
        public void GetEventsUserAttending_UpcomingEventUserAttending_ShouldBeReturned()
        {
            var evt = new Event() { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Event = evt, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var evts = _repository.GetEventsUserAttending(attendance.AttendeeId);

            evts.Should().Contain(evt);
        }
    }
}
