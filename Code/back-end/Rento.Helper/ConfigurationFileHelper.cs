using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rento.Helper
{
    public class ConfigurationFileHelper
    {

        public static string GetConnectionSetting(string connectionStringName)
        {
            try
            {
                var connection = ConfigurationManager.ConnectionStrings[connectionStringName];
                if (connection == null)
                    return null;
                return connection.ConnectionString;
            }
            catch { return null; }
        }

        public static T GetAppSetting<T>(string settingName, T defaultValue)
        {
            try
            {
                object value = ConfigurationManager.AppSettings[settingName];
                if (value == null)
                    return defaultValue;
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch { return defaultValue; }
        }

        public static Guid GetAccountId()
        {
            try
            {
                string value = ConfigurationManager.AppSettings["ACCOUNT_ID"];
                if (string.IsNullOrEmpty(value))
                    return Guid.Empty;
                return new Guid(value);
            }
            catch { return Guid.Empty; }
        }

        public static string GetAppSettingString(string settingName)
        {
            try
            {
                string value = ConfigurationManager.AppSettings[settingName];
                if (string.IsNullOrEmpty(value))
                    return string.Empty;
                return value;
            }
            catch { return string.Empty; }
        }
    }
}
