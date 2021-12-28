using Acr.UserDialogs;
using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class AccountAPI
    {
        public static AccountModel CheckAccount(string user, string pass)
        {
            try
            {
                var ipAddress = MobileInfo.GetIP();
                var deviceName = MobileInfo.GetDeviceInfo();

                var result = APIHelper.GetObjectFromAPI<BaseModel<AccountModel>>(Constaint.ServiceAddress, "/api/account/check",
                            new
                            {
                                ipAddress = ipAddress,
                                deviceName = deviceName,
                                user = user,
                                pass = pass
                            });
                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.AccountAPI.CheckAccount2()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static ConfigModel CheckAccount2(string user, string pass,string token, out bool exception)
        {
            try
            {
                var ipAddress = MobileInfo.GetIP();
                var deviceName = MobileInfo.GetDeviceInfo();

                var result = APIHelper.GetObjectFromAPI<BaseModel<ConfigModel>>(Constaint.ServiceAddress, "/api/account/check",
                            new
                            {
                                ipAddress = ipAddress,
                                deviceName = deviceName,
                                user = user,
                                pass = pass,
                                token = token
                            });
                exception = false;
                return result.data;
            }
            catch (Exception ex)
            {
                exception = true;
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.AccountAPI.CheckAccount2()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static ConfigModel Login(string user, string pass, string token, out bool exception)
        {
            try
            {
                var ipAddress = MobileInfo.GetIP();
                var deviceName = MobileInfo.GetDeviceInfo();

                var result = APIHelper.GetObjectFromAPI<BaseModel<ConfigModel>>(Constaint.ServiceAddress, Constaint.APIurl.login,
                            new
                            {
                                ipAddress = ipAddress,
                                deviceName = deviceName,
                                user = user,
                                pass = pass,
                                token = token
                            });
                exception = false;
                return result.data;
            }
            catch (Exception ex)
            {
                exception = true;
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.AccountAPI.CheckAccount2()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static int LockAccount(string user)
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<int>>(Constaint.ServiceAddress, "/api/account/lock", new { user = user });
                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.AccountAPI.LockAccount()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return -1;
            }
        }

        public static int UpdatePassword(string user, string newPass)
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<int>>(Constaint.ServiceAddress, "/api/account/changepassword", new { user = user, newPass = newPass });
                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.AccountAPI.UpdatePassowrd()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return -1;
            }
        }

        public static CusCustomerInfoModel GetAccountByID(string id)
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<CusCustomerInfoModel>>(Constaint.ServiceAddress, "api-cus/customer/getbyaccountid", new
                {
                    ACCOUNT_ID = FormTypeModel.UserID
                });
                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.AccountAPI.GetACcountByID()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}
