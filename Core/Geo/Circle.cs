// -----------------------------------------------------------------------
// <copyright file="Circle.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Geo
{
    using System;
    using System.Collections.Generic;
    using GeoAPI.Geometries;
    using NetTopologySuite.Geometries;

    /// <summary>
    /// Represents a rough circle
    /// <remarks>Taken from http://sharpmap.codeplex.com/discussions/356974/ </remarks>
    /// </summary>
    public class Circle : LinearRing
    {
        public Circle(IPoint center, double radius)
            : base(CreatePoints(center, radius).ToArray())
        {
            Center = center;
        }

        public IPoint Center { get; private set; }

        private static List<Coordinate> CreatePoints(IPoint point, double radius)
        {
            radius = radius / 1609.344; // Convert metters to miles             

            var d2r = Math.PI / 180;
            var lat = radius * 0.014483;  // Convert miles into degrees latitude
            var lng = lat / Math.Cos(point.X * d2r);
            var points = new List<Coordinate>();
            for (var i = 0; i < 33; i++)
            {
                var theta = Math.PI * (i / 16);
                var y = point.X + (lat * Math.Sin(theta));
                var x = point.Y + (lng * Math.Cos(theta));
                var p = new Coordinate(y, x);
                points.Add(p);
            }

            return points;
        }
    }
}
