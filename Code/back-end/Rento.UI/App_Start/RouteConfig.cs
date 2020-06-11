using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rento.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if (lang.Length > 2)
                lang = lang.Substring(0, 2);


            routes.MapRoute(name: "DefaultWithLanguage"
                       , url: "{language}/{controller}/{action}/{id}"
                       , defaults: new { language = lang, controller = "Home", action = "Index", id = UrlParameter.Optional }
                       , constraints: new { language = "ar|en" }
        );

            routes.MapRoute(name: "Default"
                           , url: "{controller}/{action}/{id}"
                           , defaults: new { language = lang, controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
