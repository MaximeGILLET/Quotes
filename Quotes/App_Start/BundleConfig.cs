using System.Web;
using System.Web.Optimization;

namespace Quotes
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            /*** SCRIPT BUNDLES ***/
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Quotes").Include(
                        "~/Scripts/UI/Quotes/Quotes.js"));

            bundles.Add(new ScriptBundle("~/bundles/Calendar").Include(
                      "~/Scripts/Plugins/Calendar/calendar.js",
                       "~/Scripts/UI/calendarInit.js"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/DataTable").Include(
                     "~/Content/calendar/datatables.min.js"));

            /*** STYLE BUNDLES ***/
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/CalendarStyle").Include(
                      "~/Content/calendar/calendar.min.css"));

            bundles.Add(new StyleBundle("~/bundles/DataTable").Include(
                     "~/Content/calendar/datatables.min.css"));
        }
    }
}
