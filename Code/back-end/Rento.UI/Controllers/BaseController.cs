using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Mvc.Filters;
using Rento.UI.Shared;
using System.IO;
using Rento.Entity;
using Rento.Helper;
using System.Threading.Tasks;
using Rento.UI.Models;
using Newtonsoft.Json;

namespace Rento.UI.Controllers
{
    [AuthorizationFilter]
    public class BaseController : Controller
    {

        protected string GetCarName(int type, int subType, int model)
        {
            if (FixData.IsRTL)
            {
                var itemType = FixData.SYSTEM_TYPE.First(t => t.Id == type).Name;
                var itemSubType = FixData.SYSTEM_SUB_TYPE.First(s => s.Id == subType).Name;
                return string.Format("{0} {1} - {2}", itemType, itemSubType, model);
            }
            else
            {
                var itemType = FixData.SYSTEM_TYPE.First(t => t.Id == type).NameEn;
                var itemSubType = FixData.SYSTEM_SUB_TYPE.First(s => s.Id == subType).NameEn;
                return string.Format("{0} {1} - {2}", itemType, itemSubType, model);
            }
        }

        protected JsonResult RentoJson(RentoResponse response, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            return RentoJsonObject(response, behavior);
        }
        protected JsonResult RentoJsonDelete(RentoResponse response, string actionUrl)
        {
            return RentoJsonObject(new { Message = response.Message, ErrorCode = response.ErrorCode, ActionUrl = actionUrl });
        }
        protected JsonResult RentoJsonError()
        {
            return RentoJsonObject(new { Message = Resources.Resource.GeneralError, ErrorCode = ErrorCode.GeneralError });
        }
        protected JsonResult RentoJsonObject(object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            return new LargeJsonResult()
            {
                Data = data,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue // Use this value to set your maximum size for all of your Requests
            };
        }

        protected string GetImagePath(params string[] paths)
        {
            return Server.MapPath(Path.Combine(System.Configuration.ConfigurationManager.AppSettings["IMAGE_PATH"], Path.Combine(paths))).Replace("\\en\\", "\\").Replace("\\ar\\", "\\");
        }
        protected RentoResponse TryCatch(RentoRequest request, Action process)
        {
            var response = new RentoResponse(request);
            try
            {
                process();
            }
            catch (Exception ex)
            {
                response.ErrorCode = ErrorCode.GeneralError;
                Logger.Exception(ex);
            }
            return response;
        }
        protected string GetName(List<Entity.BaseNameEntity> list, int id, bool isRtl = true)
        {
            return isRtl ? list.FirstOrDefault(l => l.Id == id).Name : list.FirstOrDefault(l => l.Id == id).NameEn;
        }
        protected string GetName(List<Entity.BaseNameEntity<int>> list, int id, int external, bool isRtl = true)
        {
            return isRtl ? list.FirstOrDefault(l => l.Id == id && l.ExternalData == external).Name : list.FirstOrDefault(l => l.Id == id && l.ExternalData == external).NameEn;
        }
        protected List<Entity.BaseNameJust> GetCarStatus(bool locolization)
        {
            var list = new List<Entity.BaseNameJust>();
            foreach (var item in Enum.GetValues(typeof(Entity.CarStatus)))
            {
                list.Add(new Entity.BaseNameJust()
                {
                    Id = Convert.ToInt32(item),
                    Name = locolization ? Resources.Resource.ResourceManager.GetString(item.ToString()) : item.ToString()
                });
            }
            return list;
        }
        protected async Task<Rento.Entity.RentoResponse<R>> CallApi<T, R>(string url, T data)
        {
            try
            {
                HttpResponseMessage response = await APIClient.Instance.PostAsJsonAsync(Rento.UI.Shared.FixData.BASE_URL + "api/" + url, GetRequest(url, data, true));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Rento.Entity.RentoResponse<R>>();

            }
            catch (Exception ex)
            {
                Helper.Logger.Exception(ex);
                return new RentoResponse<R>(null) { ErrorCode = ErrorCode.GeneralError };
            }
        }

