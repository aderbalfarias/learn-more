using LearnMore.Mvc.Models;
using System.Collections.Generic;

namespace LearnMore.Mvc.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followerId, string followeeId);
        IEnumerable<ApplicationUser> GetFollowee(string userId);
    }
}