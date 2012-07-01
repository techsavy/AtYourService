// -----------------------------------------------------------------------
// <copyright file="LocationFieldClassBridge.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System.Collections.Generic;
    using Lucene.Net.Documents;
    using NHibernate.Search.Bridge;

    public class LocationFieldClassBridge : IFieldBridge, IParameterizedBridge
    {
        public void Set(string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var service = (Service) value;
            var location = service.Location;
            var lat = new Field("Location_Latitude", location.X.ToString(), store, index);
            var lng = new Field("Location_Longitude", location.Y.ToString(), store, index);

            document.Add(lat);
            document.Add(lng);
        }

        public void SetParameterValues(Dictionary<string, object> parameters)
        {
        }
    }
}