        private Rento.Entity.RentoRequest<T> GetRequest<T>(string url, T data, bool token, GridModel model = null)
        {
            var pageSize = model == null ? 10 : ((model.iDisplayLength == 0) ? 10 : model.iDisplayLength);
            return new Rento.Entity.RentoRequest<T>()
            {
                Language = FixData.IsRTL ? (int)Language.Arabic : (int)Language.English,
                Token = token ? Session["Token"].ToString() : "",
                Data = data,
                PageNumber = model == null ? 1 : ((model.iDisplayStart == 0) ? 1 : model.iDisplayStart) / pageSize + 1
            };
        }

        protected async Task<RentoResponse<R>> CallApiEmpty<T, R>(string url, T data)
        {
            try
            {
                HttpResponseMessage response = await APIClient.Instance.PostAsJsonAsync(FixData.BASE_URL + "api/" + url, GetRequest(url, data, false));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Rento.Entity.RentoResponse<R>>();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return new RentoResponse<R>(null) { ErrorCode = ErrorCode.GeneralError };
            }
        }
        protected async Task<Rento.Entity.RentoResponse> CallApiTask<T>(string url, T data)
        {
            try
            {

                HttpResponseMessage response = await APIClient.Instance.PostAsJsonAsync(Rento.UI.Shared.FixData.BASE_URL + "api/" + url, GetRequest(url, data, true));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Rento.Entity.RentoResponse>();

            }
            catch (Exception ex)
            {
                Helper.Logger.Exception(ex);
                return new RentoResponse(null) { ErrorCode = ErrorCode.GeneralError };
            }
        }
        protected async System.Threading.Tasks.Task<Rento.Entity.RentoResponse<R>> CallApi<R>(string url)
        {
            try
            {

                HttpResponseMessage response = await APIClient.Instance.PostAsJsonAsync(Rento.UI.Shared.FixData.BASE_URL + "api/" + url, new Rento.Entity.RentoRequest() { Language = FixData.IsRTL ? (int)Language.Arabic : (int)Language.English, Token = Session["Token"].ToString() });
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Rento.Entity.RentoResponse<R>>();

            }
            catch (Exception ex)
            {
                Helper.Logger.Exception(ex);
                return new RentoResponse<R>(null) { ErrorCode = ErrorCode.GeneralError };
            }
        }
        protected async Task<Rento.Entity.RentoResponse<R>> CallApiGet<R>(string url)
        {
            try
            {

                HttpResponseMessage response = await APIClient.Instance.GetAsync(Rento.UI.Shared.FixData.BASE_URL + "api/" + url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Rento.Entity.RentoResponse<R>>();

            }
            catch (Exception ex)
            {
                Helper.Logger.Exception(ex);
                return new RentoResponse<R>(null) { ErrorCode = ErrorCode.GeneralError };
            }
        }

        protected async System.Threading.Tasks.Task<JsonResult> CallApiGrid<T, R>(string url, T data, GridModel param, Func<R, IEnumerable<object[]>> ResultListData)
        {
            var errorCode = ErrorCode.GeneralError;
            var errorMessage = Resources.Resource.GeneralError;
            try
            {

                HttpResponseMessage httpResponse = await APIClient.Instance.PostAsJsonAsync(Rento.UI.Shared.FixData.BASE_URL + "api/" + url, GetRequest(url, data, true, param));
                httpResponse.EnsureSuccessStatusCode();
                var response = await httpResponse.Content.ReadAsAsync<Rento.Entity.RentoResponse<R>>();
                if (response.ErrorCode == ErrorCode.Success)
                    return Json(new GridReponseSuccess()
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = response.RowsCount,
                        iTotalDisplayRecords = response.RowsCount,
                        aaData = ResultListData(response.Data),
                        ErrorCode = ErrorCode.Success
                    }, JsonRequestBehavior.AllowGet);
                errorCode = response.ErrorCode;
                errorMessage = response.Message;

            }
            catch (Exception ex)
            {
                Helper.Logger.Exception(ex);
            }
            return Json(new GridReponseFail()
            {
                ErrorCode = errorCode,
                sEcho = param.sEcho,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0,
                aaData = errorMessage
            }, JsonRequestBehavior.AllowGet);
        }
    }
}