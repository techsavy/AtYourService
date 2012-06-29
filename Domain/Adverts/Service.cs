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
    using Users;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class Service : Entity
    {
        public virtual string Title { get; set; }

        public virtual string Body { get; set; }

        public virtual Category Category { get; set; }

        public virtual Client Client { get; set; }

        public virtual Point Location { get; set; }

        public virtual long Views { get; set; }

        public virtual long Impressions { get; set; }

        public virtual DateTime? ExpiryDate { get; set; }

        public virtual DateTime? EffectiveDate { get; set; }

        public virtual Image Image { get; set; }
    }
}
