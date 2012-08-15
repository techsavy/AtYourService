// -----------------------------------------------------------------------
// <copyright file="ServiceOffering.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using NHibernate.Search.Attributes;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ClassBridge(typeof(LocationFieldClassBridge), Index = Index.No, Store = Store.Yes, Name = "Location")]
    [ClassBridge(typeof(DateFieldsClassBridge), Index = Index.No, Store = Store.Yes, Name = "Dates")]
    public class ServiceOffering : Service
    {
    }
}
