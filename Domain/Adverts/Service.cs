// -----------------------------------------------------------------------
// <copyright file="Service.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System;
    using Categories;
    using Core.Data;
    using Files;
    using NetTopologySuite.Geometries;
    using NHibernate.Search.Attributes;
    using Users;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Indexed]
    public abstract class Service : Entity
    {
        [Field(Index.Tokenized, Store = Store.Yes)]
        [Boost(2.0f)]
        public virtual string Title { get; set; }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Body { get; set; }

        [IndexedEmbedded(Depth = 1, Prefix = "Category_")]
        public virtual Category Category { get; set; }

        public virtual Client Client { get; set; }

        public virtual Point Location { get; set; }

        [Field(Index.UnTokenized, Store = Store.Yes)]
        public virtual long Views { get; set; }

        public virtual long Impressions { get; set; }

        public virtual DateTime? ExpiryDate { get; set; }

        public virtual DateTime? EffectiveDate { get; set; }

        [Field(Index.UnTokenized, Store = Store.Yes)]
        public virtual bool IsDeleted { get; set; }

        public virtual Image Image { get; set; }
    }
}
