using System.ComponentModel.DataAnnotations;

namespace secure_user_authentication_system.Models
{
    public class SubmitForm
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [Display(Name = "Your Message")]
        public string Message { get; set; }
    }
}
