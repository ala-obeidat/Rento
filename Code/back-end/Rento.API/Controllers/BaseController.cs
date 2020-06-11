using Rento.Entity;
using Rento.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace Rento.API.Controllers
{
    public class BaseController : ApiController
    {
        #region Proparity

        protected UserSession UserSession { set; get; }

        #endregion

        #region JWT

        protected string GenerateToken(string userName, int userId, int userType)
        {
            RentoCache.Set(userId.ToString(), (byte)userType);
            return $"{userName},{userId},{DateTime.Now.AddHours(Constant.ADDITINAL_HOUT_TIME).Ticks}".ToEncreptedString();
        }
        protected string GenerateToken(UserSession userSession)
        {
            return GenerateToken(userSession.Username, userSession.Id, userSession.Type);
        }
        private async Task<UserSession> CheckToken(string token)
        {
            UserSession result = null;
            try
            {
                if (token.Contains('-'))
                {
                    result = RentoCache.Get<UserSession>(token);
                    if (result == null && !string.IsNullOrEmpty(token))
                    {
                        result = await Database.AccountManager.Login(token);
                        if (UserSession != null)
                        {
                            RentoCache.Set(token, UserSession);
                        }
                    }
                }
                else
                {
                    string[] tokenInfo = token.ToFlatString().Split(',');
                    byte userType = RentoCache.Get<byte>(tokenInfo[1]);
                    int userId = Convert.ToInt32(tokenInfo[1]);
                    if (userType == default(byte))
                    {
                        userType = await Database.AccountManager.GetUserType(userId);
                        RentoCache.Set(userId.ToString(), (byte)userType);
                        if (userType == 0)
                        {
                            return null;
                        }
                    }
                    result = new UserSession()
                    {
                        Id = userId,
                        Type = userType,
                        Username = tokenInfo[0]
                    };
                }
            }
            catch
            {
                return null;
            }
            return result;
        }

        #endregion

        #region Helper Methods

        protected string ReadFileFromAppData(string fileName)
        {
            using (var streamReader = new StreamReader(HostingEnvironment.MapPath("~/App_Data/" + fileName + ".txt"), Encoding.UTF8, true))
                return streamReader.ReadToEnd();
        }

        protected void RunActionAsync(Action action)
        {
            Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                }
            });

        }

        protected async Task<RentoResponse> TryCatch(RentoRequest request, RentoResponse  response, ValidateType validateType, Func<Task> process)
        {
            try
            {
                bool valid = true;
                switch (validateType)
                {
                    case ValidateType.Active:
                        var validate = await ValidateRequst(request);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Pending:
                        validate = await ValidateRequst(request, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Operation:
                        valid = await ValidateRequstOperation(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Admin:
                        valid = await ValidateRequstAdmin(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Block:
                        validate = await ValidateRequst(request, true, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    default:
                        break;
                }
                if (valid)
                    await process();
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                string message = string.Empty;
                do
                {
                    message += exception.Message + "\t";
                    exception = ex.InnerException;
                } while (exception != null);
                response.DeveloperMessage = message;
                response.ErrorCode = ErrorCode.GeneralError;
                Logger.Exception(ex);
            }
            return response;
        }

        protected async Task<RentoResponse<T>> TryCatchResponse<T>(RentoRequest request,  ValidateType validateType, Func<RentoResponse<T>,Task> process)
        {
            var response = new RentoResponse<T>(request);
            try
            {
              
                bool valid = true;
                switch (validateType)
                {
                    case ValidateType.Active:
                        var validate = await ValidateRequst(request);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Pending:
                        validate = await ValidateRequst(request, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Operation:
                        valid = await ValidateRequstOperation(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Admin:
                        valid = await ValidateRequstAdmin(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Block:
                        validate = await ValidateRequst(request, true, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    default:
                        break;
                }
                if (valid)
                    await process(response);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                string message = string.Empty;
                do
                {
                    message += exception.Message + "\t";
                    exception = ex.InnerException;
                } while (exception != null);
                response.DeveloperMessage = message;
                response.ErrorCode = ErrorCode.GeneralError;
                Logger.Exception(ex);
            }
            return response;
        }
        protected async Task<RentoResponse> TryCatchResponseBase(RentoRequest request, ValidateType validateType, Func<RentoResponse, Task> process)
        {
            var response = new RentoResponse(request);
            try
            {

                bool valid = true;
                switch (validateType)
                {
                    case ValidateType.Active:
                        var validate = await ValidateRequst(request);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Pending:
                        validate = await ValidateRequst(request, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Operation:
                        valid = await ValidateRequstOperation(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Admin:
                        valid = await ValidateRequstAdmin(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Block:
                        validate = await ValidateRequst(request, true, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    default:
                        break;
                }
                if (valid)
                    await process(response);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                string message = string.Empty;
                do
                {
                    message += exception.Message + "\t";
                    exception = ex.InnerException;
                } while (exception != null);
                response.DeveloperMessage = message;
                response.ErrorCode = ErrorCode.GeneralError;
                Logger.Exception(ex);
            }
            return response;
        }

        protected async Task<RentoResponse<T>> TryCatchSync<T>(RentoRequest request, ValidateType validateType, Action<RentoResponse<T>> process)
        {
            var response = new RentoResponse<T>(request);
            try
            {
                bool valid = true;
                switch (validateType)
                {
                    case ValidateType.Active:
                        var validate = await ValidateRequst(request);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Pending:
                        validate = await ValidateRequst(request, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Operation:
                        valid = await ValidateRequstOperation(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Admin:
                        valid = await ValidateRequstAdmin(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Block:
                        validate = await ValidateRequst(request, true, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    default:
                        break;
                }
                if (valid)
                    process(response);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                string message = string.Empty;
                do
                {
                    message += exception.Message + "\t";
                    exception = ex.InnerException;
                } while (exception != null);
                response.DeveloperMessage = message;
                response.ErrorCode = ErrorCode.GeneralError;
                Logger.Exception(ex);
            }
            return response;
        }

        protected async Task<RentoResponse> TryCatchSyncBase(RentoRequest request, ValidateType validateType, Action<RentoResponse> process)
        {
            var response = new RentoResponse(request);
            try
            {
                bool valid = true;
                switch (validateType)
                {
                    case ValidateType.Active:
                        var validate = await ValidateRequst(request);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Pending:
                        validate = await ValidateRequst(request, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    case ValidateType.Operation:
                        valid = await ValidateRequstOperation(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Admin:
                        valid = await ValidateRequstAdmin(request);
                        if (!valid)
                            response.ErrorCode = ErrorCode.AccessDenai;
                        break;
                    case ValidateType.Block:
                        validate = await ValidateRequst(request, true, true);
                        if (!validate.Key)
                        {
                            response.ErrorCode = validate.Value;
                            valid = false;
                        }
                        break;
                    default:
                        break;
                }
                if (valid)
                    process(response);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                string message = string.Empty;
                do
                {
                    message += exception.Message + "\t";
                    exception = ex.InnerException;
                } while (exception != null);
                response.DeveloperMessage = message;
                response.ErrorCode = ErrorCode.GeneralError;
                Logger.Exception(ex);
            }
            return response;
        }

        #endregion

        #region Validate Request


        protected async Task<KeyValuePair<bool, ErrorCode>> ValidateRequst(RentoRequest request, bool pending = false, bool blocked = false)
        {
            try
            {
                UserSession = await CheckToken(request.Token);
                if (UserSession != null)
                {
                    switch ((UserType)UserSession.Type)
                    {
                        case UserType.Pending:
                        case UserType.Customer_Pending:
                            return new KeyValuePair<bool, ErrorCode>(pending, ErrorCode.InActiveUser);
                        case UserType.Active:
                        case UserType.Customer_Active:
                        case UserType.Administrator:
                        case UserType.Operation:
                            return new KeyValuePair<bool, ErrorCode>(true, ErrorCode.Success);
                        case UserType.Customer_Blocked:
                        case UserType.Blocked:
                            return new KeyValuePair<bool, ErrorCode>(blocked, ErrorCode.BlockedUser);
                    }
                }
                if (UserSession == null)
                    return new KeyValuePair<bool, ErrorCode>(false, ErrorCode.UserDoesNotExist);
                return new KeyValuePair<bool, ErrorCode>(false, ErrorCode.AccessDenai);
            }
            catch (Exception e)
            {
                Logger.Exception(e, "ValidateRequst");
                return new KeyValuePair<bool, ErrorCode>(false, ErrorCode.GeneralError);
            }
        }

        private async Task<bool> ValidateRequstAdmin(RentoRequest request)
        {

            UserSession = await CheckToken(request.Token);
            return (UserSession != null && (UserType)UserSession.Type == UserType.Administrator);
        }

        protected async Task<bool> ValidateRequstOperation(RentoRequest request)
        {

            UserSession = await CheckToken(request.Token);
            return (UserSession != null && ((UserType)UserSession.Type == UserType.Administrator || (UserType)UserSession.Type == UserType.Operation));
        }


        #endregion

        #region Validate Requird Field
        protected bool ValidateRequirdField(params string[] fields)
        {
            foreach (var item in fields)
            {
                if (string.IsNullOrEmpty(item))
                    return false;
            }
            return true;
        }

        protected bool ValidateRequirdField(params decimal[] fields)
        {
            foreach (var item in fields)
            {
                if (item == 0)
                    return false;
            }
            return true;
        }
        protected bool ValidateRequirdField(params RentoImage[] fields)
        {
            foreach (var item in fields)
            {
                if (item == null || item.Content == null || item.Content.Length == 0)
                    return false;
            }
            return true;
        }
        protected bool ValidateRequirdField(params Base64RentoImage[] fields)
        {
            foreach (var item in fields)
            {
                if (item == null || string.IsNullOrEmpty(item.Content))
                    return false;
            }
            return true;
        }
        protected bool ValidateRequirdField(params int[] fields)
        {
            foreach (var item in fields)
            {
                if (item == 0)
                    return false;
            }
            return true;
        }
        protected bool ValidateRequirdField(params DateTime[] fields)
        {
            foreach (var item in fields)
            {
                if (item == DateTime.MinValue)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
