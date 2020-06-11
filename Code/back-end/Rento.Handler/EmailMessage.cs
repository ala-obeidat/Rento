using System;
using System.Net.Mail;
using System.Text;

namespace Rento.Helper
{
    public class EmailMessage
    {
        
        public static string BuildBodyHTML(string subject, string email, string mobile,string name, string body)
        {
            var htmlBody = new StringBuilder();
            htmlBody.Append($"<h1> {subject}</h1>");
            htmlBody.Append("<h3> Sender info: </h3><ul>");
            htmlBody.Append($"<li><b>Name:</b> {name}</li>");
            htmlBody.Append($"<li><b>Mobile:</b> {mobile}</li>");
            htmlBody.Append($"<li><b>Email:</b> {email}</li></ul>");
            htmlBody.Append($"<p><b>Body:</b> {body}</p>");
            return htmlBody.ToString();
        }
        public static bool SendEmail(string from, string to, string subject, string body, Attachment attachment = null, AlternateView avHtml=null)
        {
            try
            {
                using (MailMessage mail = new MailMessage(from,to,subject,body))
                {
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    if (attachment != null)
                        mail.Attachments.Add(attachment);
                    if (avHtml != null)
                        mail.AlternateViews.Add(avHtml);
                    using (SmtpClient client = new SmtpClient("ashhalan.com", 25))
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("erent@ashhalan.com", "Ashhalan123?");
                        client.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Exception(e, "SendEmail");
                return false;
            }
        }
    }
}
