using FluentAssertions;
using LearnMore.Mvc.Controllers;
using LearnMore.Mvc.Core.Models;
using LearnMore.Mvc.IntegrationTests.Extensions;
using LearnMore.Mvc.Persistence;
using NUnit.Framework;
using System;
using System.Linq;


namespace LearnMore.Mvc.IntegrationTests.Controllers
{
    [TestFixture]
    public class EventsControllerTests
    {
        private EventsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new EventsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingEvents()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var evt = new Event { Owner = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Events.Add(evt);
            _context.SaveChanges();

            // Act
            //var result = _controller.Mine();

            // Assert
            _context.Entry(evt).Reload();
            evt.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            evt.Venue.Should().Be("venue");
            evt.GenreId.Should().Be(2);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGivenEvent()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var evt = new Event { Owner = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Events.Add(evt);
            _context.SaveChanges();

            // Act
            //var result = _controller.Update(new EventFormViewModel
            //{
            //    Id = evt.Id,
            //    Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
            //    Time = "20:00",
            //    Venue = "Venue",
            //    Genre = 2
            //});

            // Assert
            _context.Entry(evt).Reload();
            evt.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            evt.Venue.Should().Be("Venue");
            evt.GenreId.Should().Be(2);
        }
    }
}
