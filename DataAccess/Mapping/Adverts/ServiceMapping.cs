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

            Property(advert => advert.Title, m => { m.NotNullable(true); m.Length(200); });
            Property(advert => advert.Body, m => { m.NotNullable(true); m.Length(5000); });
            Property(advert => advert.Views, m => { m.NotNullable(true); });
            Property(advert => advert.Impressions, m => { m.NotNullable(true); });
            Property(advert => advert.EffectiveDate, m => { m.NotNullable(true); });
            Property(advert => advert.ExpiryDate, m => { m.NotNullable(true); });

            Property(user => user.Location, m => { m.Type<NHibernate.Spatial.Type.MsSql2008GeometryType>(); m.Column(c => c.SqlType("geometry")); });

            ManyToOne(category => category.Category, m => { m.Column("CategoryId"); m.NotNullable(true); m.ForeignKey("FK_Advert_Category"); });
            ManyToOne(category => category.Client, m => { m.Column("ClientId"); m.NotNullable(true); m.ForeignKey("FK_Advert_Client"); });
        }
    }
}
