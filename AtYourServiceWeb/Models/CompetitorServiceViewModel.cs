
namespace AtYourService.Web.Models
{
    public class CompetitorServiceViewModel
    {
        public int OwnServiceId { get; set; }

        public string OwnServiceTitle { get; set; }

        public int OtherServiceId { get; set; }

        public string OtherServiceTitle { get; set; }

        public string Category { get; set; }

        public double Distance { get; set; }
    }
}