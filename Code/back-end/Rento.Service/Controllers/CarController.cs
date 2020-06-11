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
    public class CarController : BaseController
    {
        #region CarItem

        [HttpPost]
        public IHttpActionResult ChangeStatus(RentoRequest<int> request)
        {
            Logger.Debug("Car - ChangeStatus", request);
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                await CarManager.ChangeStatus(UserSession.Id, request.Data);
            }));

        }


        [HttpPost]
        public IHttpActionResult List(RentoRequest<CarListRequest> request)
        {
            Logger.Debug("Car - List", request);

            var response = new RentoResponse<List<CarBaseInfo>>(request);
            
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
              {
                  var userType = UserType.Active;
                  var reult = await ValidateRequst(request);
                  if (!string.IsNullOrEmpty(request.Token) && reult.Key)
                      userType = (UserType)UserSession.Type;
                  var tempReponse= await CarManager.List(0, request.Data, request.PageNumber, Constant.PAGE_SIZE, userType);
                  response.RowsCount =tempReponse.RowsCount;
                  response.Data = tempReponse.Data;
              }));
        }

        [HttpPost]
        public IHttpActionResult UserList(RentoRequest<CarListRequest> request)
        {
            Logger.Debug("Car - UserList", request);
            var response = new RentoResponse<List<CarBaseInfo>>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {


                if (request.Data != null && !ValidateRequirdField(request.Data.CityId))
                {
                    response.ErrorCode = ErrorCode.RequirdField;
                    return;
                }
               var tempResponse = await CarManager.List(UserSession.Id, request.Data);
                response.Data = tempResponse.Data;
                response.RowsCount = tempResponse.RowsCount;


            }));
        }

        [HttpPost]
        public IHttpActionResult Save(RentoRequest<Car> request)
        {
            var response = new RentoResponse<BaseEntity>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                List<RentoImage> tempImages = request.Data.Images;
                if (request.Data.Images != null && request.Data.Images.Count > 0)
                {
                    foreach (var image in request.Data.Images)
                    {
                        if (!Rento.Helper.FileExtension.ScanImage(image.Content))
                        {
                            response.ErrorCode = ErrorCode.InvalidImageFormat;
                            return;
                        }
                    }
                    request.Data.Images = null;
                }
                Logger.Debug("Car -  Save", request);
                request.Data.Images = tempImages;
                tempImages = null;
                response.Data = await CarManager.Save(UserSession.Id, request.Data);
            }));
        }

        [HttpPost]
        public IHttpActionResult Delete(RentoRequest<int> request)
        {
            Logger.Debug("Car -  Delete", request);
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {

                var code = await CarManager.Delete(UserSession.Id, request.Data);
                if (code == 0)
                    response.ErrorCode = ErrorCode.CarLinkWithOrder;
            }));
        }

        [HttpPost]
        public IHttpActionResult Get(RentoRequest<int> request)
        {
            Logger.Debug("Car -  Get", request);
            var response = new RentoResponse<Car>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                response.Data = await CarManager.Select(UserSession.Id, request.Data);
            }));
        }

        #endregion

        #region Customer

        [HttpPost]
        public IHttpActionResult ListRequest(RentoRequest<bool> request)
        {
            Logger.Debug("ListRequest", request);
            var response = new RentoResponse<List<CarActionBaseInfo>>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                response.Data = await  CarManager.ListRequest(UserSession.Id, request.Data);
            }));
        }

        [HttpPost]
        public IHttpActionResult Checkout(RentoRequest<Checkout> request)
        {
            Logger.Debug("Checkout", request);
            var response = new RentoResponse<int>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {

                var daysPeriod = (request.Data.To - request.Data.From).Days;
                if (!ValidateRequirdField(request.Data.CarId, request.Data.Price)
                    ||
            !ValidateRequirdField(request.Data.From, request.Data.To)
                    ||
                    (daysPeriod < 1)
                    ||
            (request.Data.Flag == (int)CheckoutFlag.DeliverToMyLocation && (request.Data.Location == null || !ValidateRequirdField(request.Data.Location.Longitude, request.Data.Location.Latitude))))
                {
                    response.ErrorCode = ErrorCode.RequirdField;
                    return;
                }
                var checkOutId = await CarManager.Checkout(UserSession.Id, request.Data);
                switch (checkOutId)
                {
                    case -1:
                        response.ErrorCode = ErrorCode.CustomerNonComplete;
                        return;
                    case 0:
                        response.ErrorCode = ErrorCode.CarAlreadyReserved;
                        return;
                    default:
                        await Database.MessageManager.Create(UserSession.Id, string.Format("{0} {1}",
                        request.Language == (int)Language.Arabic ?
                        "تم ارسال طلب استئجار سيارة من قبلكم بنجاح ورقم الحجر هو" :
                        "Car rent order request done successfully with number", checkOutId
                        ));
                        break;
                }
                response.Data = checkOutId;
            }));
        }

        [HttpPost]
        public IHttpActionResult CloseBooking(RentoRequest<CloseBooking> request)
        {
            Logger.Debug("CloseBooking", request);
            var response = new RentoResponse(request);

            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {

                if (!ValidateRequirdField(request.Data.CheckoutId)
                    ||
                    !ValidateRequirdField(request.Data.Star)
                    ||
                    (request.Data.Flag == (int)CheckoutFlag.Rejected && !ValidateRequirdField(request.Data.Comment)))
                {
                    response.ErrorCode = ErrorCode.RequirdField;
                    return;
                }
                if (request.Data.Flag == (int)CheckoutFlag.GetFromOffice || request.Data.Flag == (int)CheckoutFlag.Rejected)
                    request.Data.Flag = (int)CheckoutFlag.CustomerRejected;
               await  CarManager.CloseBooking(UserSession.Id, request.Data);
            }));
        }


        #endregion

        #region Provider

        [HttpGet]
        public IHttpActionResult ListPendingOrder(string key, string token, int lang)
        {
            var request = new RentoRequest()
            {
                Language = lang,
                Token = token
            };
            var response = new RentoResponse<PendngOrder>(request);

            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
              {
                  response.Data = await CarManager.ListPendingOrder(UserSession.Id);
              }));
        }

        [HttpPost]
        public IHttpActionResult ListOrder(RentoRequest<int> request)
        {
            var response = new RentoResponse<List<Order>>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {

                if (!ValidateRequirdField(request.PageNumber))
                {
                    response.ErrorCode = ErrorCode.RequirdField;
                    return;
                };
                var temp= await CarManager.ListOrder((UserType)UserSession.Type == UserType.Operation ? 0 : UserSession.Id, request.PageNumber, Constant.PAGE_SIZE, request.Data);
                response.RowsCount = temp.RowsCount;
                response.Data = temp.Data;
            }));
        }

        [HttpPost]
        public IHttpActionResult ListOrderAction(RentoRequest<int> request)
        {
            var response = new RentoResponse<OrderAction>(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {
                if (!ValidateRequirdField(request.PageNumber))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                var rowsCount = 0;
                response.Data = await CarManager.ListOrderAction(request.Data);
                response.RowsCount = rowsCount;
            }));
        }

        [HttpPost]
        public IHttpActionResult ViewOrder(RentoRequest<int> request)
        {

            var response = new RentoResponse<OrderItem>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                response.Data = await CarManager.ViewOrder(UserSession.Id, request.Data);
            }));


        }

        [HttpPost]
        public IHttpActionResult CloseOrder(RentoRequest<OrderClose> request)
        {
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                if ((!request.Data.Approve && !ValidateRequirdField(request.Data.Comment))
                ||
                !ValidateRequirdField(request.Data.Star)
                )
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                var userToken = await CarManager.CloseOrder(UserSession.Id, request.Data);
                if (userToken != null)
                {
                    var bodyAr = request.Data.Approve ? "لقد تم الموافقة على طلبكم من قبل المكتب" : "لقد تم رفض طلبكم من قبل المكتب وذلك بسبب: " + request.Data.Comment;
                    var bodyEn = request.Data.Approve ? "Your request has been processed successfully" : "Your request has been rejected from office and the reason is: " + request.Data.Comment;
                    await Database.MessageManager.Create(userToken.CustomerId, string.Format("{0} {1}",
                            request.Language == (int)Language.Arabic ? bodyAr + " ذو الرقم " : bodyEn + " with number ", request.Data.Id
                            ));
                    if (!string.IsNullOrEmpty(userToken.NotificationToken))
                        FirebaseNotification.SendPushNotification("طلب استئجار السيارة", bodyAr, userToken.NotificationToken, userToken.IsAndroid);
                }
            }));
        }
        #endregion
    }
}
