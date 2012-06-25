// -----------------------------------------------------------------------
// <copyright file="CreateServiceModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CreateServiceModel
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}