using LearnMore.Mvc.Models;
using LearnMore.Mvc.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LearnMore.Mvc.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var evts = _context.Events
                .Where(g =>
                    g.OwnerId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();

            return View(evts);
        }

        public ActionResult Details(int id)
        {
            var evt = _context.Events
                .Include(g => g.Owner)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);

            if (evt == null)
                return HttpNotFound();

            var viewModel = new EventDetailsViewModel { Event = evt };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.EventId == evt.Id && a.AttendeeId == userId);

                viewModel.IsFollowing = _context.Followings
                    .Any(f => f.FolloweeId == evt.OwnerId && f.FollowerId == userId);
            }

            return View("Details", viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var evts = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Event)
                .Include(g => g.Owner)
                .Include(g => g.Genre)
                .ToList();

            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Event.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.EventId);

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = evts,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Event I'm Attending",
                Attendances = attendances
            };

            return View("Index", viewModel);
        }


        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Event"
            };

            return View("Form", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var evt = _context.Events.Single(g => g.Id == id && g.OwnerId == userId);

            var viewModel = new EventFormViewModel
            {
                Heading = "Edit a Event",
                Id = evt.Id,
                Genres = _context.Genres.ToList(),
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
                viewModel.Genres = _context.Genres.ToList();
                return View("Form", viewModel);
            }

            var evt = new Event
            {
                OwnerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Events.Add(evt);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Form", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var evt = _context.Events
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.OwnerId == userId);

            evt.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }
    }
}