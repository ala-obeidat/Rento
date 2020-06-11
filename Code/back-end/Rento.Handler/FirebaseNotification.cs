using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Rento.Helper
{
    public static class FirebaseNotification
    {
        const string _androidAuthorizationKey = "AAAAvvK7WJw:APA91bH-SwSWvrR25qSpJT9AmjNStPQSzG0C_yOUibjN-2HmL_Z_oVCKokX6yJ1nH1jbrre1KuufnJv9qtVQsE5nbmKsHtFcS6z9lNS2xAoT1LitOij0jI7L7_m3cnFff2YaYyLucvzv";
        const string _iosAuthorizationKey = "AAAA2jzqOiw:APA91bHesmHkgirN9GiObCwZWqWvSp3lED3IPPP8YCTDSEJHm8lBBdDwTTEahBIVEb_O4FPxj9aPn7CEqx-juFIrbdqOIeNTL5Kx-UGUBTGLFBLu4pJfhnenc4KSImsENg3RAH7Ck_Ig";

        public static void SendPushNotification(string title, string body, string notificationToken, bool isAndroid)
        {
            FirebaseRequest(GetMessage(title, body, notificationToken), isAndroid);
        }

        private static void FirebaseRequest(object message,bool isAndroid)
        {
            var json = JsonConvert.SerializeObject(message);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", isAndroid?_androidAuthorizationKey:_iosAuthorizationKey));
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
                dataStream.Write(byteArray, 0, byteArray.Length);
            tRequest.GetResponse().Dispose();
            tRequest = null;
        }

        private static object GetMessage(string title, string body, string notificationToken)
        {
           
            return new
            {
                to = notificationToken,
                notification = new
                {
                    title = title,
                    body = body,
                    sound = "Enabled"
                },
                data = new
                {
                    titleEn = title,
                    titleAr = title,
                    bodyEn = body,
                    bodyAr = body,
                }
            };
        }
    }
}
