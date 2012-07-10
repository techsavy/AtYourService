using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtYourService.Web.Models
{
    public class ServiceSerializeInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}