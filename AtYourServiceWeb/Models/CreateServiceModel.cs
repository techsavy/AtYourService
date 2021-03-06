﻿// -----------------------------------------------------------------------
// <copyright file="CreateServiceModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Util;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CreateServiceModel
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Body { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Type")]
        public int Type { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [ImageFile]
        public HttpPostedFileBase Image { get; set; }
    }
}