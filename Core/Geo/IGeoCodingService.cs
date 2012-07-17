// -----------------------------------------------------------------------
// <copyright file="IGeoCodingService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Geo
{
    using NetTopologySuite.Geometries;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IGeoCodingService
    {
        Point GetCoordinates(string location);
    }
}
