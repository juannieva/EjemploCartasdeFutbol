using System.Web;
using System.Web.Optimization;

namespace WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/Scripts/jquery", "https://code.jquery.com/jquery-3.6.0.js").Include(
                        "~/Scripts/jquery-3.6.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap.bundle.min.js"));


               bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/Mi-estilo.css"));

            bundles.Add(new ScriptBundle("~/Scripts/select2").Include(
                      "~/Scripts/Select2*"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery-unobtrusive").Include(
                      "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/Scripts/MyScript").Include(
                      "~/Scripts/MyScript*"));

            bundles.Add(new StyleBundle("~/Content/select2").Include(
                      "~/Content/Select2.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}