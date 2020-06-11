using Rento.Entity;
using Rento.Helper;
using Rento.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rento.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Policy()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactPost(Contact model)
        {
            var success = false;
            try
            {
                var bodyHtml = Rento.Helper.EmailMessage.BuildBodyHTML(model.Subject, model.Email, model.Mobile, model.Name, model.Body);
                success = Rento.Helper.EmailMessage.SendEmail(model.Email, "erent@ashhalan.com", "E-Rent Contact Message", bodyHtml);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }
            var request = new RentoRequest() { Language = Shared.FixData.IsRTL ? (int)Language.Arabic : (int)Language.English };
            if (success)
                return RentoJson(new RentoResponse(request));
            return RentoJson(new RentoResponse(request) { ErrorCode = ErrorCode.GeneralError });

        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Error = "";
            ViewBag.PasswordKey = StringHelper.GetUniqeKey();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginPost()
        {
            try
            {
                Logger.Debug("LoginPost", Request.Form["Username"]);

                var userLoginResponse = await CallApiEmpty<UserLogin, UserLoginResponse>("Account/Login", new UserLogin()
                {
                    Username = Request.Form["Username"],
                    Password = Request.Form["Password"].FromRentoBase64(Request.Form["Key"])
                });
                switch (userLoginResponse.ErrorCode)
                {
                    case ErrorCode.Success:
                        Session["USERNAME"] = userLoginResponse.Data.Username;
                        Session["Token"] = userLoginResponse.Data.Token;
                        Session["UserId"] = userLoginResponse.Data.Id;
                        await Rento.UI.Shared.FixData.InitilazeRentoConstantData();
                        Session["USER_TYPE"] = userLoginResponse.Data.Type;
                        switch ((UserType)userLoginResponse.Data.Type)
                        {
                            case UserType.Pending:
                                return RedirectToAction("Index", "User");
                            case UserType.Administrator:
                                return RedirectToAction("Index", "Management");
                            case UserType.Blocked:
                                return RedirectToAction("Index", "Message");
                            case UserType.Operation:
                                return RedirectToAction("Index", "Order");
                            default:
                                return RedirectToAction("Index", "Car");
                        }
                    case ErrorCode.UserDoesNotExist:
                        Session["Token"] = null;
                        ViewBag.Error = Resources.Resource.InvalidUsernameOrPassword;
                        break;
                    default:
                        Session["Token"] = null;
                        ViewBag.Error = Resources.Resource.GeneralError;
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                ViewBag.Error = Resources.Resource.GeneralError;
            }
            ViewBag.PasswordKey = StringHelper.GetUniqeKey();
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> AdminLogin(AdminUserLogin model)
        {
            var errorMessage = string.Empty;
            var redirectURL = Url.Action("Logout");
            var errorCode = ErrorCode.GeneralError;
            try
            {
                model.Password = model.Password.FromRentoBase64(model.Key);
                Logger.Debug("AdminLogin", model.RequestUserId);

                var userLoginResponse = await CallApi<AdminUserLogin, UserLoginResponse>("Account/AdminLogin", model);
                errorCode = userLoginResponse.ErrorCode;
                switch (errorCode)
                {
                    case ErrorCode.Success:
                        errorMessage = string.Format(Resources.Resource.AdminLogin, userLoginResponse.Data.Username);
                        Session["USERNAME"] = userLoginResponse.Data.Username;
                        Session["Token"] = userLoginResponse.Data.Token;
                        Session["UserId"] = userLoginResponse.Data.Id;
                        await Rento.UI.Shared.FixData.InitilazeRentoConstantData();
                        Session["USER_TYPE"] = userLoginResponse.Data.Type;
                        switch ((UserType)userLoginResponse.Data.Type)
                        {
                            case UserType.Pending:
                                redirectURL = Url.Action("Index", "User");
                                break;
                            case UserType.Administrator:
                                redirectURL = Url.Action("Index", "Management");
                                break;
                            case UserType.Blocked:
                                redirectURL = Url.Action("Index", "Message");
                                break;
                            case UserType.Operation:
                                redirectURL = Url.Action("Index", "Order");
                                break;
                            default:
                                redirectURL = Url.Action("Index", "Car");
                                break;
                        }
                        break;
                    case ErrorCode.UserDoesNotExist:
                        errorMessage = Resources.Resource.InvalidUsernameOrPassword;
                        break;
                    default:
                        errorMessage = Resources.Resource.GeneralError;
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                errorMessage = Resources.Resource.GeneralError;
            }
            return RentoJsonObject(new {ErrorCode= errorCode, Message = errorMessage, URL = redirectURL });
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var lang = Session["M_LANGUAGE"].ToString();
            Session.Clear();
            Session["M_LANGUAGE"] = lang;
            return RedirectToAction("Login");
        }
    }
}