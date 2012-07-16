// -----------------------------------------------------------------------
// <copyright file="SearchModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Models
{
    using System.Linq;

    public class SearchModel
    {
        public string Terms { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string Location { get; set; }

        public int? CategoryId { get; set; }

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