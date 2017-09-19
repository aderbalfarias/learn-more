using LearnMore.Mvc.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace LearnMore.Mvc.Persistence.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(f => new { f.FollowerId, f.FolloweeId });
        }
    }
}