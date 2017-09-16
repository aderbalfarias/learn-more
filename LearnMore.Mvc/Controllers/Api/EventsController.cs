using LearnMore.Mvc.Models;
using Microsoft.AspNet.Identity;
using System;
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
            var evt = _context.Events.Single(g => g.Id == id && g.OwnerId == userId);

            if (evt.IsCanceled)
                return NotFound();

            evt.IsCanceled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Event = evt,
                Type = NotificationType.GigCanceled
            };

            var attendees = _context.Attendances
                .Where(a => a.EventId == evt.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach (var attendee in attendees)
            {
                var userNotification = new UserNotification
                {
                    User = attendee,
                    Notification = notification
                };

                _context.UserNotifications.Add(userNotification);
            }

            _context.SaveChanges();


            _context.SaveChanges();

            return Ok();
        }
    }
}
