using System.Collections.Generic;
using LearnMore.Mvc.Core.Models;

namespace LearnMore.Mvc.Core.Interfaces.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followerId, string followeeId);
        IEnumerable<ApplicationUser> GetFollowee(string userId);
    }
}