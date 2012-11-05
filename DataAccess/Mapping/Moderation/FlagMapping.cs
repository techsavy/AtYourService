
namespace AtYourService.Data.Mapping.Moderation
{
    using Domain.Moderation;

    public class FlagMapping : EntityMapping<Flag>
    {
        public FlagMapping()
        {
            Property(review => review.Message, m => { m.NotNullable(false); m.Length(5000); });
            Property(review => review.Type, m => { m.NotNullable(true); });
            Property(review => review.Status, m => { m.NotNullable(true); });

            ManyToOne(review => review.Service, m => { m.Column("ServiceId"); m.NotNullable(true); m.ForeignKey("FK_Flag_Service"); });
            ManyToOne(review => review.Client, m => { m.Column("ClientId"); m.NotNullable(true); m.ForeignKey("FK_Flag_Client"); });
        }
    }
}
