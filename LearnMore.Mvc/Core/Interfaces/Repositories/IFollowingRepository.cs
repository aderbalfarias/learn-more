using LearnMore.Mvc.Core.Models;
using System.Collections.Generic;

namespace LearnMore.Mvc.Core.Interfaces.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followerId, string followeeId);
        IEnumerable<ApplicationUser> GetFollowee(string userId);
        void Add(Following entity);
        void Remove(Following entity);
    }
}