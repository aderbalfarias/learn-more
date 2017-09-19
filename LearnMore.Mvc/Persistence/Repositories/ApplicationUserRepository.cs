using System.Collections.Generic;
using System.Linq;
using LearnMore.Mvc.Core.Interfaces.Repositories;
using LearnMore.Mvc.Core.Models;

namespace LearnMore.Mvc.Persistence.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetFollowee(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }
    }
}