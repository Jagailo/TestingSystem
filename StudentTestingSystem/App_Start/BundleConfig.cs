using System.Web.Optimization;

namespace StudentTestingSystem
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/umd/popper.min.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/paper").Include(
                "~/Content/assets/js/plugins/moment.min.js",
                "~/Content/assets/js/plugins/bootstrap-switch.js",
                "~/Content/assets/js/plugins/nouislider.min.js",
                "~/Content/assets/js/plugins/chartjs.min.js",
                "~/Content/assets/js/plugins/bootstrap-notify.js",
                "~/Content/assets/js/paper-dashboard.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/fontawesome-all.min.css",
                "~/Content/assets/css/paper-dashboard.min.css",
                "~/Content/site.css"));
        }
    }
}
