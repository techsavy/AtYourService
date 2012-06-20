

namespace AtYourService.Web.Helpers
{
    using System.Web.Mvc;

    public abstract class Message
    {
        protected Message(string messageText)
        {
            MessageText = messageText;
        }

        public string MessageText { get; private set; }

        public abstract MvcHtmlString Render(HtmlHelper htmlHelper);
    }

    public class SuccessMessage : Message
    {
        public SuccessMessage(string messageText)
            : base(messageText)
        {
        }

        public override MvcHtmlString Render(HtmlHelper htmlHelper)
        {
            var message = string.Format("<div class=\"alert alert-success\"><a class=\"close\" data-dismiss=\"alert\" href=\"#\">&times;</a>{0}</div>", MessageText);

            return new MvcHtmlString(message);
        }
    }

    public class FailMessage : Message
    {
        public FailMessage(string messageText)
            : base(messageText)
        {
        }

        public override MvcHtmlString Render(HtmlHelper htmlHelper)
        {
            var message = string.Format("<div class=\"alert alert-error\"><a class=\"close\" data-dismiss=\"alert\" href=\"#\">&times;</a>{0}</div>", MessageText);

            return new MvcHtmlString(message);
        }
    }
}