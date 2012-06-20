// -----------------------------------------------------------------------
// <copyright file="ClientSettings.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------


namespace AtYourService.Domain.Users
{
    using Core.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ClientSettings : Entity
    {
        public virtual int Source { get; set; }

        public virtual int AdCount { get; set; }

        public virtual Client Client { get; set; }
    }
}
