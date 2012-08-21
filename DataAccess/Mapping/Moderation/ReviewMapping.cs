
namespace AtYourService.Data.Mapping.Moderation
{
    using Domain.Moderation;

    public class ReviewMapping : EntityMapping<Review>
    {
        public ReviewMapping()
        {
            Property(review => review.Body, m => { m.NotNullable(true); m.Length(5000); });
            Property(review => review.Score, m => { m.NotNullable(true); });

            ManyToOne(review => review.Service, m => { m.Column("ServiceId"); m.NotNullable(true); m.ForeignKey("FK_Review_Service"); });
            ManyToOne(review => review.Client, m => { m.Column("ClientId"); m.NotNullable(true); m.ForeignKey("FK_Review_Client"); });
        }
    }
}
