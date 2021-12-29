using System;
namespace QRMS.Models
{
    public class LoginInputModel
    {
        public string IpAddress { get; set; }
        public string DeviceName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
