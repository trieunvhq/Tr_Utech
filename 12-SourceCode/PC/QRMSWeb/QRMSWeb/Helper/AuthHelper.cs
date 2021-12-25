using QRMSWeb.Models;
using QRMSWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace QRMSWeb.Helper
{
    public class AuthHelper
    {
        static readonly HttpClient _httpClient = new HttpClient();
        private static bool isLoadingUser { get; set; } = false;
        public static bool IsLogined { get; set; } = false;
        private static UserModel User { get; set; } = null;
        public static string RefreshToken { get; set; } = "";


        public static bool IsManager()       {
            if (AuthHelper.User != null)
            {
               /*
                foreach(var role in FunctionActionRoles)
                {
                    if ("Admin".Equals(role.ROLE_CODE))
                    {
                        return true;
                    }
                }*/
                //account đặc biệt
                
                return (AuthHelper.User.INSURANCE_AGENT_ID == null && (AuthHelper.User.DIVISION_ID == null || AuthHelper.User.DIVISION_ID <= 0))
                        || "TCT".Equals(AuthHelper.User.INSURANCE_AGENT_CODE);
                
            }
            return false;
        }
        public static bool IsAdmin()
        {
            if (AuthHelper.User != null)
            {
                /*
                 foreach(var role in FunctionActionRoles)
                 {
                     if ("Admin".Equals(role.ROLE_CODE))
                     {
                         return true;
                     }
                 }*/
                //account đặc biệt
                if ("Admin".Equals(AuthHelper.User.USERNAME)) return true;
                /*
                return (AuthHelper.User.INSURANCE_AGENT_ID == null && (AuthHelper.User.DIVISION_ID == null || AuthHelper.User.DIVISION_ID <= 0))
                        || "TCT".Equals(AuthHelper.User.INSURANCE_AGENT_CODE);
                */
            }
            return false;
        }
        public static bool IsCusAppDepartment()
        {
            if (AuthHelper.User != null)
            {   //account thuộc phòng đặc biệt
                return "TTU".Equals(User.DEPARTMENT_CODE);
            }
            return false;
        }
        public static async Task<UserModel?> GetUser()
        {
            if (AuthHelper.User == null)
            {
                await AuthHelper.GetCurrentUserInf();
            }
            return AuthHelper.User;
        }
        public static void SetUser(UserModel UserModel)
        {
            AuthHelper.User = UserModel;
        }
        public static async Task<int?> UserLoginDivisionId()
        {
            if (AuthHelper.User == null)
            {
                await AuthHelper.GetCurrentUserInf();
            }
            if (AuthHelper.User != null && !AuthHelper.IsManager())
            {
                return (AuthHelper.User.DIVISION_ID == null || AuthHelper.User.DIVISION_ID <= 0) ? AuthHelper.User.INSURANCE_AGENT_ID : AuthHelper.User.DIVISION_ID;

            }
            return null;
        }
        public static async Task<int?> GetDivisionIdOfUserNotAdmin()
        {
            if (AuthHelper.User == null)
            {
                await AuthHelper.GetCurrentUserInf();
            }
            if (!AuthHelper.IsAdmin())
            {
                return await AuthHelper.UserLoginDivisionId();
            }
            return null;
        }

        public static bool checkPermisionOnUrl(string functionName, string actionName)
        {
            /*
            if (string.IsNullOrEmpty(functionName?.Trim()) && string.IsNullOrEmpty(actionName?.Trim())) return true;
            if (string.IsNullOrEmpty(functionName?.Trim()) && !string.IsNullOrEmpty(actionName?.Trim())) return false;
            if (!string.IsNullOrEmpty(functionName?.Trim()) && string.IsNullOrEmpty(actionName?.Trim())) return false;

            return checkPermision(getGetFunctionCodeByFunctionName(functionName), getGetActionCodeByActionName(actionName));
            */
            return true;
        }

        public static string GetFunctionCodeByFunctionName(string functionName)
        {
            if (MappFunctionNameCode() != null && MappFunctionNameCode().ContainsKey(functionName))
            {
                return MappFunctionNameCode().GetValueOrDefault(functionName);
            }
            return string.Empty;
        }
        public static string GetActionCodeByActionName(string actionName)
        {
            if (MappActionNameCode() != null && MappActionNameCode().ContainsKey(actionName))
            {
                return MappActionNameCode().GetValueOrDefault(actionName);
            }
            return string.Empty;
        }
        public static Dictionary<string, string> MappFunctionNameCode()
        {
            Dictionary<string, string> mappFunctionNameCode = new Dictionary<string, string>();
            mappFunctionNameCode.Add("functionName", "functionCode");
            return mappFunctionNameCode;
        }
        public static Dictionary<string, string> MappActionNameCode()
        {
            Dictionary<string, string> mappFunctionNameCode = new Dictionary<string, string>();
            mappFunctionNameCode.Add("actionName", "actionCode");
            return mappFunctionNameCode;
        }

        public static async Task GetCurrentUserInf()
        {
            int count = 0;
            while(isLoadingUser && count < 5 && AuthHelper.User == null)
            {
                Thread.Sleep(500);
                count++;
            }
            if (!isLoadingUser) {
                //Task.Run(async () =>
               // {
                    isLoadingUser = true;
                try
                {
                    AuthService authService = new AuthService(_httpClient);
                    var authModel = await authService.GetAuth();
                    if (authModel != null)
                    {
                        //var userString = JsonConvert.SerializeObject(authModel.User);
                        //AuthHelper.User = JsonConvert.DeserializeObject<UserModel>(userString);
                        AuthHelper.User = authModel.User;
                        AuthHelper.RefreshToken = authModel.RefreshToken;
                        IsLogined = true;
                    }
                    else
                    {
                        AuthHelper.User = null;
                        AuthHelper.RefreshToken = "";
                        IsLogined = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                    isLoadingUser = false;
               // });
            }
        }
        public static bool checkFuncType(string[] funcTypeCodes)
        {
            if (AuthHelper.IsAdmin()) return true;
            return true;

        }
        // nhóm nghiệp vụ, nghiệp vụ, action
        public static bool checkFunctionPermission(string functionTypeCode, string FuncCode)
        {
            if (AuthHelper.IsAdmin()) return true;
            if (string.IsNullOrEmpty(functionTypeCode?.Trim()) || string.IsNullOrEmpty(FuncCode?.Trim())) return false;

            return true;
        }
    }
}
