using Rento.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rento.Entity;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Rento.UI.Shared
{
    public class FixData
    {
        public static readonly JavaScriptSerializer _rentoSerializer = new JavaScriptSerializer();
        public static bool IsRTL
        {
            get
            {
                return HttpContext.Current.Session["M_LANGUAGE"].Equals("ar-SA");
            }
        }
        public static bool IsLogin
        {
            get
            {
                return HttpContext.Current.Session["Token"] != null;
            }

        }
        public static UserType UserType
        {
            get
            {
                if (HttpContext.Current.Session["USER_TYPE"] == null)
                    return UserType.Blocked;
                return (UserType)HttpContext.Current.Session["USER_TYPE"];
            }
        }
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["USERNAME"] == null)
                    return string.Empty;
                return (string)HttpContext.Current.Session["USERNAME"];
            }
        }
        public static bool Load { get; set; }

        public static string BASE_URL = System.Configuration.ConfigurationManager.AppSettings["BASE_URL"];

        private volatile static List<BaseNameEntity> _systemTypes;
        private static object _lockedObject = new object();
        public static List<BaseNameEntity> SYSTEM_TYPE
        {
            get
            {
                if (_systemTypes == null)
                    lock (_lockedObject)
                        if (_systemTypes == null)
                            _systemTypes = (List<BaseNameEntity>)HttpContext.Current.Session["SYSTEM_TYPE"];
                return _systemTypes;
            }
            set
            {
                _systemTypes = null;
                HttpContext.Current.Session["SYSTEM_TYPE"] = value;
            }

        }


        private volatile static List<BaseNameEntity<int>> _systemSubTypes;
        private static object _lockedSubObject = new object();
        public static List<BaseNameEntity<int>> SYSTEM_SUB_TYPE
        {
            get
            {
                if (_systemSubTypes == null)
                    lock (_lockedSubObject)
                        if (_systemSubTypes == null)
                            _systemSubTypes = (List<BaseNameEntity<int>>)HttpContext.Current.Session["SYSTEM_SUB_TYPE"];
                return _systemSubTypes;
            }
            set
            {
                _systemSubTypes = null;
                HttpContext.Current.Session["SYSTEM_SUB_TYPE"] = value;
            }
        }


        private volatile static List<BaseNameEntity> _systemCountry;
        private static object _lockedCountryObject = new object();
        public static List<BaseNameEntity> SYSTEM_COUNTRY
        {
            get
            {
                if (_systemCountry == null)
                    lock (_lockedCountryObject)
                        if (_systemCountry == null)
                            _systemCountry = RentoCache.Get<List<BaseNameEntity>>("SYSTEM_COUNTRY");
                return _systemCountry;
            }
        }

        private volatile static List<BaseNameEntity<int>> _citys;
        private static object _lockedCityObject = new object();
        public static List<BaseNameEntity<int>> SYSTEM_CITY
        {
            get
            {
                if (_citys == null)
                    lock (_lockedCityObject)
                        if (_citys == null)
                            _citys = RentoCache.Get<List<BaseNameEntity<int>>>("SYSTEM_CITY");
                return _citys;
            }
        }

        public static async Task InitilazeRentoConstantData()
        {
            var typesResponse = await CallApi<List<BaseNameEntity>>("LookUp/List", "Type");
            if (typesResponse.ErrorCode == ErrorCode.Success && typesResponse.Data != null)
                HttpContext.Current.Session["SYSTEM_TYPE"] = typesResponse.Data;

            var subTypesResponse = await CallApi<List<BaseNameEntity<int>>>("LookUp/ListExternal", "SubType");
            if (subTypesResponse.ErrorCode == ErrorCode.Success && subTypesResponse.Data != null)
                HttpContext.Current.Session["SYSTEM_SUB_TYPE"] = subTypesResponse.Data;


            var countryResponse = await CallApi<List<BaseNameEntity>>("LookUp/List", "Country");
            if (countryResponse.ErrorCode == ErrorCode.Success && countryResponse.Data != null)
                RentoCache.Set("SYSTEM_COUNTRY", countryResponse.Data);

            var cityResponse = await CallApi<List<BaseNameEntity<int>>>("LookUp/ListExternal", "City");
            if (cityResponse.ErrorCode == ErrorCode.Success && cityResponse.Data != null)
                RentoCache.Set("SYSTEM_CITY", cityResponse.Data);
        }
        private static async System.Threading.Tasks.Task<Rento.Entity.RentoResponse<R>> CallApi<R>(string url, string data)
        {
            try
            {

                HttpResponseMessage response = await APIClient.Instance.PostAsJsonAsync(BASE_URL + "api/" + url,
                    new Rento.Entity.RentoRequest<string>() { Language = FixData.IsRTL ? (int)Language.Arabic : (int)Language.English,  Data = data, Token = HttpContext.Current.Session["Token"].ToString() });
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Rento.Entity.RentoResponse<R>>();

            }
            catch (Exception ex)
            {
                Helper.Logger.Exception(ex);
                throw;
            }

        }

        private static async System.Threading.Tasks.Task<Rento.Entity.RentoResponse<R>> CallApiEmpty<R>(string url)
        {
            try
            {

                HttpResponseMessage response = await APIClient.Instance.PostAsJsonAsync(BASE_URL + "api/" + url,
                    new Rento.Entity.RentoRequest() { Language = FixData.IsRTL ? (int)Language.Arabic : (int)Language.English,  Token = HttpContext.Current.Session["Token"].ToString() });
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Rento.Entity.RentoResponse<R>>();

            }
            catch (Exception ex)
            {
                Helper.Logger.Exception(ex);
                throw;
            }

        }

    }
}