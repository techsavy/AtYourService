// -----------------------------------------------------------------------
// <copyright file="ClientSettingsMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------


using NHibernate.Mapping.ByCode;

namespace AtYourService.Data.Mapping.Users
{
    using Domain.Users;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ClientSettingsMapping : EntityMapping<ClientSettings>
    {
        public ClientSettingsMapping()
        {
            //NOTE: Shared primary key maping did not work when saving the entities

            Property(clientSettings => clientSettings.Source, m => m.NotNullable(true));
            Property(clientSettings => clientSettings.AdCount, m => m.NotNullable(true));
            ManyToOne(o => o.Client,
                  o =>
                  {
                      o.Column("UserId");
                      o.Unique(true);
                      o.ForeignKey("FK_Users_ClientSettings");
                  });
        }
    }
}
