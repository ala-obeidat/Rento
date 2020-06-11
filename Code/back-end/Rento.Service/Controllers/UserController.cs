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
    public class UserController : BaseController
    {
        #region Mobile Customer

        [HttpPost]
        public IHttpActionResult ListMobileCustomer(RentoRequest<MobileCustomerRequest> request)
        {
            Logger.Debug("User - ListMobileCustomer", request);
            var response = new RentoResponse<List<MobileCustomerResponse>>(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {
                var tempResponse = await Database.AccountManager.ListMobileCustomer(request.Data, request.PageNumber, Constant.PAGE_SIZE);
                response.Data = tempResponse.Data;
                response.RowsCount = tempResponse.RowsCount;
            }));
        }

        #endregion

        #region User

        [HttpPost]
        public IHttpActionResult List(RentoRequest request)
        {
            Logger.Debug("User - List", request);
            var response = new RentoResponse<List<BaseNameEntity<UserType>>>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                response.Data = await Database.AccountManager.List((UserType)UserSession.Type);
            }));
        }

        [HttpPost]
        public IHttpActionResult ListProvider(RentoRequest request)
        {
            Logger.Debug("User - ListProvider", request);
            var response = new RentoResponse<List<SelectModel>>(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {
                response.Data = await Database.UserManager.ListProvider(UserSession.Id);
            }));
        }

        [HttpPost]
        public IHttpActionResult Create(RentoRequest<UserLogin> request)
        {
            Logger.Debug("User - Create", request);
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {
                if (!ValidateRequirdField(request.Data.Username, request.Data.Password))
                { response.ErrorCode = ErrorCode.RequirdField; return; }

                if (!request.Data.Password.CheckRegexValid(Constant.PASSWORD_EXPRESION))
                { response.ErrorCode = ErrorCode.InvalidPasswordFormat; return; }

                var userId = await Database.UserManager.Create(request.Data);
                if (userId == 0)
                    response.ErrorCode = ErrorCode.UsernameAlreadyExists;
            }));
        }

        [HttpPost]
        public IHttpActionResult UpdateFlag(RentoRequest<UpdateUserFlagRequest> request)
        {
            Logger.Debug("User - UpdateFlag", request);
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Operation, async () =>
            {
                if (!ValidateRequirdField(request.Data.UserId))
                {
                    response.ErrorCode = ErrorCode.RequirdField;
                    return;
                }
                var userType = await UserManager.UpdateFlag(request.Data);
                RentoCache.Set(request.Data.UserId.ToString(), (byte)userType);
            }));
        }


        #endregion

        #region UserInfo

        [HttpPost]
        public IHttpActionResult GetUserInfo(RentoRequest<int> request)
        {
            Logger.Debug("User - GetUserInfo", request);
            var response = new RentoResponse<User>(request);
            return Ok(TryCatch(request, response, ValidateType.Active, async () =>
            {
                if (request.Data == 0)
                    request.Data = UserSession.Id;
                else
                {
                    if ((UserType)UserSession.Type != UserType.Operation && (UserType)UserSession.Type != UserType.Administrator)
                    { response.ErrorCode = ErrorCode.AccessDenai; return; }
                }

                response.Data = await AccountManager.Select(request.Data);
            }));
        }

        [HttpPost]
        public IHttpActionResult UpdateUserInfo(RentoRequest<User> request)
        {
            var response = new RentoResponse(request);
            return Ok(TryCatch(request, response, ValidateType.Pending, async () =>
            {
                var licence = request.Data.Licence;
                var logo = request.Data.Logo;
                var refarmeCard = request.Data.RefarmeCard;
                if (request.Data.Licence != null)
                {
                    if (!Rento.Helper.FileExtension.ScanImage(request.Data.Licence.Content))
                    { response.ErrorCode = ErrorCode.InvalidImageFormat; return; }
                    request.Data.Licence = new RentoImage();
                }
                if (request.Data.Logo != null)
                {
                    if (!Rento.Helper.FileExtension.ScanImage(request.Data.Logo.Content))
                    { response.ErrorCode = ErrorCode.InvalidImageFormat; return; }
                    request.Data.Logo = new RentoImage();
                }
                if (request.Data.RefarmeCard != null)
                {
                    if (!Rento.Helper.FileExtension.ScanImage(request.Data.RefarmeCard.Content))
                    { response.ErrorCode = ErrorCode.InvalidImageFormat; return; }
                    request.Data.RefarmeCard = new RentoImage();
                }

                Logger.Debug("User - UpdateUserInfo", request);

                request.Data.Licence = licence;
                request.Data.Logo = logo;
                request.Data.RefarmeCard = refarmeCard;

                if (!ValidateRequirdField(request.Data.CityId, request.Data.CountryId)
                    ||
                    ((UserType)UserSession.Type == UserType.Pending && (request.Data.Licence == null || request.Data.RefarmeCard == null))
                    ||
                    !ValidateRequirdField(request.Data.Mobile, request.Data.Name)
                    ||
                    !ValidateRequirdField(request.Data.Latitude, request.Data.Latitude)
                    )
                { response.ErrorCode = ErrorCode.RequirdField; return; }
                request.Data.Mobile = SMSMessage.CheckMobileNumber(request.Data.Mobile);
                request.Data.Id = UserSession.Id;
                await Database.AccountManager.Update(request.Data);
                if ((UserType)UserSession.Type == UserType.Pending)
                    UserSession.Type = (int)UserType.Active;
                RentoCache.Set(request.Data.Id.ToString(), (byte)UserSession.Type);
            }));
        }

        #endregion
    }
}
