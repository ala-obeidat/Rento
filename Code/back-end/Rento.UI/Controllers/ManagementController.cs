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
    public class ManagementController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.PasswordKey = StringHelper.GetUniqeKey();
                var messageReposnse = await CallApi<List<BaseNameEntity<UserType>>>("User/List");
                if (messageReposnse.ErrorCode == ErrorCode.Success && messageReposnse.Data != null)
                    return View(messageReposnse.Data);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Save(UserLogin userLogin)
        {
            try
            {
                var messageReposnse = await CallApiTask<UserLogin>("User/Create", userLogin);
                return RentoJson(messageReposnse);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return RentoJsonError();
        }
        [HttpGet]
        public ActionResult Update(int id, int type)
        {
            var model = new UpdateUserFlagRequest()
            {
                UserId = id,
                Flag = type
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateFlag(UpdateUserFlagRequest userLogin)
        {
            try
            {
                var messageReposnse = await CallApiTask<UpdateUserFlagRequest>("User/UpdateFlag", userLogin);
                return RentoJson(messageReposnse);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            return RentoJsonError();
        }
    }
}