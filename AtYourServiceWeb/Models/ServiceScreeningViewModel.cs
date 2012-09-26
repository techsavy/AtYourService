

namespace AtYourService.Web.Models
{
    using System;
    using Domain.Adverts;

    public class ServiceScreeningViewModel
    {
        public Service Service { get; set; }

        public Series<DateTime, int> Views { get; set; }

        public Series<DateTime, int> Impressions { get; set; }
    }
}