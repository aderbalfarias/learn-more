using System.Collections.Generic;
using LearnMore.Mvc.Core.Models;

namespace LearnMore.Mvc.Core.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Event GetEvent(int eventId);
        IEnumerable<Event> GetUpcomingEventsByOwner(string ownerId);
        IEnumerable<Event> GetEventsUpcoming(string query);
        Event GetEventWithAttendees(int eventId);
        IEnumerable<Event> GetEventsUserAttending(string userId);
        void Add(Event evt);
    }
}