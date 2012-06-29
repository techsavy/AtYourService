// -----------------------------------------------------------------------
// <copyright file="ImageFileAttributeTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Tests.Util
{
    using System.Web;
    using System.Web.Mvc;
    using Moq;
    using NUnit.Framework;
    using Web.Util;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class ImageFileAttributeTest
    {
        [Test]
        public void Not_A_HttpPostedFileBase()
        {
            var imageFileAttribute = new ImageFileAttribute();

            Assert.IsFalse(imageFileAttribute.IsValid("foo"));
        }

        [Test]
        public void Null_Value()
        {
            var imageFileAttribute = new ImageFileAttribute();

            Assert.IsTrue(imageFileAttribute.IsValid(null));
        }

        [Test]
        public void Larger_Than_A_Megabite()
        {
            var postedFile = new Mock<HttpPostedFileBase>();
            postedFile.SetupGet(f => f.ContentLength).Returns(1024*1024 + 2);
            var imageFileAttribute = new ImageFileAttribute();

            Assert.IsFalse(imageFileAttribute.IsValid(postedFile.Object));
        }
    }
}
