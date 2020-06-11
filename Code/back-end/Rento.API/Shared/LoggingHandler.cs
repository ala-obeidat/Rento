using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Rento.Entity;
using System.Web.Hosting;
using Rento.Helper;

namespace Rento.API.Shared
{
    public class LoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            try
            {
                IEnumerable<string> headerValue;
                bool valid = false;

                if (request.Headers.TryGetValues("ApplicationKey", out headerValue))
                {
                    if (Rento.Entity.Constant.APPLICATION_KEYS.Contains(headerValue.First()))
                    {
                        if (request.Headers.TryGetValues("ClientId", out headerValue))
                        {
                            var client = Clients.FirstOrDefault(c => c.Id == headerValue.First());
                            if (client != null)
                            {
                                if (request.Headers.TryGetValues("SecretKey", out headerValue))
                                {
                                    if (client.SecritKey.Equals(headerValue.First().ToHashString()))
                                    {
                                        
                                        string ip= HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                                        if(string.IsNullOrEmpty(ip))
                                        {
                                            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                                        }
                                        valid = true;
                                        Rento.Helper.Logger.Debug(request.ToString(), client.Name);
                                    }
                                }
                            }
                        }
                    }
                }
                if (!valid)
                    return request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "Forbidden to call API");

                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
                return response;
            }
            catch(Exception ex)
            {
                Logger.Exception(ex, "LoggingHandler");
                return request.CreateResponse(System.Net.HttpStatusCode.ExpectationFailed, "There is an error on server");
            }
        }
        private volatile static List<Client> _clients;
        private object _lockObject = new object();
        private List<Client> Clients
        {
            get
            {
                if (_clients == null || _clients.Count == 0)
                {
                    lock (_lockObject)
                    {
                        if (_clients == null || _clients.Count == 0)
                        {
                            _clients = Rento.Database.ClientManager.List();
                        }
                    }
                }
                return _clients;
            }
        }
    }
}