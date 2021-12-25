using System;

namespace BLL.Models.Web
{
    public class EmailModel
    {
        public string FromEmail { get; set; }
        public string FromPassword { get; set; }
        public string To { get; set; }
        public string Bcc { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public bool IsBodyHtml { get; set; } = false;
        public string Body { get; set; }
        public int Port { get; set; } = 587;
        public string Host { get; set; } = "smtp.gmail.com"; //for gmail host 
        public bool EnableSsl { get; set; } = true;
        public bool UseDefaultCredentials { get; set; } = false;
    }
}
