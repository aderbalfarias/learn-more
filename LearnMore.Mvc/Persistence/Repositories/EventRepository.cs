using LearnMore.Mvc.Core.Interfaces.Generics;
using LearnMore.Mvc.Core.Interfaces.Repositories;
using LearnMore.Mvc.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LearnMore.Mvc.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IApplicationDbContext _context;

        public EventRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Event GetEvent(int eventId)
        {
            return _context.Events
                    .Include(g => g.Owner)
                    .Include(g => g.Genre)
                    .SingleOrDefault(g => g.Id == eventId);
        }

        public IEnumerable<Event> GetUpcomingEventsByOwner(string ownerId)
        {
            return _context.Events
                .Where(g =>
                    g.OwnerId == ownerId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Event> GetEventsUpcoming(string query)
        {
            var upcomingEvents = _context.Events
                .Include(g => g.Owner)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = upcomingEvents
                    .Where(g =>
                        g.Owner.Name.Contains(query) ||
                        g.Genre.Name.Contains(query) ||
                        g.Venue.Contains(query));
            }

            return upcomingEvents
                .ToList();
        }

        public Event GetEventWithAttendees(int eventId)
        {
            return _context.Events
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == eventId);
        }

        public Event GetEventWithAttendees(int eventId, string userId)
        {
            return _context.Events
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == eventId && g.OwnerId == userId);
        }

        public IEnumerable<Event> GetEventsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Event)
                .Include(g => g.Owner)
                .Include(g => g.Genre)
                .ToList();
        }

        public void Add(Event evt)
        {
            _context.Events.Add(evt);
        }
    }
}