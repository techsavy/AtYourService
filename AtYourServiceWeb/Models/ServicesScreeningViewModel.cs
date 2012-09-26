

namespace AtYourService.Web.Models
{
    using System;

    public class ServicesScreeningViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public int Count { get; set; }

        public DateTime FirstDate { get; set; }

        public DateTime LatestDate { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}