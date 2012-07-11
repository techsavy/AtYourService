// -----------------------------------------------------------------------
// <copyright file="ExtensionTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Tests
{
    using AtYourService.Core.Geo;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class PointFactoryTest
    {
        [Test]
        public void Create_Point_Assigns_Correctly()
        {
            const double lat = 9.344d;
            const double lng = 10.654d;

            var point = PointFactory.Create(lat, lng);

            Assert.AreEqual(lat, point.Y);
            Assert.AreEqual(lng, point.X);
            Assert.AreEqual(PointFactory.WorldGeodeticSystemSrid, point.SRID);
        }
    }
}
