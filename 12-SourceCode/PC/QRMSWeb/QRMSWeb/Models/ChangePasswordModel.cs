using System;

namespace QRMSWeb.Models
{
    public class ChangePasswordModel
    {
        public int ID { get; set; }
        public string CURRENT_PASSWORD { get; set; }
        public string NEW_PASSWORD { get; set; }
        public string NEW_CONFIRM_PASSWORD { get; set; }
    }
}