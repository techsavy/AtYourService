// -----------------------------------------------------------------------
// <copyright file="PointFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Geo
{
    using NetTopologySuite.Geometries;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PointFactory
    {
        public const int WorldGeodeticSystemSrid = 4326;

        public static Point Create(double latitude, double longitude)
        {
            var point = new Point(longitude, latitude) { SRID = WorldGeodeticSystemSrid };

            return point;
        }
    }
}
