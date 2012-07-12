﻿

namespace AtYourService.Web.Models
{
    using System.Linq;

    public class CategoryBrowseModel
    {
        public int Id { get; set; }

        public int? Page { get; set; }

        public string SortBy { get; set; }

        public string GetSortField()
        {
            if ((new[] { "Date", "Distance", "Views" }).Contains(SortBy))
                return SortBy;

            return "Date";
        }
    }
}