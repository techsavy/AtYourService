

namespace AtYourService.Web.Models
{
    using System;

    public class ServicesByUserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime LastActiceDate { get; set; }

        public int AdCount { get; set; }
    }
}