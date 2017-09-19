using LearnMore.Mvc.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LearnMore.Mvc.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => new { a.EventId, a.AttendeeId });
        }
    }
}