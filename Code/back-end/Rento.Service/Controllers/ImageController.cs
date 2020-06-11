using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rento.Entity;
using Rento.Database;

namespace Rento.Service.Controllers
{
    public class ImageController : BaseController
    {

        [HttpPost]
        public IHttpActionResult Get(RentoRequest<int> request)
        {
            var response = new RentoResponse<RentoImage>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.ImageManager.Select(UserSession.Id, request.Data);
            }));
        }
    }
}