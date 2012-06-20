// -----------------------------------------------------------------------
// <copyright file="AdministratorMapping.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using AtYourService.Domain.Users;
using NHibernate.Mapping.ByCode.Conformist;

namespace AtYourService.Data.Mapping.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AdministratorMapping : SubclassMapping<Administrator>
    {
        public AdministratorMapping()
        {
            DiscriminatorValue(1);
        }
    }
}
