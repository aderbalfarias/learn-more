using LearnMore.Mvc.Models;
using System.Collections.Generic;

namespace LearnMore.Mvc.ViewModels
{
    public class EventsViewModel
    {
        public IEnumerable<Event> UpcomingEvent { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
    }
}