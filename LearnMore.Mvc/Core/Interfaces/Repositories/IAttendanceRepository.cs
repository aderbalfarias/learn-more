using System.Collections.Generic;
using LearnMore.Mvc.Core.Models;

namespace LearnMore.Mvc.Core.Interfaces.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendance(int eventId, string userId);
    }
}