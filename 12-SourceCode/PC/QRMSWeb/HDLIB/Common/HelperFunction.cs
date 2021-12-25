using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace HDLIB.Common
{
    public static class HelperFunction
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        public static string GetGenderNameById(int id)
        {
            switch(id)
            {
                case 1: return "Nam";
                case 2: return "Nữ";
                default: return null;
            }
        }

        public static Nullable<int> GetUserId(IPrincipal User)
        {
            try
            {
                List<Claim> claims = ((ClaimsIdentity)User.Identity).Claims.ToList();
                
                return Convert.ToInt32(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Sid)).Value);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static string GetUserName(IPrincipal User)
        {
            try
            {
                List<Claim> claims = ((ClaimsIdentity)User.Identity).Claims.ToList();

                return claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name))?.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            return null;
        }
    }
}
