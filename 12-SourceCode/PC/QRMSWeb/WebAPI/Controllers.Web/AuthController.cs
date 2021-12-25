using AuthenticationService.Managers;
using AuthenticationService.Models;
using BLL.FactoryBLL.Web.Users;
using BPL.Models.Web;
using HDLIB.Common;
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
                                    new Claim(ClaimTypes.NameIdentifier, account.Email),
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