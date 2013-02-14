// -----------------------------------------------------------------------
// <copyright file="GeoCodingSerive.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Geo
{
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;
    using Core.Geo;
    using NetTopologySuite.Geometries;

    /// <summary>
    /// GeoCodeService implemented using google maps API
    /// https://developers.google.com/maps/documentation/geocoding/
    /// </summary>
    public class GeoCodingSerive : IGeoCodingService
    {
        private const string BaseUrl = "http://maps.googleapis.com/maps/api/geocode/xml?address=";

        public Point GetCoordinates(string location)
        {
            var response = SendGetRequest(SanitizeLocation(location));

            var xmlDoc = XDocument.Load(response.GetResponseStream());
            var status = xmlDoc.Elements("GeocodeResponse").Elements().Single(e => e.Name == "status").Value;

            if (status == "OK")
            {
                var locationElement = xmlDoc.Descendants().First(e => e.Name == "location");
                var lat = locationElement.Elements().Single(e => e.Name == "lat").Value;
                var lng = locationElement.Elements().Single(e => e.Name == "lng").Value;

                return PointFactory.Create(double.Parse(lat), double.Parse(lng));
            }

            return null;
        }

        private string SanitizeLocation(string location)
        {
            location = location.Trim();
            if (!location.ToLower().Contains("sri lanka"))
            {
                location = string.Format("{0},+{1}", location, "Sri+Lanka");
            }

            return location;
        }

        public WebResponse SendGetRequest(string location)
        {
            var url = string.Format("{0}{1}&sensor=false", BaseUrl, location);
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            //webRequest.Proxy.Credentials = new NetworkCredential("", "");

            return webRequest.GetResponse();
        }
    }
}