using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rento.Entity;
using Rento.Database;
using System.Threading.Tasks;

namespace Rento.API.Controllers
{
    public class ImageController : BaseController
    {

        [HttpPost]
        public async Task<IHttpActionResult> Get(RentoRequest<int> request)
        {
            return Ok(await TryCatchResponse(request,  ValidateType.Active, async (RentoResponse<RentoImage> response) =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.ImageManager.Select(UserSession.Id, request.Data);
            }));
        }
    }
}