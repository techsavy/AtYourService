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
    using NHibernate.Search.Attributes;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Indexed]
    public class Category : Entity
    {
        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Name { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual ISet<Category> SubCategories { get; set; }

        public virtual ISet<Service> Services { get; set; }

        public virtual int AdCount { get; set; }
    }
}
