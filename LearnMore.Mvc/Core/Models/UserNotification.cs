using System;

namespace LearnMore.Mvc.Core.Models
{
    public class UserNotification
    {
        public string UserId { get; set; }

        public int NotificationId { get; set; }

        public ApplicationUser User { get; set; }

        public Notification Notification { get; set; }

        public bool IsRead { get; private set; }

        protected UserNotification()
        {
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            User = user;
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}