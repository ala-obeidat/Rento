using System.Web;
using System.Web.Optimization;

namespace Rento.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/MainJs").Include(
                    "~/assets/js/main.js",
                    "~/assets/plugin/JQuery/jquery.js",
                    "~/assets/plugin/bootstrap/bootstrap.min.js",
                    "~/assets/plugin/data-table/js/Grid.js",
                    "~/assets/plugin/data-table/js/grid.partial.js",
                    "~/assets/plugin/data-table/js/jquery.dataTables.js",
                    "~/assets/plugin/data-table/js/shCore.js",
                    "~/assets/plugin/dialog/js/alertify.js",
                    "~/assets/plugin/dialog/js/Dialog.js"
                       ));

            bundles.Add(new StyleBundle("~/assets/Login").Include(
                "~/assets/css/Login/login.css"
                ));
            bundles.Add(new StyleBundle("~/assets/LoginAr").Include(
                "~/assets/css/Login/loginAr.css"
                ));

            bundles.Add(new StyleBundle("~/assets/User/Update").Include(
                "~/assets/css/User/update.css"
                ));
            bundles.Add(new StyleBundle("~/assets/User/UpdateAr").Include(
                "~/assets/css/User/updateAr.css"
                ));

            bundles.Add(new StyleBundle("~/assets/Car/List").Include(
                "~/assets/css/Car/list.css"
                ));
            bundles.Add(new StyleBundle("~/assets/Car/ListAr").Include(
                "~/assets/css/Car/listAr.css"
                ));

            bundles.Add(new StyleBundle("~/assets/Car/Save").Include(
                "~/assets/css/Car/save.css"
                ));
            bundles.Add(new StyleBundle("~/assets/Car/SaveAr").Include(
                "~/assets/css/Car/saveAr.css"
                ));
            bundles.Add(new ScriptBundle("~/assets/Color").Include(
                "~/assets/plugin/JsColor/jscolor.js"
                ));
            bundles.Add(new ScriptBundle("~/Bundle/OrderIndex").Include(
                "~/assets/js/order-index.js"
                ));
            bundles.Add(new ScriptBundle("~/Bundle/CustomerMobileIndex").Include(
                "~/assets/js/customerMobile-index.js"
                ));
            
            bundles.Add(new ScriptBundle("~/Bundle/CarList").Include(
                "~/assets/js/car-list.js"
                ));

            bundles.Add(new StyleBundle("~/assets/Main").Include(
                "~/assets/css/Main/main.css",
                "~/assets/plugin/bootstrap/bootstrap.min.css",
                "~/assets/plugin/data-table/css/grid.ltr.css",
                "~/assets/plugin/data-table/css/jquery.dataTables.css",
                "~/assets/plugin/data-table/css/shCore.css",
                "~/assets/plugin/dialog/css/alertify.css"
                ));
            bundles.Add(new StyleBundle("~/assets/MainAr").Include(
                "~/assets/css/Main/mainAr.css",
                "~/assets/plugin/bootstrap/bootstrap.min.css",
                "~/assets/plugin/data-table/css/grid.rtl.css",
                "~/assets/plugin/data-table/css/jquery.dataTables.css",
                "~/assets/plugin/data-table/css/shCore.css",
                "~/assets/plugin/dialog/css/alertify.rtl.css"
                ));
        }
    }
}
