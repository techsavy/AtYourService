
namespace AtYourService.Web.Util
{
    using System.Web.Optimization;

    public static class BundleExtensions
    {
        public static void EnableBootstrapBundle(this BundleCollection bundles)
        {
            var bootstrapCss = new Bundle("~/bootstrap/css", new CssMinify());
            bootstrapCss.AddFile("~/Content/bootstrap.css");
            bootstrapCss.AddFile("~/Content/bootstrap-responsive.css");
            bootstrapCss.AddFile("~/Content/Site.css");

            bundles.Add(bootstrapCss);

            var bootstrapJs = new Bundle("~/bootstrap/js", new JsMinify());
            bootstrapJs.AddFile("~/Scripts/jquery-1.7.1.js");
            bootstrapJs.AddFile("~/Scripts/bootstrap.js");
            bootstrapJs.AddFile("~/Scripts/jquery.validate.js");
            bootstrapJs.AddFile("~/Scripts/jquery.validate.unobtrusive.js");
            bootstrapJs.AddFile("~/Scripts/jquery.placeholder.js");
            bootstrapJs.AddFile("~/Scripts/ays.init.js");

            bundles.Add(bootstrapJs);
        }
    }
}