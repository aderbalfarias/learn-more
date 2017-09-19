using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using LearnMore.Mvc.Core.Models;
using LearnMore.Mvc.Persistence;

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
            var evt = _context.Events
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == id && g.OwnerId == userId);

            if (evt.IsCanceled)
                return NotFound();

            evt.Cancel();

            _context.SaveChanges();

            return Ok();
        }
    }
}
