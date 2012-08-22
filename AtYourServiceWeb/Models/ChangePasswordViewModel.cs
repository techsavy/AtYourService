
namespace AtYourService.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long and not more than 32 characters.", MinimumLength = 6)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long and not more than 32 characters.", MinimumLength = 6)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        [Display(Name = "Confirm password")]
        public string RetypePassword { get; set; }
    }
}