
namespace AtYourService.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserModel
    {
        [StringLength(1000)]
        [Display(Name = "About Me")]
        public string Brag { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}