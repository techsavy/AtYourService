
namespace AtYourService.Web.Models
{
    public class CreateReviewModel
    {
        public int ServiceId { get; set; }

        public byte Score { get; set; }

        public string Body { get; set; }
    }
}