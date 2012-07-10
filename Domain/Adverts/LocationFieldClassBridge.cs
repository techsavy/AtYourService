// -----------------------------------------------------------------------
// <copyright file="LocationFieldClassBridge.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Domain.Adverts
{
    using System.Collections.Generic;
    using Lucene.Net.Documents;
    using Lucene.Net.Spatial.Tier.Projectors;
    using Lucene.Net.Util;
    using NHibernate.Search.Bridge;

    public class LocationFieldClassBridge : IFieldBridge, IParameterizedBridge
    {
        private static readonly List<CartesianTierPlotter> Ctps = new List<CartesianTierPlotter>();

        private static readonly IProjector Projector = new SinusoidalProjector();

        static LocationFieldClassBridge()
        {
            for (var @base = 2; @base <= 15; @base++)
            {
                Ctps.Add(new CartesianTierPlotter(@base, Projector, CartesianTierPlotter.DefaltFieldPrefix));
            }
        }

        public void Set(string name, object value, Document document, Field.Store store, Field.Index index, float? boost)
        {
            var service = (Service)value;
            var location = service.Location;
            var lat = new Field("Location_Latitude", NumericUtils.DoubleToPrefixCoded(location.X), Field.Store.YES, Field.Index.NOT_ANALYZED);
            var lng = new Field("Location_Longitude", NumericUtils.DoubleToPrefixCoded(location.Y), Field.Store.YES, Field.Index.NOT_ANALYZED);

            document.Add(lat);
            document.Add(lng);

            document.Add(new Field("metafile", "doc", Field.Store.YES, Field.Index.ANALYZED));

            int ctpsize = Ctps.Count;
            for (int i = 0; i < ctpsize; i++)
            {
                CartesianTierPlotter ctp = Ctps[i];
                var boxId = ctp.GetTierBoxId(location.X, location.Y);
                document.Add(new Field(ctp.GetTierFieldName(), NumericUtils.DoubleToPrefixCoded(boxId), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            }
        }

        public void SetParameterValues(Dictionary<string, object> parameters)
        {
        }
    }
}