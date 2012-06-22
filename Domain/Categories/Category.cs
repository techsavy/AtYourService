// -----------------------------------------------------------------------
// <copyright file="Category.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------


namespace AtYourService.Domain.Categories
{
    using Adverts;
    using Core.Data;
    using Iesi.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Category : Entity
    {
        public virtual string Name { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual ISet<Category> SubCategories { get; set; }

        public virtual ISet<Service> Adverts { get; set; }

        public virtual int AdCount { get; set; }
    }
}
