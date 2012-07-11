// -----------------------------------------------------------------------
// <copyright file="GeoExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Geo
{
    using System;
    using NetTopologySuite.Geometries;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class GeoExtensions
    {
        public static double DistanceInKm(this Point point, Point other)
        {
            var s = point.Coordinate;
            var f = other.Coordinate;
            var delta = s.Y - f.Y;

            var t = Math.Acos(Math.Sin(s.X) * Math.Sin(f.X) + Math.Cos(s.Y) * Math.Cos(f.Y) * Math.Cos(delta));

            return 6335.439 * t;
        }
    }
}
