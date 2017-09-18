using LearnMore.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LearnMore.Mvc.Repositories
{
    public class EventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
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

        public Event GetEventWithAttendees(int eventId)
        {
            return _context.Events
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == eventId);
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