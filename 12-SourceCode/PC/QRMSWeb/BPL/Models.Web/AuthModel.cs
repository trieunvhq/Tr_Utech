using BLL.Models.Web;
using System.Collections.Generic;

namespace BPL.Models.Web
{
    public class AuthModel
    {
        public UserModel User { get; set; }
        public string RefreshToken { get; set; }

    }
}
