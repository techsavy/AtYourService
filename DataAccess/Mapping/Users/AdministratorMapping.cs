﻿// -----------------------------------------------------------------------
// <copyright file="AdministratorMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Mapping.Users
{
    using Domain.Users;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AdministratorMapping : SubclassMapping<Administrator>
    {
        public AdministratorMapping()
        {
            DiscriminatorValue("Admin");
        }
    }
}
