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

namespace Rento.API.Controllers
{
    public class MessageController : BaseController
    {
        
        [HttpPost]
        public async Task<IHttpActionResult> List(RentoRequest request)
        {
            Logger.Debug("Message - List", request);
            return Ok(await TryCatchResponse(request,  ValidateType.Block, async (RentoResponse<List<Message>> response) =>
            {
                
                response.Data = await Database.MessageManager.List(UserSession.Id);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(RentoRequest<Message> request)
        {
            return Ok(await TryCatchResponseBase(request,  ValidateType.Operation, async (RentoResponse response) =>
            {
                if (!ValidateRequirdField(request.Data.Body))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                await Database.MessageManager.Create(UserSession.Id, request.Data.Body);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> SendSMS(RentoRequest<SMSMessageEntity> request)
        {
            return Ok(await TryCatchSyncBase(request,  ValidateType.Operation,  (RentoResponse response) =>
            {
                if (!ValidateRequirdField(request.Data.Body,request.Data.Mobile))
                { response.ErrorCode = ErrorCode.RequirdField; return; };
                SMSMessage.Send(SMSMessage.CheckMobileNumber(request.Data.Mobile), request.Data.Body);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateMobileNotificationMultiple(RentoRequest<MobileMessage> request)
        {
            Logger.Debug("CreateMobileNotificationMultiple", request);
            return Ok(await  TryCatchSyncBase(request,  ValidateType.Admin, (RentoResponse response) =>
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
