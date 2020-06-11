using Rento.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rento.Service.Controllers
{
    public class LookUpController : BaseController
    {
        [HttpPost]
        public IHttpActionResult List(RentoRequest<string> request)
        {
            var response = new RentoResponse<List<Entity.BaseNameEntity>>(request);
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.LookUpManager.List(0, request.Data);
            }));
        }

        [HttpPost]
        public IHttpActionResult ListExternal(RentoRequest<string> request)
        {

            var response = new RentoResponse<List<Entity.BaseNameEntity<int>>>(request);
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.LookUpManager.List<int>(0, request.Data);
            }));

        }

        [HttpPost]
        public IHttpActionResult Save(RentoRequest<LookUp> request)
        {
            var response = new RentoResponse<Entity.BaseEntity>(request);
            return Ok(TryCatch(request, response, ValidateType.Admin, async () =>
            {
                if (!ValidateRequirdField(request.Data.Name, request.Data.NameEn))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.LookUpManager.Save(UserSession.Id, request.Data);
            }));
        }

        [HttpPost]
        public IHttpActionResult SaveExternal(RentoRequest<LookUp<int>> request)
        {
            var response = new RentoResponse<Entity.BaseEntity>(request);
            return Ok(TryCatch(request, response, ValidateType.Admin, async () =>
            {
                if (!ValidateRequirdField(request.Data.Name, request.Data.NameEn)
                    || !ValidateRequirdField(request.Data.ExternalData)
                    )
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.LookUpManager.Save<int>(UserSession.Id, request.Data);
            }));
        }

        [HttpPost]
        public IHttpActionResult Delete(RentoRequest<LookUpDelete> request)
        {

            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Admin, async () =>
            {
                if (!ValidateRequirdField(request.Data.Id))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                await Database.LookUpManager.Delete(UserSession.Id, request.Data);
            }));
        }
    }
}
