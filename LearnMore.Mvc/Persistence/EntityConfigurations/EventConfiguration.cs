using LearnMore.Mvc.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LearnMore.Mvc.Persistence.EntityConfigurations
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            Property(g => g.OwnerId)
                .IsRequired();

            Property(g => g.GenreId)
                .IsRequired();

            Property(g => g.Venue)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(g => g.Attendances)
                .WithRequired(a => a.Event)
                .WillCascadeOnDelete(false);
        }
    }
}