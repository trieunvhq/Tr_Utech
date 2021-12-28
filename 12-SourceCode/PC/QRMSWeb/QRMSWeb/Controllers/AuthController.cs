using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers.Web;

namespace QRMSWeb.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        [Route("api-wa/auth/login1")]
        public BaseRespModel AccountLogin()
        {
            var _return = new BaseRespModel();
               _return.Message = "ok";
                _return.RespondCode = "OK";
                _return.ErrorCode = "OK";
                return _return;
            
        }


    }
}
