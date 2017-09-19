using System;
using System.ComponentModel.DataAnnotations;

namespace LearnMore.Mvc.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Event Event { get; private set; }

        protected Notification()
        {
        }

        private Notification(NotificationType type, Event evt)
        {
            if (evt == null)
                throw new ArgumentNullException(nameof(evt));

            Type = type;
            Event = evt;
            DateTime = DateTime.Now;
        }

        public static Notification EventCreated(Event Event)
        {
            return new Notification(NotificationType.EventCreated, Event);
        }

        public static Notification EventUpdated(Event newEvent, DateTime originalDateTime, string originalVenue)
        {
            var notification =
                new Notification(NotificationType.EventUpdated, newEvent)
                {
                    OriginalDateTime = originalDateTime,
                    OriginalVenue = originalVenue
                };

            return notification;
        }

        public static Notification EventCanceled(Event Event)
        {
            return new Notification(NotificationType.EventCanceled, Event);
        }
    }
}