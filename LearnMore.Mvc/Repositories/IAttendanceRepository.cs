using LearnMore.Mvc.Models;
using System.Collections.Generic;

namespace LearnMore.Mvc.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendance(int eventId, string userId);
    }
}