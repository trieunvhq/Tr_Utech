using BPL.Acounts;
using BPL.Models;
using HDLIB.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AcountController : ApiController
    {
        //Check đăng nhập
        [HttpPost]
        [Route("api-cus/account/checklogin")]
        public BaseModel CheckAccount([FromBody] JObject jObject)
        {
            var _return = new BaseModel();
            try
            {
                string user = jObject["user"]?.ToString();
                string pass = jObject["pass"]?.ToString();

                var result = AcountLogin.checkLogin(user, pass);

                if (result != null)
                {
                    if (result.isTrue)
                    {
                        _return.RespondCode = "1";
                        _return.Message = "Đăng nhập thành công";
                        _return.ErrorCode = "0";
                    }
                    else
                    {
                        _return.RespondCode = "2";
                        _return.ErrorCode = "0";
                        _return.Message = "Sai tài khoản hoặc mật khẩu";
                    }    
                }
                else
                {
                    _return.RespondCode = "0";
                    _return.ErrorCode = "1";
                    _return.Message = "";
                }

                _return.data = result;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.RespondCode = "-1";
                _return.ErrorCode = "-1";
                _return.Message = ex.ToString();
            }
            return _return;
        }

        //Tạo tài khoản
        [HttpPost]
        [Route("api-cus/account/createaccount")]
        public BaseModel CreateAccount([FromBody] AcountModel ac)
        {
            var _return = new BaseModel();
            try
            {

                var result = AcountLogin.CreateAccount(ac);

                if (result == 1)
                {
                    _return.RespondCode = "1";
                    _return.Message = "Tạo tài khoản thành công";
                    _return.ErrorCode = "0";
                }
                else if (result == 2)
                {
                    _return.RespondCode = "2";
                    _return.ErrorCode = "0";
                    _return.Message = "Tài khoản đã tồn tại";
                }
                else if (result == 0)
                {
                    _return.RespondCode = "0";
                    _return.ErrorCode = "1";
                    _return.Message = "Tạo tài khoản không thành công. Vui lòng thử lại";
                }
                else
                {
                    _return.RespondCode = "-1";
                    _return.ErrorCode = "-1";
                    _return.Message = "Tạo tài khoản không thành công. Vui lòng thử lại";
                }    
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.RespondCode = "-99";
                _return.ErrorCode = "-99";
                _return.Message = ex.ToString();
            }
            return _return;
        }

        //Update
        [HttpPost]
        [Route("api-cus/account/updateaccount")]
        public BaseModel UpdateAccount([FromBody] AcountModel ac)
        {
            var _return = new BaseModel();
            try
            {

                var result = AcountLogin.UpdateAccount(ac);

                if (result == 1)
                {
                    _return.RespondCode = "1";
                    _return.Message = "Thay đổi thông tin tài khoản thành công";
                    _return.ErrorCode = "0";
                }
                else if (result == 0)
                {
                    _return.RespondCode = "0";
                    _return.ErrorCode = "1";
                    _return.Message = "Thay đổi thông tin tài khoản không thành công. Vui lòng thử lại";
                }
                else
                {
                    _return.RespondCode = "-1";
                    _return.ErrorCode = "-1";
                    _return.Message = "Thay đổi thông tin tài khoản không thành công. Vui lòng thử lại";
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.RespondCode = "-99";
                _return.ErrorCode = "-99";
                _return.Message = ex.ToString();
            }
            return _return;
        }

        //Update
        [HttpPost]
        [Route("api-cus/account/delaccount")]
        public BaseModel DeleteAccount(int id)
        {
            var _return = new BaseModel();
            try
            {

                var result = AcountLogin.UpdateAccount(ac);

                if (result == 1)
                {
                    _return.RespondCode = "1";
                    _return.Message = "Xoá tài khoản thành công";
                    _return.ErrorCode = "0";
                }
                else if (result == 0)
                {
                    _return.RespondCode = "0";
                    _return.ErrorCode = "1";
                    _return.Message = "Xoá tài khoản không thành công. Vui lòng thử lại";
                }
                else
                {
                    _return.RespondCode = "-1";
                    _return.ErrorCode = "-1";
                    _return.Message = "Xoá tài khoản không thành công. Vui lòng thử lại";
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.RespondCode = "-99";
                _return.ErrorCode = "-99";
                _return.Message = ex.ToString();
            }
            return _return;
        }

    }
}