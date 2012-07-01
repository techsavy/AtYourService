// -----------------------------------------------------------------------
// <copyright file="SearchModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Models
{
    public class SearchModel
    {
        public string Terms { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}