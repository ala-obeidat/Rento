using Jil;
using Rento.Service.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Rento.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Clear();
            var _jilOptions = new Options(dateFormat: DateTimeFormat.ISO8601, excludeNulls: false, includeInherited: true);
            config.Formatters.Add(new JilFormatter(_jilOptions));
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
