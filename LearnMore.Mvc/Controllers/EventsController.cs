using LearnMore.Mvc.Models;
using LearnMore.Mvc.ViewModels;
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

        // GET: Event
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }
    }
}