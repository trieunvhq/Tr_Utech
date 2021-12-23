using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BPL;
using BPL.Acounts;
using HDLIB.Common;
using Newtonsoft.Json.Linq;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AcountControllers : ApiController
    {
        //Check đăng nhập
        [HttpPost]
        [Route("api-cus/account/login")]
        public BaseModel CheckAccount([FromBody] acmodel jObject)
        {
            var _return = new BaseModel();
            try
            {
                string user = jObject.user;
                string pass = jObject.pass;

                var result = AcountLogin.checkLogin(user, pass);

                if (result != null)
                {
                    if (result.isTrue)
                    {
                        _return.RespondCode = "1";
                        _return.Message = "Đăng nhập thành công";
                        _return.ErrorCode = "";
                    }
                }
                else
                {
                    _return.RespondCode = "0";
                    _return.ErrorCode = "";
                    _return.Message = "";
                }    

                _return.data = result;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ex.ToString();
            }
            return _return;
        }
    }

    public class acmodel
    {
        public string user { get; set; }
        public string pass { get; set; }
    }
}

