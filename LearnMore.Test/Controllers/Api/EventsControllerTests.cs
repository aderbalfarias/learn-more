using FluentAssertions;
using LearnMore.Mvc.Controllers.Api;
using LearnMore.Mvc.Core.Interfaces.Generics;
using LearnMore.Mvc.Core.Interfaces.Repositories;
using LearnMore.Mvc.Core.Models;
using LearnMore.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace LearnMore.Test.Controllers.Api
{
    [TestClass]
    public class EventsControllerTests
    {
        private EventsController _controller;
        private Mock<IEventRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IEventRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Events).Returns(_mockRepository.Object);

            _controller = new EventsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoEventWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_EventIsCanceled_ShouldReturnNotFound()
        {
            var evt = new Event();
            evt.Cancel();

            _mockRepository.Setup(r => r.GetEventWithAttendees(1)).Returns(evt);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersEvent_ShouldReturnUnauthorized()
        {
            var evt = new Event { OwnerId = _userId + "-" };

            _mockRepository.Setup(r => r.GetEventWithAttendees(1, _userId)).Returns(evt);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var evt = new Event { OwnerId = _userId };

            _mockRepository.Setup(r => r.GetEventWithAttendees(1, _userId)).Returns(evt);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }
    }
}
