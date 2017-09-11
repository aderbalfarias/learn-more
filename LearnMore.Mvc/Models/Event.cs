using System;
using System.ComponentModel.DataAnnotations;

namespace LearnMore.Mvc.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser Owner { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public Genre Genre { get; set; }
    }
}