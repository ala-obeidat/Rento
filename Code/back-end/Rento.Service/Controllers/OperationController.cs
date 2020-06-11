using Rento.Database;
using Rento.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rento.Service.Controllers
{
    public class OperationController : BaseController
    {
        [HttpPost]
        public IHttpActionResult List(RentoRequest request)
        {
            var response = new RentoResponse<List<CarActionBaseInfo>>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                response.Data= await CarManager.ListRequest(UserSession.Id, true);
            }));
        }
    }
}
