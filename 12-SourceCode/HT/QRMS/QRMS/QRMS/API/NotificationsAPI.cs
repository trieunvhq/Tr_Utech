using QRMS.AppLIB.Common;
using QRMS.Helper;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class NotificationsAPI
    {
        public static List<AccountNotifyModel> GetNotifyByAccount(string username)
        {
            try
            {
                List<AccountNotifyModel> listItem = new List<AccountNotifyModel>();
                var result = APIHelper.PostObjectToAPI<BaseModel<List<AccountNotifyModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetNotifyByUser,
                    new
                    {
                        username = username
                    });
                if (result.data != null && result.data.Count > 0)
                {
                    foreach (var item in result.data)
                    {
                        listItem.Add(new AccountNotifyModel()
                        {
                            ID = item.ID,
                            NOTIFY_ID = item.NOTIFY_ID,
                            NOTIFY_TYPE = item.NOTIFY_TYPE,
                            NEWS_ID = item.NEWS_ID,
                            NEWS_CODE = item.NEWS_CODE,
                            NEWS_NAME = item.NEWS_NAME,
                            CONTRACT_ID = item.CONTRACT_ID,
                            CONTRACT_CODE = item.CONTRACT_CODE,
                            CONTRACT_TYPE = item.CONTRACT_TYPE,
                            NOTIFY = item.NOTIFY,
                            IMAGE = item.IMAGE,
                            PUBLIC_DATE = item.PUBLIC_DATE,
                            SEND = item.SEND,
                            VIEWED = item.VIEWED,
                            REMARK = item.REMARK,
                            STATUS_CONTRACT = item.STATUS_CONTRACT,
                            INSUR_PRODUCT_CODE = item.INSUR_PRODUCT_CODE,
                            CONTRACT_ISSUE_TYPE = item.CONTRACT_ISSUE_TYPE,
                            AGENT_ID = item.AGENT_ID,
                            ACCOUNT_ID = item.ACCOUNT_ID
                        });
                    }
                }
                return listItem;
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
                var actionName = "QRMS.API.NotificationsAPI.GetNotifyByAccount()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                DependencyService.Get<ILogger>().Log(ex.ToString());
                throw;
            }
        }

        public static string ChangeViewNotify(int id)
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<string>>(Constaint.ServiceAddress, Constaint.APIurl.ChangeViewNotify, new { id = id });
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
                var actionName = "QRMS.API.NotificationsAPI.ChangeViewNotify()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                DependencyService.Get<ILogger>().Log(ex.ToString());
                throw;
            }
        }

        public static string CheckNotify(int id)
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<string>>(Constaint.ServiceAddress, Constaint.APIurl.CheckNotify, id);
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
                var actionName = "QRMS.API.NotificationsAPI.CheckNotify()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                DependencyService.Get<ILogger>().Log(ex.ToString());
                throw;
            }
        }

        public static BaseModel<string> GetToken(string token)
        {
            try
            {
               var result = APIHelper.PostObjectToAPI<BaseModel<string>>(Constaint.ServiceAddress, Constaint.APIurl.GetToken,
                    new
                    {
                        token = token
                    });
                return result;
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
                var actionName = "QRMS.API.NotificationsAPI.GetToken()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                DependencyService.Get<ILogger>().Log(ex.ToString());
                throw;
            }
        }
        public static BaseModel<string> CheckAccountToken(string token, string id)
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<string>>(Constaint.ServiceAddress, Constaint.APIurl.CheckAccountToken,
                    new
                    {
                        token = token,
                        account_id = id
                    });
                return result;
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
                var actionName = "QRMS.API.NotificationsAPI.CheckAccountToken()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                DependencyService.Get<ILogger>().Log(ex.ToString());
                throw;
            }
        }

    }
}
