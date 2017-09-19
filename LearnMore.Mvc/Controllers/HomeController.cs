using LearnMore.Mvc.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using LearnMore.Mvc.Core.Models;
using LearnMore.Mvc.Core.ViewModels;

namespace LearnMore.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public ActionResult Index(string searchTerm = null)
        {
            var upcomingEvents = _unitOfWork.Events.GetEventsUpcoming(searchTerm);

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Event I'm Attending",
                Attendances = _unitOfWork
                    .Attendances
                    .GetFutureAttendances(User.Identity.GetUserId())
                    .ToLookup(a => a.EventId),
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