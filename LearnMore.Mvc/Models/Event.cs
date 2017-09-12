using System;
using System.ComponentModel.DataAnnotations;

namespace LearnMore.Mvc.Models
{
    public class Event
    {
        public int Id { get; set; }

        public ApplicationUser Owner { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }
    }
}