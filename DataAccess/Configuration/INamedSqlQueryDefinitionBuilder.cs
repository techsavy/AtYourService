// -----------------------------------------------------------------------
// <copyright file="INamedSqlQueryDefinitionBuilder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Configuration
{
    using System.Collections.Generic;
    using NHibernate;
    using NHibernate.Engine;
    using NHibernate.Engine.Query.Sql;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface INamedSqlQueryDefinitionBuilder
    {
        bool IsCacheable { get; set; }

        string CacheRegion { get; set; }

        int FetchSize { get; set; }

        int Timeout { get; set; }

        FlushMode FlushMode { get; set; }

        string Query { get; set; }

        bool IsReadOnly { get; set; }

        string Comment { get; set; }

        CacheMode? CacheMode { get; set; }

        IList<string> QuerySpaces { get; set; }

        INativeSQLQueryReturn[] QueryReturns { get; set; }
    }

    internal class NamedSqlQueryDefinitionBuilder : INamedSqlQueryDefinitionBuilder
    {
        private int fetchSize = -1;
        private int timeout = -1;

        public NamedSqlQueryDefinitionBuilder()
        {
            FlushMode = FlushMode.Unspecified;
        }

        public bool IsCacheable { get; set; }
        public string CacheRegion { get; set; }

        public int FetchSize
        {
            get { return fetchSize; }
            set
            {
                if (value > 0)
                {
                    fetchSize = value;
                }
                else
                {
                    fetchSize = -1;
                }
            }
        }

        public int Timeout
        {
            get { return timeout; }
            set
            {
                if (value > 0)
                {
                    timeout = value;
                }
                else
                {
                    timeout = -1;
                }
            }
        }

        public FlushMode FlushMode { get; set; }

        public string Query { get; set; }

        public bool IsReadOnly { get; set; }

        public string Comment { get; set; }

        public CacheMode? CacheMode { get; set; }

        public IList<string> QuerySpaces { get; set; }

        public INativeSQLQueryReturn[] QueryReturns { get; set; }

        public NamedSQLQueryDefinition Build()
        {
            return new NamedSQLQueryDefinition(Query, QueryReturns, QuerySpaces, IsCacheable, CacheRegion, Timeout, FetchSize, FlushMode, CacheMode, IsReadOnly, Comment, new Dictionary<string, string>(1), true);
        }
    }
}
