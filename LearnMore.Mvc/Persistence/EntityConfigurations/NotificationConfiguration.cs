using LearnMore.Mvc.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LearnMore.Mvc.Persistence.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            HasRequired(n => n.Event);
        }
    }
}