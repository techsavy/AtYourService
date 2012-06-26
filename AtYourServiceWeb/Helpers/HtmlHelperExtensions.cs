
namespace AtYourService.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlHelperExtensions
    {
        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }

        public static MvcHtmlString TruncatedText(this HtmlHelper helper, string text, int length)
        {
            if (string.IsNullOrWhiteSpace(text))
                return MvcHtmlString.Empty;

            var taga = new TagBuilder("a");
            taga.MergeAttribute("href", "#");
            taga.MergeAttribute("title", text);

            taga.SetInnerText(helper.Truncate(text, length));

            return new MvcHtmlString(taga.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FuzzyDate(this HtmlHelper helper, DateTime? dateTime)
        {
            if (dateTime != null)
            {
                var abbrTag = new TagBuilder("abbr");
                abbrTag.AddCssClass("timeago");
                abbrTag.Attributes.Add("title", dateTime.Value.ToString("o"));
                abbrTag.InnerHtml += dateTime.Value.ToString(Properties.Resources.DateTimeFormat);

                return new MvcHtmlString(abbrTag.ToString());
            }

            return new MvcHtmlString(string.Empty);
        }

        public static EnhancedHtmlHelper<TModel> Custom<TModel>(this HtmlHelper<TModel> helper)
        {
            return new EnhancedHtmlHelper<TModel>(helper);
        }

        public static MvcHtmlString Message(this HtmlHelper helper)
        {
            if (helper.ViewContext.TempData.ContainsKey(ViewDataKeys.Message))
            {
                var message = (Message)helper.ViewContext.TempData[ViewDataKeys.Message];

                return message.Render(helper);
            }

            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString CategoryFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList)
        {
            var attributes = new Dictionary<string, object>();
            attributes.Add("data-val", "true");
            attributes.Add("data-val-required", "The Category field is required");
            attributes.Add("data-val-number", "The Category field is required");

            return htmlHelper.DropDownGroupListFor(expression, selectList, "-", attributes);
        }

        public static MvcHtmlString HiddenLocation(this HtmlHelper helper)
        {
            var lat = helper.Hidden("Latitude");
            var lng = helper.Hidden("Longitude");

            return new MvcHtmlString(lat.ToHtmlString() + lng.ToHtmlString());
        }
    }
}