using AuthenticationService.Managers;
using AuthenticationService.Models;
using BLL.FactoryBLL.Web.Users;
using BPL.Models.Web;
using HDLIB.Common;
using PISAS_API.Models.Web.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using Web_API.Attributes.Web;
using Web_API.Controllers.Web;

namespace WebAPI.Controllers.Web
{
    public class WebAuthController : BaseAPIController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api-wa/auth/login")]
        public BaseRespModel AccountLogin([FromBody] LoginDTO accountLoginDTO)
        {
            var _return = new BaseRespModel();
            try
            {
                var userBLL = new UserBLL();
                var account = userBLL.GetAccountByUserName(accountLoginDTO.USERNAME);
                if (account == null)
                {
                    _return.Message = "Tài khoản không tồn tại";
                    _return.RespondCode = ConstAPIResponseCode.NOT_FOUND;
                    _return.ErrorCode = ConstAPIErrorCode.VALIDATION;
                    return _return;
                }

                if (ConstRecordStatus.Locked.Equals(account.RecordStatus))
                {
                    _return.Message = "Tài khoản đã bị khóa";
                    _return.RespondCode = ConstAPIResponseCode.NOT_FOUND;
                    _return.ErrorCode = ConstAPIErrorCode.VALIDATION;
                    return _return;
                }

                var result = userBLL.AccountLogin(account, accountLoginDTO.PASSWORD, Request.GetClientIpAddress(), Request.Headers.UserAgent.ToString());
                if (result == 1)
                {
                    _return.Message = "Đăng nhập thành công";
                    _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                    _return.data = GenarateToken(account);
                }
                else
                {
                    _return.Message = "Đăng nhập thất bại";
                    _return.RespondCode = ConstAPIResponseCode.NOT_FOUND;
                    _return.ErrorCode ="LOGIN_FAILED";
                }

                return _return;
                    
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ConstAPIMessage.SYSTEM_ERROR;
                _return.RespondCode = ConstAPIResponseCode.SERVER_ERROR;
                _return.ErrorCode = ConstAPIErrorCode.SYSTEM_ERROR;
                return _return;
            }
        }



        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/auth/get-auth-of-login")]
        public BaseRespModel GetAccountRoles()
        {
            var _return = new BaseRespModel();
            try
            {
                int? userID = HelperFunction.GetUserId(User);
                    
                var userlogin = new UserBLL().GetAccountById(userID??0, false);
                if (userlogin != null)
                {
                    AuthModel _authModel = new AuthModel();
                    _authModel.User = userlogin;
                    _authModel.RefreshToken = GenarateToken(userlogin);
                    _return.data = _authModel;
                    _return.ErrorCode = "0";
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
        private string GenarateToken(UserModel account)
        {
            IAuthContainerModel model = new JWTContainerModel()
            {
                Claims = new Claim[]
                                {
                                    new Claim(ClaimTypes.Sid, account.ID.ToString()),
                                    new Claim(ClaimTypes.Name, account.Code),
                                    new Claim(ClaimTypes.NameIdentifier, account.Code),
                                }
            };
            var WebTimeOut = System.Configuration.ConfigurationSettings.AppSettings["WebTimeOut"]?.Trim();
            if (!string.IsNullOrEmpty(WebTimeOut))
            {
                try
                {
                    model.ExpireMinutes = Int32.Parse(WebTimeOut); // WebTimeOut
                }
                catch (Exception)
                {

                }
            }

            IAuthService authService = new JWTService(model.SecretKey);
            return authService.GenerateToken(model);
        }


    }
}