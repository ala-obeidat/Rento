using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rento.Entity;
using Rento.Database;
using Rento.Helper;
using System.Web.Hosting;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

namespace Rento.Service.Controllers
{
    public class AccountController : BaseController
    {
        const string FORGET_PASSWORD_KEY = "forgetPasswordKey";
        const string VERIFICATION_KEY = "verificationKey";
        const string RESEND_CODE_KEY = "RESEND_CODE_KEY";

        [HttpPost]
        public IHttpActionResult Login(RentoRequest<UserLogin> request)
        {

            Logger.Debug("Login", request);
            request.Data.Username = request.Data.Username.Trim();
            var response = new RentoResponse<UserLoginResponse>(request);
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
            {
                if (!ValidateRequirdField(request.Data.Username, request.Data.Password))
                {
                    response.ErrorCode = ErrorCode.RequirdField;
                    return;
                };
                UserSession = await AccountManager.Login(request.Data);
                var token = GenerateToken(request.Data.Username, UserSession.Id, UserSession.Type);
                if (UserSession == null)
                {
                    response.ErrorCode = ErrorCode.UserDoesNotExist;
                    return;
                }
                if ((UserType)UserSession.Type == UserType.Customer_Pending || (UserType)UserSession.Type == UserType.Customer_Blocked)
                {
                    response.ErrorCode = ErrorCode.InActiveUser;
                    if ((UserType)UserSession.Type == UserType.Customer_Pending)
                    {
                        var mobileNumber = await Database.AccountManager.SelectMobile(UserSession.Id);
                        SendVirificationCode(mobileNumber, UserSession.Id, token);
                        response.Data = new UserLoginResponse()
                        {
                            Id = UserSession.Id,
                            Type = UserSession.Type,
                            Token = token,
                            Username = request.Data.Username
                        };
                    }
                    else
                        return;
                }
                response.Data = new UserLoginResponse()
                {
                    Id = UserSession.Id,
                    Type = UserSession.Type,
                    Token = token,
                    Username = request.Data.Username
                };
            }));
        }

        [HttpPost]
        public IHttpActionResult AdminLogin(RentoRequest<AdminUserLogin> request)
        {
            Logger.Debug("AdminLogin", request);
            var response = new RentoResponse<UserLoginResponse>(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {

                if (!ValidateRequirdField(request.Data.Password) || !ValidateRequirdField(request.Data.RequestUserId))
                { response.ErrorCode = ErrorCode.RequirdField; return; }

                var userSession = await AccountManager.AdminLogin(UserSession.Id, request.Data);
                if (userSession == null)
                {
                    response.ErrorCode = ErrorCode.UserDoesNotExist;
                    return;
                }
                string token = GenerateToken(userSession.Username, userSession.Id, userSession.Type);
                response.Data = new UserLoginResponse()
                {
                    Id = userSession.Id,
                    Type = userSession.Type,
                    Token = token,
                    Username = userSession.Username
                };
            }));
        }

        [HttpPost]
        public IHttpActionResult RefreshToken(RentoRequest<TokenRefresh> request)
        {
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
            {
                await AccountManager.RefreshToken(request.Data);
            }));
        }


        [HttpPost]
        public IHttpActionResult ForgetPassword(RentoRequest<string> request)
        {
            Logger.Debug("ForgetPassword", request.Data);

            var response = new RentoResponse<string>(request);
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
            {
                if (!ValidateRequirdField(request.Data))
                { response.ErrorCode = ErrorCode.RequirdField; return; }
                var forgetPassword = await Database.AccountManager.SelectMobile(request.Data);
                if (forgetPassword != null)
                {
                    var code = StringHelper.GenerateRandomNumber(6);
                    RunActionAsync(() =>
                    {
                        SMSMessage.Send(forgetPassword.Mobile, code.ToString());
                    });
                    RentoCache.Set(FORGET_PASSWORD_KEY + forgetPassword.Token, new ForgetPasswordCacheObject()
                    {
                        Code = code,
                        UserId = forgetPassword.UserId
                    }, 1);
                    response.Data = forgetPassword.Token.ToString();
                }

            }));
        }

        [HttpPost]
        public IHttpActionResult ResendCode(RentoRequest request)
        {
            Logger.Debug("ResendCode", request);

            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Pending,
            async delegate ()
            {
                var mobileNumber = await AccountManager.SelectMobile(UserSession.Id);
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    var lastCache = RentoCache.Get<int>(RESEND_CODE_KEY + mobileNumber);
                    if (lastCache == 0)
                        lastCache = 1;
                    if (lastCache > 3)
                        response.ErrorCode = ErrorCode.GeneralError;
                    else
                    {
                        RentoCache.Set(RESEND_CODE_KEY + mobileNumber, lastCache++);

                        RentoCache.Set(mobileNumber, 1);
                        var code = StringHelper.GenerateRandomNumber(4);
                        Logger.Debug("Send Code Resend", new { Code = code, Mobile = mobileNumber });
                        RunActionAsync(() =>
                        {
                            SMSMessage.Send(mobileNumber, string.Format("Your Verification Code is: {0}", code));
                        });
                        RentoCache.Set(VERIFICATION_KEY + request.Token, new ForgetPasswordCacheObject()
                        {
                            Code = code,
                            UserId = UserSession.Id
                        }, 1);
                    }
                }

            }));
        }

        [HttpPost]
        public IHttpActionResult ResetPassword(RentoRequest<ResetPassword> request)
        {
            Logger.Debug("ResetPassword", request.Data);
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
            {
                if (!ValidateRequirdField(request.Data.Code) || !ValidateRequirdField(request.Data.NewPassword))
                {
                    response.ErrorCode = ErrorCode.RequirdField; return;
                }
                var cacheReponse = RentoCache.Get<ForgetPasswordCacheObject>(FORGET_PASSWORD_KEY + request.Token);
                if (cacheReponse != null)
                {
                    if (request.Data.Code.Equals(cacheReponse.Code))
                        await Database.AccountManager.ResetPassword(cacheReponse.UserId, request.Data.NewPassword);
                    else
                        response.ErrorCode = ErrorCode.InvalidCode;
                }
                else
                    response.ErrorCode = ErrorCode.UserDoesNotExist;
            }));
        }

        [HttpPost]
        public IHttpActionResult ChangePassword(RentoRequest<ChangePassword> request)
        {
            request.Data.NewPassword = request.Data.NewPassword.ToSafe();
            request.Data.OldPassword = request.Data.OldPassword;

            Logger.Debug("Change Password", request);
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
             {
                 if (!ValidateRequirdField(request.Data.OldPassword, request.Data.NewPassword))
                 {
                     response.ErrorCode = ErrorCode.RequirdField;
                     return;
                 };

                 if (!request.Data.NewPassword.CheckRegexValid(Constant.PASSWORD_EXPRESION))
                 {
                     response.ErrorCode = ErrorCode.InvalidPasswordFormat;
                     return;
                 };
                 var success = await Database.AccountManager.ChangePassword(UserSession.Id, request.Data);
                 if (!success)
                     response.ErrorCode = ErrorCode.OldPasswordNotMatch;

             }));

        }


        [HttpPost]
        public IHttpActionResult Verification(RentoRequest<Verification> request)
        {
            Logger.Debug("Verification", request);
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Pending, async () =>
            {
                if (!ValidateRequirdField(request.Data.Code))
                {
                    response.ErrorCode = ErrorCode.RequirdField;
                    return;
                };
                var cacheReponse = RentoCache.Get<ForgetPasswordCacheObject>(VERIFICATION_KEY + request.Token);
                if (cacheReponse != null)
                {
                    if (request.Data.Code.Equals(cacheReponse.Code))
                    {
                        await AccountManager.Verify(cacheReponse.UserId);
                        UserSession.Type = (int)UserType.Customer_Active;
                        GenerateToken(UserSession);
                    }
                    else
                        response.ErrorCode = ErrorCode.InvalidCode;
                }
                else
                    response.ErrorCode = ErrorCode.UserDoesNotExist;
            }));
        }

        [HttpPost]
        public IHttpActionResult SignUp(RentoRequest<Customer> request)
        {
            var userId = 0;
            var welcomeFilePath = string.Empty;
            var response = new RentoResponse<string>(request);
            return Ok(TryCatch(request, response, ValidateType.None, async () =>
            {
                var hasIdentifier = request.Data.Identifier != null && string.IsNullOrEmpty(request.Data.Identifier.Content);
                var hasLicence = request.Data.Identifier != null && string.IsNullOrEmpty(request.Data.Licence.Content);
                var identefier = string.Empty;
                var licence = string.Empty;
                if (hasIdentifier)
                {
                    identefier = request.Data.Identifier.Content;
                    request.Data.Identifier.Content = "Base64 string";
                }
                if (hasLicence)
                {
                    licence = request.Data.Licence.Content;
                    request.Data.Licence.Content = "Base64 string";
                }

                Logger.Debug("SignUp", request.Data);
                if (hasLicence)
                {
                    request.Data.Licence.Content = licence;
                    if (!FileExtension.ScanImage(request.Data.Licence.ContentArray))
                    {
                        response.ErrorCode = ErrorCode.InvalidImageFormat;
                        return;
                    }
                }
                if (hasIdentifier)
                {
                    request.Data.Identifier.Content = identefier;
                    if (!FileExtension.ScanImage(request.Data.Identifier.ContentArray))
                    {
                        response.ErrorCode = ErrorCode.InvalidImageFormat;
                        return;
                    }
                }


                if (!string.IsNullOrEmpty(request.Data.BirthDate))
                    request.Data.DOP = DateTime.ParseExact(request.Data.BirthDate, "dd/MM/yyyy", null);

                if (ValidateRequirdField(request.Data.IdentifierId)
                    &&
                    ValidateRequirdField(request.Data.DOP)
                   )
                    request.Data.Flag = (int)CustomerFlag.CompleteBySignUp;
                else
                    request.Data.Flag = (int)CustomerFlag.UnComplete;


                if (!ValidateRequirdField(request.Data.Mobile, request.Data.Password, request.Data.Username, request.Data.Email, request.Data.FullName))
                {
                    response.ErrorCode = ErrorCode.RequirdField;
                    return;
                }

                request.Data.Mobile = SMSMessage.CheckMobileNumber(request.Data.Mobile);
                userId = await AccountManager.SignUp(request.Data);
                if (userId == 0)
                {
                    response.ErrorCode = ErrorCode.UsernameAlreadyExists;
                    return;
                }
                RunActionAsync(() =>
                    {
                        var imagePath = HostingEnvironment.MapPath("~/App_Data");
                        var fileId = ImageHelper.WriteOnImage(imagePath + "/welcome.jpeg", request.Data.FullName, imagePath);
                        welcomeFilePath = Path.Combine(imagePath, fileId);
                        LinkedResource LinkedImage = new LinkedResource(welcomeFilePath);
                        LinkedImage.ContentId = "ERent";
                        LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<img src=cid:ERent>", null, "text/html");
                        htmlView.LinkedResources.Add(LinkedImage);
                        EmailMessage.SendEmail("erent@ashhalan.com", request.Data.Email, request.Language == (int)Language.Arabic ? "مرحبا بك في " + "E-Rent" : "Welcome to E-Rent", " ", null, htmlView);
                        if (!string.IsNullOrEmpty(welcomeFilePath) && File.Exists(welcomeFilePath))
                            File.Delete(welcomeFilePath);
                    });
                string token = GenerateToken(request.Data.Username, userId, (int)UserType.Customer_Pending);
                SendVirificationCode(request.Data.Mobile, userId, token);
                response.Data = token;
            }));
        }

        private void SendVirificationCode(string mobileNumber, int userId, string token)
        {
            var code = StringHelper.GenerateRandomNumber(4);
            Logger.Debug("Send Code To Sign up", new { UserId = userId, Code = code, Mobile = mobileNumber });
            RunActionAsync( () =>
            {
                SMSMessage.Send(mobileNumber, string.Format("Your Verification Code is: {0}", code));
            });
            RentoCache.Set(VERIFICATION_KEY + token, new ForgetPasswordCacheObject()
            {
                Code = code,
                UserId = userId
            }, 1);

        }

        [HttpPost]
        public IHttpActionResult UpdateCustomerInfo(RentoRequest<CustomerOptinal> request)
        {
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                var identefier = request.Data.Identifier.Content;
                request.Data.Identifier.Content = "Base64 string";
                var licence = request.Data.Licence.Content;
                request.Data.Licence.Content = "Base64 string";
                Logger.Debug("UpdateCustomerInfo", request.Data);
                request.Data.Licence.Content = licence;
                request.Data.Identifier.Content = identefier;

                if (!string.IsNullOrEmpty(request.Data.BirthDate))
                    request.Data.DOP = DateTime.ParseExact(request.Data.BirthDate, "dd/MM/yyyy", null);

                if (!ValidateRequirdField(request.Data.Identifier, request.Data.Licence)
                    ||
                    !ValidateRequirdField(request.Data.IdentifierId)
                    ||
                    !ValidateRequirdField(request.Data.DOP)
                    ||
                    !ValidateRequirdField(request.Data.Id)
                   )
                {
                    response.ErrorCode = ErrorCode.RequirdField;
                    return;
                }
                request.Data.Flag = (int)CustomerFlag.CompleteFromUpdate;
                await Database.AccountManager.UpdateCustomer(request.Data);
            }));
        }
    }
}