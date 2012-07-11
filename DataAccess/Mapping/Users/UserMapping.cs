// -----------------------------------------------------------------------
// <copyright file="UserMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping.Users
{
    using Domain.Users;
    using NHibernate;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserMapping : EntityMapping<User>
    {
        public UserMapping()
        {
            Property(user => user.Name, m => { m.NotNullable(true); m.Length(50); });
            Property(user => user.Email, m => { m.NotNullable(true); m.Length(50); m.UniqueKey("UQ_User_Email"); });
            Property(user => user.Brag, m => m.Length(1000));
            Property(user => user.PasswordHash, m => m.NotNullable(true));
            Property(user => user.IsVerified, m => m.NotNullable(true));
            Property(user => user.LastActiveDate, m => m.NotNullable(true));
            Property(user => user.Location, m => { m.Type<NHibernate.Spatial.Type.MsSql2008GeographyType>(); m.Column(c => c.SqlType("geography")); });

            Discriminator(x =>
            {
                x.Force(true);
                x.Insert(true);
                x.Length(10);
                x.NotNullable(true);
                x.Type(NHibernateUtil.String);

                x.Column("UserType");
            });

            Table("Users");
            Schema("dbo");
        }
    }
}
