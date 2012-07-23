// -----------------------------------------------------------------------
// <copyright file="Screening.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class Screening
    {
        public virtual long Id { get; set; }

        public virtual int ServiceId { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string UserName { get; set; }

        public virtual string IP { get; set; }
    }

    public class Impression : Screening
    {         
    }

    public class View : Screening
    {
    }
}
