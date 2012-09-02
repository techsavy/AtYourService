
namespace AtYourService.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewModel
    {
        public int ServiceId { get; set; }

        [Required]
        public byte Score { get; set; }

        [StringLength(5000, MinimumLength = 20)]
        [Required]
        public string Body { get; set; }
    }
}