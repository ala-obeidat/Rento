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
    public class UserController : BaseController
    {
        #region Mobile Customer

        [HttpPost]
        public async Task<IHttpActionResult> ListMobileCustomer(RentoRequest<MobileCustomerRequest> request)
        {
            Logger.Debug("User - ListMobileCustomer", request);
            return Ok(await TryCatchResponse(request,  ValidateType.Operation, async (RentoResponse<List<MobileCustomerResponse>> response) =>
            {
                var tempResponse = await Database.AccountManager.ListMobileCustomer(request.Data, request.PageNumber, Constant.PAGE_SIZE);
                response.Data = tempResponse.Data;
                response.RowsCount = tempResponse.RowsCount;
            }));
        }

        #endregion

        #region User

        [HttpPost]
        public async Task<IHttpActionResult> List(RentoRequest request)
        {
            Logger.Debug("User - List", request);
            return Ok(await TryCatchResponse(request,  ValidateType.Active, async (RentoResponse<List<BaseNameEntity<UserType>>> response) =>
            {
                response.Data = await Database.AccountManager.List((UserType)UserSession.Type);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> ListProvider(RentoRequest request)
        {
            Logger.Debug("User - ListProvider", request);
            return Ok(await TryCatchResponse(request,  ValidateType.Operation, async (RentoResponse<List<SelectModel>> response) =>
            {
                response.Data = await Database.UserManager.ListProvider(UserSession.Id);
            }));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(RentoRequest<UserLogin> request)
        {
            Logger.Debug("User - Create", request);
            return Ok(await TryCatchResponseBase(request,  ValidateType.Operation, async (RentoResponse response) =>
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
        public async Task<IHttpActionResult> UpdateFlag(RentoRequest<UpdateUserFlagRequest> request)
        {
            Logger.Debug("User - UpdateFlag", request);
            return Ok(await TryCatchResponseBase(request,  ValidateType.Operation, async (RentoResponse response) =>
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
        public async Task<IHttpActionResult> GetUserInfo(RentoRequest<int> request)
        {
            Logger.Debug("User - GetUserInfo", request);
            return Ok(await TryCatchResponse(request,  ValidateType.Active, async (RentoResponse<User> response) =>
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
        public async Task<IHttpActionResult> UpdateUserInfo(RentoRequest<User> request)
        {
            return Ok(await TryCatchResponseBase(request,  ValidateType.Pending, async (RentoResponse response) =>
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
