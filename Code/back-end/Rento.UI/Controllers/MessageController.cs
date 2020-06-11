using Rento.Entity;
using Rento.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rento.UI.Controllers
{
    public class MessageController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var messageReposnse = await CallApi<List<Message>>("Message/List");
                if (messageReposnse.ErrorCode == ErrorCode.Success && messageReposnse.Data != null)
                    return View(messageReposnse.Data);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return View();
        }
        public ActionResult MobileNotification()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> MobileNotificationPost(MobileMessage model)
        {
            return RentoJson(await CallApiTask("Message/CreateMobileNotificationMultiple", model));
        }
        public ActionResult SMS()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SMSPost(SMSMessageEntity model)
        {
            return RentoJson(await CallApiTask("Message/SendSMS", model));
        }
    }
}