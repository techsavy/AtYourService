// -----------------------------------------------------------------------
// <copyright file="ClientMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping.Users
{
    using Domain.Users;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ClientMapping : SubclassMapping<Client>
    {
        public ClientMapping()
        {
            DiscriminatorValue("Client");
            OneToOne(c => c.ClientSettings, m => m.PropertyReference(typeof(ClientSettings).GetPropertyOrFieldMatchingName("Client")));
            Set(c => c.Services, m => { m.OrderBy(c => c.Title); m.Key(k => k.Column("ClientId")); }, l => l.OneToMany());
            Set(c => c.Reviews, m => { m.OrderBy(c => c.LastModifiedDate); m.Key(k => k.Column("ClientId")); }, l => l.OneToMany());
        }
    }
}
