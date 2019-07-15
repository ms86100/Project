using System.Web;
using System.Web.Optimization;

namespace Brothers
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/utility").Include(
                        //"~/Content/Plugins/Select2/select2.min.js",
                        "~/Content/bootstrap-datetimepicker.min.js",
                        "~/Content/Plugins/BoxSlider/jquery.bxslider.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Plugins/Select2/select2.min.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/Plugins/BoxSlider/jquery.bxslider.css"));


            bundles.Add(new StyleBundle("~/Content/frontcss").Include(
                      "~/MountPandim/css/owl.carousel.css",
                      "~/MountPandim/css/idangerous.swiper.css",
                      "~/MountPandim/css/style.css"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/FrontJS").Include(
                   "~/MountPandim/js/jquery.1.7.1.js",
                   "~/MountPandim/js/idangerous.swiper.js",
                   "~/MountPandim/js/slideInit.js",
                   "~/MountPandim/js/jqeury.appear.js",
                   "~/MountPandim/js/script.js",
                   "~/MountPandim/js/owl.carousel.min.js",
                   "~/MountPandim/js/custom.select.js"
                   ));

        }
    }
}
