using LearnMore.Mvc.Core.Models;
using System.Collections.Generic;

namespace LearnMore.Mvc.Core.Interfaces.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendance(int eventId, string userId);
        void Add(Attendance entity);
        void Remove(Attendance entity);
    }
}