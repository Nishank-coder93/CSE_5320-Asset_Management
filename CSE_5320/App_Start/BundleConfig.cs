using System.Web;
using System.Web.Optimization;

namespace CSE_5320
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                       "~/Scripts/Chart.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/map").Include(
                      "~/Scripts/anmap.js",
                      "~/Scripts/usaLow.js",
                      "~/Scripts/export.min.js",
                      "~/Scripts/light.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                      "~/Scripts/dashboard.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/dashboard").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/offcanvas.css"));
        }
    }
}
