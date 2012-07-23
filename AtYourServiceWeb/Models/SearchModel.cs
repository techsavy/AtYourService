// -----------------------------------------------------------------------
// <copyright file="SearchModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class SearchModel
    {
        [Display(Name = "Looking For")]
        public string Terms { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string Location { get; set; }

        public byte? Type { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Sort By")]
        public string SortBy { get; set; }

        public static readonly string Date = "Date";
        public static readonly string Distance = "Distance";
        public static readonly string Views = "Views";

        public string GetSortField()
        {
            if ((new[] { Date, Distance, Views }).Contains(SortBy))
                return SortBy;

            return Date;
        }
    }
}