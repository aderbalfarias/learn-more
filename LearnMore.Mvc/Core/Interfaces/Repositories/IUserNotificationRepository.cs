using LearnMore.Mvc.Core.Models;
using System.Collections.Generic;

namespace LearnMore.Mvc.Core.Interfaces.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<UserNotification> GetUserNotificationsFor(string userId);
    }
}