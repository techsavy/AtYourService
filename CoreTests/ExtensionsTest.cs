// -----------------------------------------------------------------------
// <copyright file="ExtensionsTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class ExtensionsTest
    {
        [Test]
        public void IsNullOrEmpty_When_String_Is_Null()
        {
            string input = null;

            Assert.IsTrue(input.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_When_String_Is_Empty()
        {
            string input = string.Empty;

            Assert.IsTrue(input.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_When_String_Is_Not_Empty()
        {
            string input = "test";

            Assert.IsFalse(input.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrWhiteSpace_When_String_Is_Null()
        {
            string input = null;

            Assert.IsTrue(input.IsNullOrWhiteSpace());
        }

        [Test]
        public void IsNullOrWhiteSpace_When_String_Is_Empty()
        {
            string input = string.Empty;

            Assert.IsTrue(input.IsNullOrWhiteSpace());
        }

        [Test]
        public void IsNullOrWhiteSpace_When_String_Is_WhiteSpace()
        {
            string input = "    ";

            Assert.IsTrue(input.IsNullOrWhiteSpace());
        }

        [Test]
        public void IsNullOrWhiteSpace_When_String_Is_Not_Empty()
        {
            string input = "test";

            Assert.IsFalse(input.IsNullOrWhiteSpace());
        }
    }
}
