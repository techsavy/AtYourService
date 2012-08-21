using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtYourService.Web.Models
{
    public class ReviewsModel
    {
        public int ServiceId { get; set; }

        public int? Page { get; set; }

        public bool AllowCreate { get; set; }
    }
}