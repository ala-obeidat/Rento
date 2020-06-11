using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Rento.Helper
{
    public class Logger
    {
        #region Variables

        static string PATH = ConfigurationFileHelper.GetAppSettingString("LOG_PATH");
        static CultureInfo CULTURE = new CultureInfo("en-US");
        const int ADDITINAL_HOUT_TIME = 7;
        #endregion

        #region Methods

        static Logger()
        {
            try
            {
                var directory = Path.GetDirectoryName(PATH);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
            }
            catch { }
        }
        public static void Exception(Exception ex, string caption = "")
        {
            var message = string.Empty;
            var e = ex;
            do
            {
                message += "\t" + e.Message;
                e = e.InnerException;
            } while (e != null);
            var text = string.Format("{0} \t EXCEPTION \t {1} \t {2}",DateTime.Now.AddHours(ADDITINAL_HOUT_TIME).ToString("dd/MM/yyyy HH:mm:ss"), ex.Message, ex.StackTrace);
            WriteLog(text);
        }
        public static void Debug(string caption, object data)
        {
            var text = string.Format("{0} \t DEBUG \t Method: {1} \t Data=> {2}",DateTime.Now.AddHours(ADDITINAL_HOUT_TIME).ToString("dd/MM/yyyy HH:mm:ss"), caption, JsonConvert.SerializeObject(data));
            WriteLog(text);
        }
        public static void Debug(string date)
        {
            var text = string.Format("{0} \t DEBUG  \t Data=> {1}", DateTime.Now.AddHours(ADDITINAL_HOUT_TIME).ToString("dd/MM/yyyy HH:mm:ss"), date);
            WriteLog(text);
        }
        private static void WriteLog(string text)
        {
            try
            {
                using (var file = new System.IO.StreamWriter(string.Format("{0}_{1}.txt", PATH,DateTime.Now.AddHours(ADDITINAL_HOUT_TIME).ToString("yyyy-MM-dd", CULTURE)), true))
                    file.WriteLine(text);
            }
            catch { }
        }

        #endregion
    }
}
