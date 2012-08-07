// -----------------------------------------------------------------------
// <copyright file="DateFieldsClassBridge.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System;
    using System.Collections.Generic;
    using Lucene.Net.Documents;
    using NHibernate.Search.Bridge;

    /// <summary>
    /// Stores the required date fields as ticks
    /// </summary>
    public class DateFieldsClassBridge : IFieldBridge, IParameterizedBridge
    {
        public void Set(string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var service = (Service)value;

            var effectiveDate = new Field("EffectiveDate", service.EffectiveDate.Value.Ticks.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
            var expiryDate = new Field("ExpiryDateDate", (service.ExpiryDate ?? DateTime.MaxValue).Ticks.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);

            document.Add(effectiveDate);
            document.Add(expiryDate);
        }

        public void SetParameterValues(Dictionary<string, object> parameters)
        {
        }
    }
}
