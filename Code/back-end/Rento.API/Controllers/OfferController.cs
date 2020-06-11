
using System.Collections.Generic;
using System.Web.Http;
using Rento.Entity;
using Rento.Helper;
using System.Threading.Tasks;

namespace Rento.API.Controllers
{
    public class OfferController : BaseController
    {
        #region User

        [HttpPost]
        public async Task<IHttpActionResult> List(RentoRequest request)
        {
            Logger.Debug("Offer - List", request);
            return Ok(await TryCatchResponse(request,  ValidateType.None, async (RentoResponse<List<Offer>> response) =>
            {
                response.Data= await Database.OfferManager.List(0);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(RentoRequest<OfferCreate> request)
        {
            return Ok(await TryCatchResponse(request,  ValidateType.Active, async (RentoResponse<BaseEntity> response) =>
            {
                if (!ValidateRequirdField(request.Data.CarId,request.Data.Cost,request.Data.Discount)
                ||
                !ValidateRequirdField(request.Data.From, request.Data.To)
                )
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await Database.OfferManager.Create(UserSession.Id, request.Data);
            }));
        }
        
        #endregion
    }
}
