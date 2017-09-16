using LearnMore.Mvc.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace LearnMore.Mvc.Controllers.Api
{
    [Authorize]
    public class EventsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Events.Single(g => g.Id == id && g.OwnerId == userId);

            if (gig.IsCanceled)
                return NotFound();

            gig.IsCanceled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
