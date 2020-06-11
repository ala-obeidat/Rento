using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rento.Entity;
using Rento.Database;
using Rento.Helper;
using System.Threading.Tasks;

namespace Rento.Service.Controllers
{
    public class MessageController : BaseController
    {
        
        [HttpPost]
        public IHttpActionResult List(RentoRequest request)
        {
            Logger.Debug("Message - List", request);

            var response = new RentoResponse<List<Message>>(request);
            return Ok(TryCatch(request, response, ValidateType.Block, async () =>
            {
                
                response.Data = await Database.MessageManager.List(UserSession.Id);
            }));
        }

        [HttpPost]
        public IHttpActionResult Create(RentoRequest<Message> request)
        {
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {
                if (!ValidateRequirdField(request.Data.Body))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                await Database.MessageManager.Create(UserSession.Id, request.Data.Body);
            }));
        }

        [HttpPost]
        public IHttpActionResult SendSMS(RentoRequest<SMSMessageEntity> request)
        {
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Operation,  () =>
            {
                if (!ValidateRequirdField(request.Data.Body,request.Data.Mobile))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                SMSMessage.Send(SMSMessage.CheckMobileNumber(request.Data.Mobile), request.Data.Body);
            }));
        }

        [HttpPost]
        public IHttpActionResult CreateMobileNotificationMultiple(RentoRequest<MobileMessage> request)
        {
            Logger.Debug("CreateMobileNotificationMultiple", request);
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Admin, () =>
            {
                if (!ValidateRequirdField(request.Data.Body, request.Data.Title))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                switch ((MobileType)request.Data.Type)
                {
                    case MobileType.All:
                        Helper.FirebaseNotification.SendPushNotification(request.Data.Title, request.Data.Body, "/topics/erentusers", true);
                        Helper.FirebaseNotification.SendPushNotification(request.Data.Title, request.Data.Body, "/topics/erentusers", false);
                        break;
                    case MobileType.Android:
                        Helper.FirebaseNotification.SendPushNotification(request.Data.Title, request.Data.Body, "/topics/erentusers", true);
                        break;
                    case MobileType.iPhone:
                        Helper.FirebaseNotification.SendPushNotification(request.Data.Title, request.Data.Body, "/topics/erentusers", false);
                        break;
                    default:
                        break;
                }

            }));
        }
    }
}
