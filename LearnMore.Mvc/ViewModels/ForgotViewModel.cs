using System.ComponentModel.DataAnnotations;

namespace LearnMore.Mvc.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}