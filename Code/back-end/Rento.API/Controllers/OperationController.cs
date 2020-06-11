using Rento.Database;
using Rento.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rento.API.Controllers
{
    public class OperationController : BaseController
    {
        [HttpPost]
        public async Task<IHttpActionResult> List(RentoRequest request)
        {
            return Ok(await TryCatchResponse(request,  ValidateType.Active, async (RentoResponse<List<CarActionBaseInfo>> response) =>
            {
                response.Data= await CarManager.ListRequest(UserSession.Id, true);
            }));
        }
    }
}
