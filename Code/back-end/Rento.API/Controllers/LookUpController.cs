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
    public class LookUpController : BaseController
    {
        [HttpPost]
        public async Task<IHttpActionResult> List(RentoRequest<string> request)
        {
            return Ok(await TryCatchResponse(request,  ValidateType.None, async (RentoResponse<List<Entity.BaseNameEntity>> response) =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.LookUpManager.List(0, request.Data);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> ListExternal(RentoRequest<string> request)
        {

            return Ok(await TryCatchResponse(request,  ValidateType.None, async (RentoResponse<List<Entity.BaseNameEntity<int>>> response) =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.LookUpManager.List<int>(0, request.Data);
            }));

        }

        [HttpPost]
        public async Task<IHttpActionResult> Save(RentoRequest<LookUp> request)
        {
            return Ok(await TryCatchResponse(request,  ValidateType.Admin, async (RentoResponse<Entity.BaseEntity> response) =>
            {
                if (!ValidateRequirdField(request.Data.Name, request.Data.NameEn))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.LookUpManager.Save(UserSession.Id, request.Data);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> SaveExternal(RentoRequest<LookUp<int>> request)
        {
            return Ok(await TryCatchResponse(request,  ValidateType.Admin, async (RentoResponse<Entity.BaseEntity> response) =>
            {
                if (!ValidateRequirdField(request.Data.Name, request.Data.NameEn)
                    || !ValidateRequirdField(request.Data.ExternalData)
                    )
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.LookUpManager.Save<int>(UserSession.Id, request.Data);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Delete(RentoRequest<LookUpDelete> request)
        {
            return Ok(await TryCatchResponseBase(request,  ValidateType.Admin, async (RentoResponse response) =>
            {
                if (!ValidateRequirdField(request.Data.Id))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                await Database.LookUpManager.Delete(UserSession.Id, request.Data);
            }));
        }
    }
}
