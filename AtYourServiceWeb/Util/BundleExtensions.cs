﻿
namespace AtYourService.Web.Util
{
    using System.Web.Optimization;

    public static class BundleExtensions
    {
        public static void EnableBootstrapBundle(this BundleCollection bundles)
        {
            var bootstrapCss = new Bundle("~/bootstrap/css", new CssMinify());
            bootstrapCss.AddFile("~/Content/bootstrap.css");
            bootstrapCss.AddFile("~/Content/rateit.css");
            bootstrapCss.AddFile("~/Content/Site.css");

            bundles.Add(bootstrapCss);

            var bootstrapJs = new Bundle("~/bootstrap/js", new JsMinify());
            bootstrapJs.AddFile("~/Scripts/jquery-1.7.1.js");
            bootstrapJs.AddFile("~/Scripts/bootstrap.js");
            bootstrapJs.AddFile("~/Scripts/jquery.validate.js");
            bootstrapJs.AddFile("~/Scripts/jquery.validate.unobtrusive.js");
            bootstrapJs.AddFile("~/Scripts/jquery.placeholder.js");
            bootstrapJs.AddFile("~/Scripts/jquery.rateit.js");
            bootstrapJs.AddFile("~/Scripts/ays.init.js");
            bootstrapJs.AddFile("~/Scripts/ays.util.js");
            bootstrapJs.AddFile("~/Scripts/ays.location.js");

            bundles.Add(bootstrapJs);
        }

        public static void EnableReportingBundle(this BundleCollection bundles)
        {
            //it looks like there is a parsing error when files are combined
            var reportJs = new Bundle("~/reports/js", new JsMinify());
            reportJs.AddFile("~/Scripts/raphael.js");
            reportJs.AddFile("~/Scripts/g.raphael.js");
            reportJs.AddFile("~/Scripts/g.line.js");
            reportJs.AddFile("~/Scripts/date.format.js");

            bundles.Add(reportJs);
        }
    }
}