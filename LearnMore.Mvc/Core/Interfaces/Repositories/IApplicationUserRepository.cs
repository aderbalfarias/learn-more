using System.Collections.Generic;
using LearnMore.Mvc.Core.Models;

namespace LearnMore.Mvc.Core.Interfaces.Repositories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetFollowee(string userId);
    }
}