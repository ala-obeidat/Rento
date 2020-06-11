using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rento.Entity;
using Rento.Database;
using Rento.Helper;

namespace Rento.Service.Controllers
{
    public class OfferController : BaseController
    {
        #region User

        [HttpPost]
        public IHttpActionResult List(RentoRequest request)
        {
            Logger.Debug("Offer - List", request);
            var response = new RentoResponse<List<Offer>>(request);
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
            {
                response.Data= await Database.OfferManager.List(0);
            }));
        }

        [HttpPost]
        public IHttpActionResult Create(RentoRequest<OfferCreate> request)
        {
            var response = new RentoResponse<BaseEntity>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                if (!ValidateRequirdField(request.Data.CarId,request.Data.Cost,request.Data.Discount)
                ||
                !ValidateRequirdField(request.Data.From, request.Data.To)
                )
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.OfferManager.Create(UserSession.Id, request.Data);
            }));
        }


        [HttpPost]
        public IHttpActionResult Delete(RentoRequest<int> request)
        {
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                await Database.OfferManager.Delete(UserSession.Id, request.Data);
            }));
        }

        [HttpPost]
        public IHttpActionResult Update(RentoRequest<OfferCreate> request)
        {
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                if (!ValidateRequirdField(request.Data.CarId, request.Data.Cost, request.Data.Discount)
                 ||
                 !ValidateRequirdField(request.Data.From, request.Data.To)
                 )
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                if (request.Data.Id == 0)
                    request.Data.Id = UserSession.Id;
                await Database.OfferManager.Update(UserSession.Id, request.Data);
            }));
        }

        #endregion
    }
}
