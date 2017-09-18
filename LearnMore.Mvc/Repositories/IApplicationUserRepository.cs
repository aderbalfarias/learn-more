using LearnMore.Mvc.Models;
using System.Collections.Generic;

namespace LearnMore.Mvc.Repositories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetFollowee(string userId);
    }
}