using Rento.API.Shared;
using System.Web.Http;

namespace Rento.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new LoggingHandler());
        }
    }
}
