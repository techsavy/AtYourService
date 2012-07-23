// -----------------------------------------------------------------------
// <copyright file="ScreeningMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping.Adverts
{
    using Domain.Adverts;
    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ScreeningMapping : ClassMapping<Screening>
    {
        public ScreeningMapping()
        {
            Id(c => c.Id, c => c.Generator(Generators.Identity));

            Property(c => c.ServiceId, c => c.NotNullable(true));
            Property(c => c.Date, c => c.NotNullable(true));
            Property(c => c.IP, c => c.NotNullable(true));
            Property(c => c.UserName, c => c.NotNullable(true));

            Discriminator(x =>
            {
                x.Force(true);
                x.Insert(true);
                x.Length(2);
                x.NotNullable(true);
                x.Type(NHibernateUtil.String);

                x.Column("ScreeningType");
            });
        }
    }

    public class ImpressionMapping : SubclassMapping<Impression>
    {
        public ImpressionMapping()
        {
            DiscriminatorValue("I");
        }
    }

    public class ViewMapping : SubclassMapping<View>
    {
        public ViewMapping()
        {
            DiscriminatorValue("V");
        }
    }
}
