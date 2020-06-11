using Rento.Database;
using Rento.Entity;
using Rento.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rento.Service.Controllers
{
    public class OrganizationController : BaseController
    {

        [HttpPost]
        public IHttpActionResult Get(RentoRequest<int> request)
        {
            Logger.Debug("Organization - Get", request);
            var response = new RentoResponse<Organization>(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data= await OrganizationManager.Select(UserSession.Id, request.Data);
            }));
        }

        [HttpPost]
        public IHttpActionResult List(RentoRequest request)
        {
            Logger.Debug("Organization - List", request);
            var response = new RentoResponse<List<Organization>>(request);
            return Ok(TryCatch(request, response, ValidateType.Block, async () =>
            {
                response.Data = await OrganizationManager.List(UserSession.Id);
            }));
        }

        [HttpPost]
        public IHttpActionResult Save(RentoRequest<Organization> request)
        {
            Logger.Debug("Organization - Save", request);
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {
                if (!ValidateRequirdField(request.Data.Name))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                await Database.OrganizationManager.Save(UserSession.Id, request.Data);
            }));
        }

    }
}
