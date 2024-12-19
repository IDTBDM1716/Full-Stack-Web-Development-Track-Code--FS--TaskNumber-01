using System.Web;
using System.Web.Optimization;

namespace secure_user_authentication_system
{
    public class BundleConfig
    {
        // Register bundles for the application
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bundle for jQuery (include minified version)
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Bundle for jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            // Bundle for Modernizr (optional, used for feature detection)
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Bundle for Bootstrap JS
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js")); // Use bootstrap.bundle.js (includes popper.js)

            // Bundle for Bootstrap and custom site CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Enable optimizations for production mode
            BundleTable.EnableOptimizations = true;
        }
    }
}
