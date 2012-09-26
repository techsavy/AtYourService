// -----------------------------------------------------------------------
// <copyright file="INativeSQLQueryRootReturn.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface INativeSqlQueryRootReturnBuilder
    {
        string Alias { get; set; }
        string EntityName { get; set; }
    }
}
