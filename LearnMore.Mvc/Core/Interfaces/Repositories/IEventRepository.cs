using LearnMore.Mvc.Core.Models;
using System.Collections.Generic;

namespace LearnMore.Mvc.Core.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Event GetEvent(int eventId);
        IEnumerable<Event> GetUpcomingEventsByOwner(string ownerId);
        IEnumerable<Event> GetEventsUpcoming(string query);
        Event GetEventWithAttendees(int eventId);
        Event GetEventWithAttendees(int eventId, string userId);
        IEnumerable<Event> GetEventsUserAttending(string userId);
        void Add(Event evt);
    }
}