// -----------------------------------------------------------------------
// <copyright file="HtmlHelperTest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Web.Tests.Helpers
{
    using System.Web.Mvc;
    using Models;
    using NUnit.Framework;
    using Web.Helpers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class HtmlHelperTests
    {
        [Test]
        public void TruncateText_Length_Smaller_Than_Limit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length + 1;

            var truncatedText = htmlHelper.Truncate(text, truncateLength);

            Assert.AreEqual(text, truncatedText);
        }

        [Test]
        public void TruncateText_Length_Larger_Than_Limit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length - 1;
            const string expectedTruncatedText = "12345678...";

            var truncatedText = htmlHelper.Truncate(text, truncateLength);

            Assert.AreEqual(expectedTruncatedText, truncatedText);
        }

        [Test]
        public void Truncate_Text_Length_Equals_The_Limit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length;

            var truncatedText = htmlHelper.Truncate(text, truncateLength);

            Assert.AreEqual(text, truncatedText);
        }

        [Test]
        public void TruncateText_As_Link_When_Length_Smaller_Than_Limit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length + 1;
            const string expectedLink = "<a href=\"#\" title=\"123456789\">123456789</a>";

            var truncatedText = htmlHelper.TruncatedText(text, truncateLength);

            Assert.AreEqual(expectedLink, truncatedText.ToHtmlString());
        }

        [Test]
        public void TruncateText_As_Link_When_Length_Larger_Than_Limit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length - 1;
            const string expectedLink = "<a href=\"#\" title=\"123456789\">12345678...</a>";

            var truncatedText = htmlHelper.TruncatedText(text, truncateLength);

            Assert.AreEqual(expectedLink, truncatedText.ToHtmlString()); ;
        }

        [Test]
        public void TruncateText_As_Link_When_Length_Equals_The_Limit()
        {
            HtmlHelper htmlHelper = null;
            const string text = "123456789";
            var truncateLength = text.Length;
            const string expectedLink = "<a href=\"#\" title=\"123456789\">123456789</a>";

            var truncatedText = htmlHelper.TruncatedText(text, truncateLength);

            Assert.AreEqual(expectedLink, truncatedText.ToHtmlString()); ;
        }

        [Test]
        public void HiddenLocation_When_Model_Is_Null()
        {
            var htmlHelper = MvcMockHelpers.CreateHtmlHelper(new ViewDataDictionary());
            const string expectedHtml = "<input id=\"Latitude\" name=\"Latitude\" type=\"hidden\" value=\"\" /><input id=\"Longitude\" name=\"Longitude\" type=\"hidden\" value=\"\" />";

            var actualHtml = htmlHelper.HiddenLocation();

            Assert.AreEqual(expectedHtml, actualHtml.ToHtmlString());
        }

        [Test]
        public void HiddenLocation_When_Model_Is_Passed()
        {
            var model = new CreateServiceModel { Latitude = 13.37, Longitude = 60.61};
            var htmlHelper = MvcMockHelpers.CreateHtmlHelper(new ViewDataDictionary(model));
            const string expectedHtml = "<input id=\"Latitude\" name=\"Latitude\" type=\"hidden\" value=\"13.37\" /><input id=\"Longitude\" name=\"Longitude\" type=\"hidden\" value=\"60.61\" />";

            var actualHtml = htmlHelper.HiddenLocation();

            Assert.AreEqual(expectedHtml, actualHtml.ToHtmlString());
        }

        [Test]
        public void HiddenLocation_When_Model_Is_Passed_With_Model_State()
        {
            var model = new CreateServiceModel { Latitude = 13.37, Longitude = 60.61 };
            var viewData = new ViewDataDictionary(model);
            viewData.ModelState.Add("Latitude", new ModelState { Value = new ValueProviderResult(13.37, "13.37", null) });
            viewData.ModelState.Add("Longitude", new ModelState { Value = new ValueProviderResult(60.61, "60.61", null) });

            var htmlHelper = MvcMockHelpers.CreateHtmlHelper(viewData);
            const string expectedHtml = "<input id=\"Latitude\" name=\"Latitude\" type=\"hidden\" value=\"13.37\" /><input id=\"Longitude\" name=\"Longitude\" type=\"hidden\" value=\"60.61\" />";

            var actualHtml = htmlHelper.HiddenLocation();

            Assert.AreEqual(expectedHtml, actualHtml.ToHtmlString());
        }
    }
}
