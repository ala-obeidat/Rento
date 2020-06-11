using Rento.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;

namespace Rento.Service.Controllers
{
    public class HelpController : BaseController
    {
        [HttpPost]
        public IHttpActionResult GetTermsAndCondition(RentoRequest<int> request)
        {
            var response = new RentoResponse<string>(request);
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
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
        public IHttpActionResult MessageContact(RentoRequest<ContactMessage> request)
        {
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.None,  () =>
            {
                var model = request.Data;
                var bodyHtml = Rento.Helper.EmailMessage.BuildBodyHTML(model.Subject, model.Email, model.Mobile, model.Name, model.Body);
                RunActionAsync(() =>
                {
                     Rento.Helper.EmailMessage.SendEmail(model.Email, "erent@ashhalan.com", "E-Rent Contact Message", bodyHtml);
                });
            }));
        }

        
    }
}
