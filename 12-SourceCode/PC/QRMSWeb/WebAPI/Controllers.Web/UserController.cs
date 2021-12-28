using BLL.Factory.Web.SaleOrder;
using BLL.FactoryBLL.Web.Users;
using BPL.Models.Web;
using HDLIB;
using HDLIB.Common;
using Newtonsoft.Json.Linq;
using PISAS_API.Models.Web.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Web_API.Attributes.Web;
using WebAPI.Models;

namespace WebAPI.Controllers.Web
{
    public class WebAPIUserController : BaseAPIController
    {
        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/account/create")]
        public BaseModel CreateAccount([FromBody] UserModel userModel)
        {
            var _return = new BaseModel();
            try
            {
                
                //check trùng username
                var user_exitst = new UserBLL().GetAccountByUserName(userModel.Code?.Trim());
                if (user_exitst != null)
                {
                    _return.Message = "Tài khoản đã tồn tại";
                    _return.RespondCode = APIResponseCode.VALIDATION;
                    _return.ErrorCode = APIErrorCode.VALIDATION;
                    return _return;
                }

                userModel.Code = userModel.Code.Trim();
                userModel.FullName = userModel.FullName?.Trim();
                userModel.Email = userModel.Email?.Trim();
                userModel.Phone = userModel.Phone?.Trim();
                userModel.Role = userModel.Role.Trim();
                string userName = HelperFunction.GetUserName(User);
                List<UserModel> lstUserModel = new List<UserModel>() { userModel };
                var result = new UserBLL().AddAccount(lstUserModel, userName);
                if (result > 0)
                {
                    _return.ErrorCode = "0";
                    _return.Message = "Thêm mới tài khoản thành công";
                } else
                {
                    _return.ErrorCode = "-1";
                    _return.Message = "Thêm mới tài khoản thất bại";
                }

                return _return;                    
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ex.ToString();
                _return.ErrorCode = "9999";
                return _return;
            }
        }

        [HttpGet]
        //[AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/account/all-account")]
        public BaseModel GetListAccount()
        {
            var _return = new BaseModel();

            try
            {
                string fullname = "";
                string username = "";
                int page = 1;
                int limit = 10;
                var queryString = Request.GetQueryNameValuePairs();

                foreach (var param in queryString)
                {
                    switch (param.Key)
                    {
                        case "username":
                            {
                                username = param.Value?.Trim();
                                break;
                            }
                        case "fullname":
                            {
                                fullname = param.Value.Trim();
                                break;
                            }
                        case "page":
                            {
                                page = int.Parse(param.Value ?? "1");
                                break;
                            }
                        case "limit":
                            {
                                limit = int.Parse(param.Value ?? "10");
                                break;
                            }
                    }
                }

                _return.RespondCode = "THÀNH CÔNG";
                _return.data = new UserBLL().GetAllUser(page, limit, username, fullname);
                    
                return _return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _return.Message = "Lỗi hệ thống";
                _return.RespondCode = "500";
                _return.ErrorCode = "SYSTEM_ERROR";
                return _return;
            }
        }
        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/account/update")]
        public BaseModel EditAccount(int id, [FromBody] UserModel userModel)
        {
            var _return = new BaseModel();
            try
            {
                string userName = HelperFunction.GetUserName(User);
                var accBLL = new UserBLL();
                var user_exitst = accBLL.GetAccountByUserName(userModel.Code?.Trim());
                if (user_exitst != null && user_exitst.ID != id)
                {
                    _return.Message = "Tài khoản đã tồn tại";
                    _return.RespondCode = APIResponseCode.VALIDATION;
                    _return.ErrorCode = APIErrorCode.VALIDATION;
                    return _return;
                }

                var currentAccount = accBLL.GetAccountById(id, true);
                if (currentAccount == null)
                {
                    _return.RespondCode = APIResponseCode.BAD_REQUEST;
                    _return.Message = "Không tim thấy tài khoản";
                    _return.ErrorCode = APIErrorCode.VALIDATION;
                    return _return;
                }
                //trim
                userModel.Code = userModel.Code.Trim();
                userModel.FullName = userModel.FullName?.Trim();
                userModel.Email = userModel.Email?.Trim();
                userModel.Phone = userModel.Phone?.Trim();
                userModel.Role = userModel.Role.Trim();
                userModel.ID = currentAccount.ID;
                var result = accBLL.EditAcc(userModel, userName);
                if (result == 1)
                {
                    _return.RespondCode = APIResponseCode.SUCCESS;
                    _return.Message = "Cập nhật tài khoản thành công";
                }
                else
                {
                    _return.RespondCode = APIResponseCode.BAD_REQUEST;
                    _return.Message = "Lỗi cập nhật tài khoản";
                }
                return _return;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ex.ToString();
                _return.ErrorCode = "9999";
                return _return;
            }
        }

        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/account/change-password")]
        public BaseModel ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var _return = new BaseModel();
            try
            {
                int? userID = HelperFunction.GetUserId(User);

                var accBLL = new UserBLL();
                var currentAccount = accBLL.GetAccountById(changePasswordDTO.ID, true);

                if (currentAccount == null)
                {
                    _return.RespondCode = APIResponseCode.BAD_REQUEST;
                    _return.Message = "Không tim thấy tài khoản";
                    _return.ErrorCode = APIErrorCode.VALIDATION;
                    return _return;
                }

                //changePasswordDTO
                var currentPass = Cipher.Encrypt(changePasswordDTO.CURRENT_PASSWORD, PasswordEncrypt.PRIVATE_KEY);
                if (currentAccount.Password != currentPass)
                {
                    _return.RespondCode = APIResponseCode.BAD_REQUEST;
                    _return.Message = "Mật khẩu hiện tại không đúng";
                    _return.ErrorCode = APIErrorCode.VALIDATION;
                    return _return;
                }
                var result = accBLL.UpdatePassword(currentAccount.Code, changePasswordDTO.NEW_PASSWORD, userID);
                if (result == 1)
                {
                    _return.RespondCode = APIResponseCode.SUCCESS;
                    _return.Message = "Cập nhật mật khẩu thành công";
                }
                else
                {
                    _return.RespondCode = APIResponseCode.BAD_REQUEST;
                    _return.ErrorCode = APIErrorCode.VALIDATION;
                    _return.Message = "Lỗi cập nhật mật khẩu";
                }

                return _return;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ex.ToString();
                _return.ErrorCode = "9999";
                return _return;
            }
        }

