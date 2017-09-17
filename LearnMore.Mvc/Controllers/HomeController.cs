using LearnMore.Mvc.Models;
using LearnMore.Mvc.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LearnMore.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string searchTerm = null)
        {
            var upcomingEvents = _context.Events
                .Include(g => g.Owner)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                upcomingEvents = upcomingEvents
                    .Where(g =>
                        g.Owner.Name.Contains(searchTerm) ||
                        g.Genre.Name.Contains(searchTerm) ||
                        g.Venue.Contains(searchTerm));
            }

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = searchTerm
            };

            return View("Index", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}