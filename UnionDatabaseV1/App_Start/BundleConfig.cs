using System.Web;
using System.Web.Optimization;

namespace UnionDatabaseV1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/lib/css/slick.css",
                      "~/Content/jquery-ui.min.css",
                      "~/lib/css/bootstrap.min.css"));


            bundles.Add(new StyleBundle("~/Content/admin").Include(
                      "~/lib/css/slick.css",
                      "~/lib/css/bootstrap.min.css",
                      "~/lib/css/font-awesome.min.css",
                      "~/lib/jh/jquery-ui-1.10.4.custom.min.css",
                      "~/Content/PagedList.css",
                      "~/lib/css/default.css",
                      "~/lib/css/style.css",
                      "~/lib/jh/jHTree.css",
                      "~/lib/css/responsive.css"));


            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                      "~/lib/js/vendor/modernizr-3.6.0.min.js",
                      "~/Scripts/jquery.js",
                      "~/lib/jh/jquery-ui-1.10.4.custom.min.js",
                      "~/lib/js/bootstrap.min.js",
                      "~/lib/js/slick.min.js",
                      "~/lib/jh/jQuery.jHTree.js",
                      "~/lib/js/main.js"));
        }
    }
}
