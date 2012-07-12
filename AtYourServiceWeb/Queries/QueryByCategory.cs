

namespace AtYourService.Web.Queries
{
    using System;
    using System.Collections.Generic;
    using Domain.Adverts;
    using Models;
    using NHibernate;

    public class QueryByCategory : IQuery<IEnumerable<Service>>
    {
        public QueryByCategory(CategoryBrowseModel categoryBrowseModel)
        {
            _categoryBrowseModel = categoryBrowseModel;
        }

        private CategoryBrowseModel _categoryBrowseModel;

        public IEnumerable<Service> Execute(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}