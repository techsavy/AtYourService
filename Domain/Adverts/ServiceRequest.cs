// -----------------------------------------------------------------------
// <copyright file="ServiceRequest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using NHibernate.Search.Attributes;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ClassBridge(typeof(LocationFieldClassBridge), Index = Index.No, Store = Store.Yes)]
    [ClassBridge(typeof(DateFieldsClassBridge), Index = Index.No, Store = Store.Yes)]
    public class ServiceRequest : Service
    {
    }
}
