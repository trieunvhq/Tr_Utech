using BPL.Master;
using BPL.Models;
using BPL.Models.Users;
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
    public class AcountController : BaseAPIController
    {
        [HttpPost]
        [Route("api-ht/account/login")]
        public BaseModel Login([FromBody] LoginInputModel input)
        {
            var _return = new BaseModel();
            try
            {
                using (var db = new DAL.QRMSEntities())
                {
                    string err_code = "";
                    string err_msg = "";

                    var result = new AccountBPL(db).CheckAccount(input.UserName, input.Password, out err_code, out err_msg);

                    _return.ErrorCode = err_code;
                    _return.Message = err_msg;
                    _return.data = result;
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = ResponseErrorCode.Error.ToString();
                _return.Message = ex.Message;
            }
            return _return;
        }


        [HttpPost]
        [Route("api-ht/account/lock")]
        public BaseModel LockAccount([FromBody] JObject dataSent)
        {
            var _return = new BaseModel();
            try
            {
                string err_code = "";
                string err_msg = "";
                string emailOrMobile = dataSent?["UserName"]?.ToObject<string>();

                var accBPL = new AccountBPL(db);
                accBPL.LockAccount(emailOrMobile, out err_code, out err_msg);
                _return.ErrorCode = err_code;
                _return.Message = err_msg;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = "99";
                _return.Message = ex.Message;
            }

            return _return;
        }

        [HttpPost]
        [Route("api-ht/account/create")]
        public BaseModel CreateAccount([FromBody] RegisterInputModel input)
        {
            var _return = new BaseModel();
            try
            {
                string err_code = "";
                string err_msg = "";

                var accBPL = new AccountBPL(db);
                accBPL.AddAccount(input, out err_code, out err_msg);
                _return.ErrorCode = err_code;
                _return.Message = err_msg;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = "99";
                _return.Message = ex.Message;
            }

            return _return;
        }


        //public BaseModel CheckAccount([FromBody] JObject jObject)
        //{
        //    var _return = new BaseModel();
        //    try
        //    {
        //        string user = jObject["user"]?.ToString();
        //        string pass = jObject["pass"]?.ToString();

        //        var result = AcountLogin.checkLogin(user, pass);

        //        if (result != null)
        //        {
        //            if (result.isTrue)
        //            {
        //                _return.RespondCode = "1";
        //                _return.Message = "Đăng nhập thành công";
        //                _return.ErrorCode = "0";
        //            }
        //            else
        //            {
        //                _return.RespondCode = "2";
        //                _return.ErrorCode = "0";
        //                _return.Message = "Sai tài khoản hoặc mật khẩu";
        //            }    
        //        }
        //        else
        //        {
        //            _return.RespondCode = "0";
        //            _return.ErrorCode = "1";
        //            _return.Message = "";
        //        }

        //        _return.data = result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.LogError(ex);
        //        _return.RespondCode = "-1";
        //        _return.ErrorCode = "-1";
        //        _return.Message = ex.ToString();
        //    }
        //    return _return;
        //}

        ////Tạo tài khoản
        //[HttpPost]
        //[Route("api-cus/account/createaccount")]
        //public BaseModel CreateAccount([FromBody] AcountModel ac)
        //{
        //    var _return = new BaseModel();
        //    try
        //    {

        //        var result = AcountLogin.CreateAccount(ac);

        //        if (result == 1)
        //        {
        //            _return.RespondCode = "1";
        //            _return.Message = "Tạo tài khoản thành công";
        //            _return.ErrorCode = "0";
        //        }
        //        else if (result == 2)
        //        {
        //            _return.RespondCode = "2";
        //            _return.ErrorCode = "0";
        //            _return.Message = "Tài khoản đã tồn tại";
        //        }
        //        else if (result == 0)
        //        {
        //            _return.RespondCode = "0";
        //            _return.ErrorCode = "1";
        //            _return.Message = "Tạo tài khoản không thành công. Vui lòng thử lại";
        //        }
        //        else
        //        {
        //            _return.RespondCode = "-1";
        //            _return.ErrorCode = "-1";
        //            _return.Message = "Tạo tài khoản không thành công. Vui lòng thử lại";
        //        }    
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.LogError(ex);
        //        _return.RespondCode = "-99";
        //        _return.ErrorCode = "-99";
        //        _return.Message = ex.ToString();
        //    }
        //    return _return;
        //}

        ////Update
        //[HttpPost]
        //[Route("api-cus/account/updateaccount")]
        //public BaseModel UpdateAccount([FromBody] AcountModel x)
        //{
        //    var _return = new BaseModel();
        //    try
        //    {
        //        using(AcountLogin t = new AcountLogin())
        //        {

        //        }    
        //        var result = AcountLogin.UpdateAccount(x);

        //        if (result == 1)
        //        {
        //            _return.RespondCode = "1";
        //            _return.Message = "Thay đổi thông tin tài khoản thành công";
        //            _return.ErrorCode = "0";
        //        }
        //        else if (result == 0)
        //        {
        //            _return.RespondCode = "0";
        //            _return.ErrorCode = "1";
        //            _return.Message = "Thay đổi thông tin tài khoản không thành công. Vui lòng thử lại";
        //        }
        //        else
        //        {
        //            _return.RespondCode = "-1";
        //            _return.ErrorCode = "-1";
        //            _return.Message = "Thay đổi thông tin tài khoản không thành công. Vui lòng thử lại";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.LogError(ex);
        //        _return.RespondCode = "-99";
        //        _return.ErrorCode = "-99";
        //        _return.Message = ex.ToString();
        //    }
        //    return _return;
        //}

        ////Update
        //[HttpPost]
        //[Route("api-cus/account/delaccount")]
        //public BaseModel DeleteAccount(int id)
        //{
        //    var _return = new BaseModel();
        //    try
        //    {

        //        var result = AcountLogin.UpdateAccount(ac);

        //        if (result == 1)
        //        {
        //            _return.RespondCode = "1";
        //            _return.Message = "Xoá tài khoản thành công";
        //            _return.ErrorCode = "0";
        //        }
        //        else if (result == 0)
        //        {
        //            _return.RespondCode = "0";
        //            _return.ErrorCode = "1";
        //            _return.Message = "Xoá tài khoản không thành công. Vui lòng thử lại";
        //        }
        //        else
        //        {
        //            _return.RespondCode = "-1";
        //            _return.ErrorCode = "-1";
        //            _return.Message = "Xoá tài khoản không thành công. Vui lòng thử lại";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.LogError(ex);
        //        _return.RespondCode = "-99";
        //        _return.ErrorCode = "-99";
        //        _return.Message = ex.ToString();
        //    }
        //    return _return;
        //}

    }
}