using LearnMore.Mvc.Models;
using LearnMore.Mvc.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace LearnMore.Mvc.Controllers
{
    public class FolloweesController : Controller
    {

        private readonly UnitOfWork _unitOfWork;

        public FolloweesController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var owner = _unitOfWork
                .ApplicationUsers
                .GetFollowee(userId);

            return View(owner);
        }
    }
}