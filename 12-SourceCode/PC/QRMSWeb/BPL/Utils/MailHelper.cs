using BLL.Models.Web;
using System.Net;
using System.Net.Mail;

namespace BLL.Utils
{
    class MailHelper
    {
        public static void SendEmail(EmailModel emailModel)
        {
           
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(emailModel.FromEmail);
            message.To.Add(new MailAddress(emailModel.To));
            message.Subject = emailModel.Subject;
            message.IsBodyHtml = emailModel.IsBodyHtml;
            message.Body = emailModel.Body;
            smtp.Port = emailModel.Port;
            smtp.Host = emailModel.Host;
            smtp.EnableSsl = emailModel.EnableSsl;
            smtp.UseDefaultCredentials = emailModel.UseDefaultCredentials;
            smtp.Credentials = new NetworkCredential(emailModel.FromEmail, emailModel.FromPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
