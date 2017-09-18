using LearnMore.Mvc.Models;
using LearnMore.Mvc.Persistence;
using LearnMore.Mvc.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace LearnMore.Mvc.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public EventsController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var evts = _unitOfWork.Events.GetUpcomingEventsByOwner(userId);

            return View(evts);
        }

        public ActionResult Details(int id)
        {
            var evt = _unitOfWork.Events.GetEvent(id);

            if (evt == null)
                return HttpNotFound();

            var viewModel = new EventDetailsViewModel { Event = evt };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _unitOfWork
                    .Attendances
                    .GetAttendance(evt.Id, userId) != null;

                viewModel.IsFollowing = _unitOfWork
                    .Followings
                    .GetFollowing(evt.OwnerId, userId) != null;
            }

            return View("Details", viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = _unitOfWork.Events.GetEventsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Event I'm Attending",
                Attendances = _unitOfWork
                    .Attendances
                    .GetFutureAttendances(userId)
                    .ToLookup(a => a.EventId)
            };

            return View("Index", viewModel);
        }


        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Event"
            };

            return View("Form", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var evt = _unitOfWork.Events.GetEvent(id);

            if (evt == null)
                return HttpNotFound();

            if (evt.OwnerId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new EventFormViewModel
            {
                Heading = "Edit a Event",
                Id = evt.Id,
                Genres = _unitOfWork.Genres.GetGenres(),
                Date = evt.DateTime.ToString("d MMM yyyy"),
                Time = evt.DateTime.ToString("HH:mm"),
                Genre = evt.GenreId,
                Venue = evt.Venue
            };

            return View("Form", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("Form", viewModel);
            }

            var evt = new Event
            {
                OwnerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Events.Add(evt);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Events");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("Form", viewModel);
            }

            var evt = _unitOfWork.Events.GetEventWithAttendees(viewModel.Id);

            if (evt == null)
                return HttpNotFound();

            if (evt.OwnerId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            evt.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Events");
        }
    }
}