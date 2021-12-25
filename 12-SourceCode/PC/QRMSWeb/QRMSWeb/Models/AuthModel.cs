using System;
using System.Collections.Generic;

namespace QRMSWeb.Models
{
    public class AuthModel
    {
        public UserModel User { get; set; }
        public string RefreshToken { get; set; }
    }
}