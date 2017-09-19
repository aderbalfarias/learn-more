using LearnMore.Mvc.Core.Interfaces.Repositories;
using LearnMore.Mvc.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnMore.Mvc.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Event.DateTime > DateTime.Now)
                .ToList();
        }

        public Attendance GetAttendance(int eventId, string userId)
        {
            return _context.Attendances
                .SingleOrDefault(a => a.EventId == eventId
                    && a.AttendeeId == userId);
        }

        public void Add(Attendance entity)
        {
            _context.Attendances.Add(entity);
        }

        public void Remove(Attendance entity)
        {
            _context.Attendances.Remove(entity);
        }
    }
}