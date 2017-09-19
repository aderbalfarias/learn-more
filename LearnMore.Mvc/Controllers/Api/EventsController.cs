using LearnMore.Mvc.Core.Interfaces.Generics;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace LearnMore.Mvc.Controllers.Api
{
    [Authorize]
    public class EventsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var evt = _unitOfWork.Events.GetEventWithAttendees(id, userId);

            if (evt == null || evt.IsCanceled)
                return NotFound();

            if (evt.OwnerId != userId)
                return Unauthorized();

            evt.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
