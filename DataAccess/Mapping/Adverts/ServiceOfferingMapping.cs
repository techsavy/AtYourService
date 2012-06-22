// -----------------------------------------------------------------------
// <copyright file="ServiceOfferingMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping.Adverts
{
    using Domain.Adverts;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServiceOfferingMapping : SubclassMapping<ServiceOffering>
    {
        public ServiceOfferingMapping()
        {
            DiscriminatorValue("Offering");
        }
    }
}
