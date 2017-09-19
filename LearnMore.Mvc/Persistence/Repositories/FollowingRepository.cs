using LearnMore.Mvc.Core.Interfaces.Repositories;
using LearnMore.Mvc.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace LearnMore.Mvc.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string followerId, string followeeId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FolloweeId == followeeId
                    && f.FollowerId == followerId);
        }

        public IEnumerable<ApplicationUser> GetFollowee(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }

        public void Add(Following entity)
        {
            _context.Followings.Add(entity);
        }

        public void Remove(Following entity)
        {
            _context.Followings.Remove(entity);
        }
    }
}