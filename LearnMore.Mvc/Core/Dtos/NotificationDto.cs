using System;
using LearnMore.Mvc.Core.Models;

namespace LearnMore.Mvc.Core.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public EventDto Event { get; set; }
    }
}