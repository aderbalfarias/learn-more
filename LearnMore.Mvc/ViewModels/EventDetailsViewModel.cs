using LearnMore.Mvc.Models;

namespace LearnMore.Mvc.ViewModels
{
    public class EventDetailsViewModel
    {
        public Event Event { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}