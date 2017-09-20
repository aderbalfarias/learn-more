using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LearnMore.Mvc.Core.Models
{
    public class Event
    {
        public int Id { get; set; }

        public bool IsCanceled { get; set; }

        public ApplicationUser Owner { get; set; }

        public string OwnerId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Event()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.EventCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.EventUpdated(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }
    }
}