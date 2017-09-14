using LearnMore.Mvc.Models;
using LearnMore.Mvc.ViewModels;
using Microsoft.AspNet.Identity;
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
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var evts = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Event)
                .Include(g => g.Owner)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new EventsViewModel()
            {
                UpcomingEvent = evts,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Events I'm Attending"
            };

            return View("Index", viewModel);
        }


        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventsFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventsFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
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

            return RedirectToAction("Index", "Home");
        }
    }
}