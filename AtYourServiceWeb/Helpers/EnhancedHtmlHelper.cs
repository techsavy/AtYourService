
namespace AtYourService.Web.Helpers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public class EnhancedHtmlHelper<TModel>
    {
        private readonly HtmlHelper<TModel> _helper;

        public EnhancedHtmlHelper(HtmlHelper<TModel> helper)
        {
            _helper = helper;
        }

        public MvcHtmlString LabelFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var resolvedLabelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(resolvedLabelText))
            {
                return MvcHtmlString.Empty;
            }

            if (metadata.IsRequired && typeof(TProperty) != typeof(bool))
            {
                var spanTag = new TagBuilder("span");
                spanTag.AddCssClass("required-star");
                spanTag.InnerHtml = "*";

                resolvedLabelText = string.Concat(resolvedLabelText, spanTag.ToString(TagRenderMode.Normal));
            }

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(_helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tag.InnerHtml = resolvedLabelText;
            tag.AddCssClass("control-label");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}
