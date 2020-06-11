using Jil;
using Rento.API.Shared;
using System.Web.Http;

namespace Rento.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Clear();
            var _jilOptions = new Options(dateFormat: DateTimeFormat.ISO8601, excludeNulls: true, includeInherited: true);
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
