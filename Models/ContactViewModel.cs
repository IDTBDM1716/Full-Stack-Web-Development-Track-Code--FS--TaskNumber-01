using System.ComponentModel.DataAnnotations;

namespace secure_user_authentication_system.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [Display(Name = "Your Message")]
        public string Message { get; set; }
    }
}
