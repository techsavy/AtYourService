
namespace AtYourService.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Helpers;
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

        public static MvcHtmlString CategoryFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, string optionLabel = "-")
        {
            var attributes = new Dictionary<string, object>();
            attributes.Add("data-val", "true");
            attributes.Add("data-val-required", "The Category field is required");
            attributes.Add("data-val-number", "The Category field is required");

            return htmlHelper.DropDownGroupListFor(expression, selectList, optionLabel, attributes);
        }

        public static MvcHtmlString CategoryFilterFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, string optionLabel = "-")
        {
            return htmlHelper.DropDownGroupListFor(expression, selectList, optionLabel);
        }

        public static MvcHtmlString HiddenLocation(this HtmlHelper helper)
        {
            var lat = BuildHiddenLocationComponent(helper, "Latitude");
            var lng = BuildHiddenLocationComponent(helper, "Longitude");

            return new MvcHtmlString(lat + lng);
        }

        private static string BuildHiddenLocationComponent(HtmlHelper helper, string component)
        {
            var componentValue = string.Empty;
            var componentModelValue = helper.ViewData.Eval(component);

            if (helper.ViewData.ModelState.ContainsKey(component))
            {
                componentValue = helper.ViewData.ModelState[component].Value.AttemptedValue;
            }
            else if (componentModelValue != null)
            {
                componentValue = componentModelValue.ToString();
            }

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "hidden");
            tagBuilder.MergeAttribute("id", component);
            tagBuilder.MergeAttribute("name", component);
            tagBuilder.MergeAttribute("value", componentValue);

            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }

        public static MvcHtmlString ImageUploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "file");
            tagBuilder.MergeAttribute("id", htmlFieldName);
            tagBuilder.MergeAttribute("name", htmlFieldName);
            tagBuilder.MergeAttribute("accept", "image/gif,image/jpeg,image/png");

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString Serialize(this HtmlHelper helper, object value)
        {
            return new MvcHtmlString(Json.Encode(value));
        }

        public static MvcHtmlString ServiceDetailsActionLink(this HtmlHelper helper, Domain.Adverts.Service service)
        {
            var formattedTitle = service.Title.Replace(' ', '-');
            return helper.ActionLink(service.Title, "Details", "Services", new { id = service.Id, title = formattedTitle }, null);
        }

        public static MvcHtmlString MenuLink(this HtmlHelper helper, string text, string action, string controller)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"];
            var currentAction = routeData["action"];

            var tagBuilder = new TagBuilder("li");
            if (String.Equals(action, currentAction as string,
                      StringComparison.OrdinalIgnoreCase)
                &&
               String.Equals(controller, currentController as string,
                       StringComparison.OrdinalIgnoreCase))
            {
                tagBuilder.AddCssClass("active");
            }

            tagBuilder.InnerHtml = helper.ActionLink(text, action, controller).ToString();
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}