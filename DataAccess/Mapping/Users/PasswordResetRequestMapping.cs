// -----------------------------------------------------------------------
// <copyright file="PasswordResetRequestMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping.Users
{
    using Domain.Users;

    public class PasswordResetRequestMapping : EntityMapping<PasswordResetRequest>
    {
        public PasswordResetRequestMapping()
        {
            Property(clientSettings => clientSettings.Token, m => m.NotNullable(true));
            Property(clientSettings => clientSettings.ExpiryDate, m => m.NotNullable(true));

            ManyToOne(o => o.User,
                  o =>
                  {
                      o.Column("UserId");
                      o.Unique(true);
                      o.NotNullable(true);
                      o.ForeignKey("FK_Users_PasswordResetRequest");
                  });

            Schema("dbo");
        }
    }
}
