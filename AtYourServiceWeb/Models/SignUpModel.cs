// -----------------------------------------------------------------------
// <copyright file="SignUpViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SignUpViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long and not more than 32 characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Confirm password")]
        public string RetypePassword { get; set; }

        [Required]
        [Display(Name = "Came here though")]
        public int? Source { get; set; }

        [StringLength(1000)]
        [Display(Name = "About Me")]
        public string Brag { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}