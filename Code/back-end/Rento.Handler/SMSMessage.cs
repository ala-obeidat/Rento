using System;
using System.IO;
using System.Net;

namespace Rento.Helper
{
    public class SMSMessage
    {
        public static string CheckMobileNumber(string mobile)
        {
            var result = mobile;
            if (mobile.Length == 10)
                return result;
            if (mobile.Length > 10)
                mobile = mobile.Substring(mobile.Length - 9);
            if (mobile.Length == 9)
                result = $"0{mobile}";
            if (mobile.Length == 8)
                result = $"05{mobile}";
            return result;
        }
        public static bool Send(string mobile, string text)
        {
            try
            {
                
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.mobily.ws/api/msgSend.php");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                string postData = "mobile=966539447144&password=P@ssw0rd!@#&numbers=" + mobile + "&sender=E-Rent&msg=" + ConvertToUnicode(text) + "&applicationType=61";
                req.ContentLength = postData.Length;

                using (StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
                {
                    stOut.Write(postData);
                    stOut.Close();
                }
                // Do the request to get the response
                using (StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream()))
                    return stIn.ReadToEnd() == "1";
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                return false;
            }
        }

        private static string ConvertToUnicode(string val)
        {
            string msg2 = string.Empty;

            for (int i = 0; i < val.Length; i++)
            {
                msg2 += convertToUnicode(System.Convert.ToChar(val.Substring(i, 1)));
            }

            return msg2;
        }
        private static string convertToUnicode(char ch)
        {
            System.Text.UnicodeEncoding class1 = new System.Text.UnicodeEncoding();
            byte[] msg = class1.GetBytes(System.Convert.ToString(ch));

            return fourDigits(msg[1] + msg[0].ToString("X"));
        }

        private static string fourDigits(string val)
        {
            string result = string.Empty;

            switch (val.Length)
            {
                case 1: result = "000" + val; break;
                case 2: result = "00" + val; break;
                case 3: result = "0" + val; break;
                case 4: result = val; break;
            }

            return result;
        }

    }
}
