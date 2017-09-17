using System;

namespace LearnMore.Mvc.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public UserDto Owner { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public GenreDto Genre { get; set; }
    }
}