        [HttpPost]
        [Route("api_wa/account/reset-password")]
        public BaseModel RestPassword([FromBody] ResetPasswordDTO restPasswordDTO)
        {
            var _return = new BaseModel();
            try
            {
                    var accBLL = new UserBLL();
                    var currentAccount = accBLL.GetAccountByUserName(restPasswordDTO.Code);
                    if (currentAccount == null)
                    {
                        _return.RespondCode = APIResponseCode.BAD_REQUEST;
                        _return.Message = "Không tìm thấy tài khoản";
                        _return.ErrorCode = APIErrorCode.VALIDATION;
                        return _return;
                    }
                   
                    //reset password

                    var result = accBLL.ResetPassword(currentAccount.Code, currentAccount.Password);
                    if (result == 1)
                    {
                        _return.RespondCode = APIResponseCode.SUCCESS;
                        _return.Message = "Mật khẩu mới đã được gửi vào địa chỉ mail của bạn.";
                    }
                    else
                    {
                        _return.RespondCode = APIResponseCode.BAD_REQUEST;
                        _return.ErrorCode = APIErrorCode.VALIDATION;
                        _return.Message = "Lỗi reset mật khẩu";
                    }


                return _return;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ex.ToString();
                _return.ErrorCode = "9999";
                return _return;
            }
        }

        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/account/delete")]
        public BaseModel DeleteAccount([FromBody] JObject obj)
        {
            var _return = new BaseModel();
            try
            {
                int? userID = HelperFunction.GetUserId(User);

                //var UserId = Int32.Parse(JObjectUtils.GetValueByKeyJObject(obj, "USER_ID"));
                var Id = Int32.Parse(JObjectUtils.GetValueByKeyJObject(obj, "ID"));
                var accBLL = new UserBLL();
                var result = accBLL.DeleteAcc(Id, userID);
                if (result == 1)
                {
                    _return.RespondCode = "1";
                }
                else
                {
                    if (result == -2)
                    {
                        _return.ErrorCode = "-2";
                        _return.Message = "Đang sử dụng";
                    }

                    else
                    {
                        _return.ErrorCode = "-1";
                        _return.Message = "Xoá thất bại";
                    }
                }

                return _return;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = "9999";
                _return.Message = ex.ToString();
                return _return;
            }
        }

        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/account/lock")]
        public BaseModel LockAccount([FromBody] JObject obj)
        {
            var _return = new BaseModel();
            try
            {
                string userName = HelperFunction.GetUserName(User);

                //var UserId = Int32.Parse(JObjectUtils.GetValueByKeyJObject(obj, "USER_ID"));
                var Id = Int32.Parse(JObjectUtils.GetValueByKeyJObject(obj, "ID"));
                var accBLL = new UserBLL();
                var result = accBLL.LockAcc(Id, userName);
                if (result == 1)
                {
                    _return.RespondCode = "1";
                }
                else
                {
                    _return.ErrorCode = "-1";
                }

                return _return;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = "9999";
                _return.Message = ex.ToString();
                return _return;
            }
        }
        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/account/unlock")]
        public BaseModel UnlockAccount([FromBody] JObject obj)
        {
            var _return = new BaseModel();
            try
            {
                string userName = HelperFunction.GetUserName(User);

                //var UserId = Int32.Parse(JObjectUtils.GetValueByKeyJObject(obj, "USER_ID"));
                var Id = Int32.Parse(JObjectUtils.GetValueByKeyJObject(obj, "ID"));
                var accBLL = new UserBLL();
                var result = accBLL.UnlockAcc(Id, userName);
                if (result == 1)
                {
                    _return.RespondCode = "1";
                }
                else
                {
                    _return.ErrorCode = "-1";
                }
                return _return;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = "9999";
                _return.Message = ex.ToString();
                return _return;
            }
        }

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/account/lock")]
        public BaseModel LockAccount(string user = "")
        {
            var _return = new BaseModel();
            try
            {
                int? userID = HelperFunction.GetUserId(User);

                var result = new UserBLL().LockAccount(user, userID);
                if (result == 0)
                {
                    _return.RespondCode = "200";
                    _return.data = result;
                }
                else
                {
                    _return.Message = "Lock fail";
                    _return.ErrorCode = "-1";
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
            }

            return _return;
        }

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/account/changepassword")]
        public BaseModel UpdatePassword(string user = "", string newPass = "")
        {
            var _return = new BaseModel();
            try
            {
                int? userID = HelperFunction.GetUserId(User);

                var result = new UserBLL().UpdatePassword(user, newPass, userID);
                if (result == 0)
                {
                    _return.RespondCode = "200";
                    _return.data = result;
                }
                else
                {
                    _return.Message = "Update fail";
                    _return.ErrorCode = "-1";
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
            }

            return _return;
        }

        

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/account/{accountID}")]
        public BaseModel GetAccountById(int accountID)
        {
            var _return = new BaseModel();
            try
            {
                var result = new UserBLL().GetAccountById(accountID, true);
                if (result != null)
                {
                    _return.ErrorCode = "0";
                    _return.data = result;
                }
                else
                {
                    _return.Message = "Không thể lấy thông tin account";
                    _return.ErrorCode = "-1";
                }

                return _return;
                
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ex.ToString();
                return _return;
            }
        }

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/account/my-profile")]
        public BaseModel GetMyProfile()
        {
            var _return = new BaseModel();
            try
            {
                int? userID = HelperFunction.GetUserId(User);

                var account = userID != null ? new UserBLL().GetAccountById(userID??0, true) : null;
                if (account == null)
                {
                    _return.Message = "Không tìm thấy tài khoản";
                    _return.RespondCode = APIResponseCode.NOT_FOUND;
                    _return.ErrorCode = "ACCOUNT_NOT_FOUND";
                    return _return;
                }

                _return.RespondCode = APIResponseCode.SUCCESS;
                _return.data = account;
                return _return;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = APIMessage.SYSTEM_ERROR;
                _return.RespondCode = APIResponseCode.SERVER_ERROR;
                _return.ErrorCode = APIErrorCode.SYSTEM_ERROR;
                return _return;
            }
        }
    }
}