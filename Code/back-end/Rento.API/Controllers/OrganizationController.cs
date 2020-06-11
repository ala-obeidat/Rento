using Rento.Database;
using Rento.Entity;
using Rento.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rento.API.Controllers
{
    public class OrganizationController : BaseController
    {

        [HttpPost]
        public async Task<IHttpActionResult> Get(RentoRequest<int> request)
        {
            Logger.Debug("Organization - Get", request);
            return Ok(await TryCatchResponse(request,  ValidateType.Operation, async (RentoResponse<Organization> response) =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data= await OrganizationManager.Select(UserSession.Id, request.Data);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> List(RentoRequest request)
        {
            Logger.Debug("Organization - List", request);
            return Ok(await TryCatchResponse(request,  ValidateType.Block, async (RentoResponse<List<Organization>> response) =>
            {
                response.Data = await OrganizationManager.List(UserSession.Id);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Save(RentoRequest<Organization> request)
        {
            Logger.Debug("Organization - Save", request);
            return Ok(await TryCatchResponseBase(request,  ValidateType.Operation, async (RentoResponse response) =>
            {
                if (!ValidateRequirdField(request.Data.Name))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                await Database.OrganizationManager.Save(UserSession.Id, request.Data);
            }));
        }

    }
}
