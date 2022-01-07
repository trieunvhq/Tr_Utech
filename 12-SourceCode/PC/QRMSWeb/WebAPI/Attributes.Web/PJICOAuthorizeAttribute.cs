using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using AuthenticationService.Managers;
using AuthenticationService.Models;
using BLL.FactoryBLL.Web.Users;
using HDLIB.Common;
using WebAPI.Controllers.Web;

namespace Web_API.Attributes.Web
{
    public class PJICOAuthorizeAttribute : AuthorizeAttribute
    {
        public string[] codes { get; set; }

        public PJICOAuthorizeAttribute(params string[] codes)
        {
            this.codes = codes;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Constant.IsAuthentication && RequireAuthorization(actionContext))
            {
                
                if (Authorize(actionContext))
                {
                    return;
                }

                HandleUnauthorizedRequest(actionContext);
            }

            return;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var baseRespModel = new BaseRespModel(ConstAPIMessage.UNAUTHORIZED, ConstAPIResponseCode.UNAUTHORIZED, ConstAPIErrorCode.UNAUTHORIZED);
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, baseRespModel);
        }

        private static bool RequireAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);
            return actionContext.ActionDescriptor.GetCustomAttributes<AuthRequireAttribute>().Any() || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AuthRequireAttribute>().Any();
        }

        private bool Authorize(HttpActionContext actionContext)
        {
            try
            {
                var authorization = actionContext.Request.Headers.Authorization;
                if (authorization == null || authorization.Parameter == null || authorization.Scheme != "Bearer") return false;

                IAuthContainerModel model = new JWTContainerModel();
                IAuthService authService = new JWTService(model.SecretKey);

                bool isAuth = authService.IsTokenValid(authorization.Parameter);

                if (!isAuth) return false;

                List<Claim> claims = authService.GetTokenClaims(authorization.Parameter).ToList();

                var userID = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Sid)).Value;
                //using (var db = new DAL.QRMSEntities())
                {
                    var account = new UserBLL().GetAccountById(Convert.ToInt32(userID), false);
                    if (account == null) return false;

                 
                        claims.Add(new Claim(ClaimTypes.Sid, account.ID.ToString()));
                        claims.Add(new Claim(ClaimTypes.Name, account.Code));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, account.Code));
                                        

                    //new Claim(ClaimTypes.NameIdentifier, account.ACCOUNT_TYPE_ID.ToString());

                    var identity = new ClaimsIdentity(claims, "Bearer");
                    var principal = new ClaimsPrincipal(new[] { identity });
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return false;
            }
        }
    }
}
