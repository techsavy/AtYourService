// -----------------------------------------------------------------------
// <copyright file="EmailVerificationMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping.Users
{
    using Domain.Users;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EmailVerificationMapping : EntityMapping<EmailVerification>
    {
        public EmailVerificationMapping()
        {
            //NOTE: Shared primary key maping did not work when saving the entities

            Property(clientSettings => clientSettings.VerificationCode, m => m.NotNullable(true));
            Property(clientSettings => clientSettings.IsVerified, m => m.NotNullable(true));

            ManyToOne(o => o.Client,
                  o =>
                  {
                      o.Column("UserId");
                      o.Unique(true);
                      o.NotNullable(true);
                      o.ForeignKey("FK_Users_EmailVerification");
                  });

            Schema("dbo");
        }
    }
}
