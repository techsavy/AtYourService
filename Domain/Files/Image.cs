// -----------------------------------------------------------------------
// <copyright file="Image.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Files
{
    using Adverts;
    using Core.Data;
    using Iesi.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Image : Entity
    {
        public virtual string FileName { get; set; }

        public virtual string ContentType { get; set; }

        public virtual ISet<Service> Services { get; set; }
    }
}
