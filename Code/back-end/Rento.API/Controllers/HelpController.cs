using Rento.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace Rento.API.Controllers
{
    public class HelpController : BaseController
    {
        [HttpPost]
        public async Task<IHttpActionResult> GetTermsAndCondition(RentoRequest<int> request)
        {
            return Ok(await TryCatchResponse(request,  ValidateType.None, async (RentoResponse<string> response) =>
            {
                if (request.Data == 0)
                {
                    if (request.Language == (int)Language.English)
                        response.Data = ReadFileFromAppData("termsEn");
                    response.Data= ReadFileFromAppData("termsAr");
                }
                response.Data = await Database.UserManager.GetGetTermsAndCondition(request.Data);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> MessageContact(RentoRequest<ContactMessage> request)
        {
            return Ok(await TryCatchSyncBase(request,  ValidateType.None,  (RentoResponse response) =>
            {
                var model = request.Data;
                var bodyHtml = Rento.Helper.EmailMessage.BuildBodyHTML(model.Subject, model.Email, model.Mobile, model.Name, model.Body);
                RunActionAsync(() =>
                {
                     Rento.Helper.EmailMessage.SendEmail(model.Email, "erent@ashhalan.com", "E-Rent Contact Message", bodyHtml);
                });
            }));
        }
        [HttpGet]
        public HttpResponseMessage Health(string key)
        {
            
            if (key.Equals(Entity.Constant.APPLICATION_KEY, StringComparison.OrdinalIgnoreCase))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Error");
            
        }
    }
}
