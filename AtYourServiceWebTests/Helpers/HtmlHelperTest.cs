// -----------------------------------------------------------------------
// <copyright file="HtmlHelperTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Tests.Helpers
{
    using System.Web.Mvc;
    using NUnit.Framework;
    using Web.Helpers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class HtmlHelperTests
    {
        [Test]
        public void TruncateTextLengthSmallerThanLimit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length + 1;

            var truncatedText = htmlHelper.Truncate(text, truncateLength);

            Assert.AreEqual(text, truncatedText);
        }

        [Test]
        public void TruncateTextLengthLargerThanLimit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length - 1;
            const string expectedTruncatedText = "12345678...";

            var truncatedText = htmlHelper.Truncate(text, truncateLength);

            Assert.AreEqual(expectedTruncatedText, truncatedText);
        }

        [Test]
        public void TruncateTextLengthEqualsTheLimit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length;

            var truncatedText = htmlHelper.Truncate(text, truncateLength);

            Assert.AreEqual(text, truncatedText);
        }

        [Test]
        public void TruncateTextAsLinkWhenLengthSmallerThanLimit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length + 1;
            const string expectedLink = "<a href=\"#\" title=\"123456789\">123456789</a>";

            var truncatedText = htmlHelper.TruncatedText(text, truncateLength);

            Assert.AreEqual(expectedLink, truncatedText.ToHtmlString());
        }

        [Test]
        public void TruncateTextAsLinkWhenLengthLargerThanLimit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length - 1;
            const string expectedLink = "<a href=\"#\" title=\"123456789\">12345678...</a>";

            var truncatedText = htmlHelper.TruncatedText(text, truncateLength);

            Assert.AreEqual(expectedLink, truncatedText.ToHtmlString()); ;
        }

        [Test]
        public void TruncateTextAsLinkWhenLengthEqualsTheLimit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length;
            const string expectedLink = "<a href=\"#\" title=\"123456789\">123456789</a>";

            var truncatedText = htmlHelper.TruncatedText(text, truncateLength);

            Assert.AreEqual(expectedLink, truncatedText.ToHtmlString()); ;
        }
    }
}
