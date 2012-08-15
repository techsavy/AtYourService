// -----------------------------------------------------------------------
// <copyright file="ServiceMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping.Adverts
{
    using Domain.Adverts;
    using NHibernate;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServiceMapping : EntityMapping<Service>
    {
        public ServiceMapping()
        {
            Discriminator(x =>
            {
                x.Force(true);
                x.Insert(true);
                x.Length(10);
                x.NotNullable(true);
                x.Type(NHibernateUtil.String);

                x.Column("ServiceType");
            });

            Property(service => service.Title, m => { m.NotNullable(true); m.Length(200); });
            Property(service => service.Body, m => { m.NotNullable(true); m.Length(5000); });
            Property(service => service.Views, m => { m.NotNullable(true); });
            Property(service => service.Impressions, m => { m.NotNullable(true); });
            Property(service => service.EffectiveDate, m => { m.NotNullable(true); });
            Property(service => service.ExpiryDate, m => { m.NotNullable(true); });
            Property(service => service.IsDeleted, m => { m.NotNullable(true); });

            Property(service => service.Location, m => { m.Type<NHibernate.Spatial.Type.MsSql2008GeographyType>(); m.Column(c => c.SqlType("geography")); });

            ManyToOne(service => service.Category, m => { m.Column("CategoryId"); m.NotNullable(true); m.ForeignKey("FK_Service_Category"); });
            ManyToOne(service => service.Client, m => { m.Column("ClientId"); m.NotNullable(true); m.ForeignKey("FK_Service_Client"); });
            ManyToOne(service => service.Image, m => { m.Column("ImageId"); m.NotNullable(false); m.ForeignKey("FK_Service_Image"); });

            Set(c => c.Reviews, m => { m.OrderBy(c => c.LastModifiedDate); m.Key(k => k.Column("ServiceId")); }, l => l.OneToMany());
        }
    }
}
