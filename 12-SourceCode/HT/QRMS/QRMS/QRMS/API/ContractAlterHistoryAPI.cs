using QRMS.AppLIB.Common;
using QRMS.Helper;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class ContractAlterHistoryAPI
    {
        public static List<ContractAlterHistory> GetVehicleHistoryByKey()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<List<ContractAlterHistory>>>(Constaint.ServiceAddress, Constaint.APIurl.GetVehicleAlterHistory, FormTypeModel.Key);
                return result.data;
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
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
                var actionName = "QRMS.API.ContractAlterHistoryAPI.GetVehicleHistoryByKey()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static List<ContractAlterHistory> GetMotoCompulsoryHistoryByKey()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<List<ContractAlterHistory>>>(Constaint.ServiceAddress, Constaint.APIurl.GetMotoCompulsoryAlterHistory, FormTypeModel.Key);
                return result.data;
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
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
                var actionName = "QRMS.API.ContractAlterHistoryAPI.GetMotoCompulsoryHistoryByKey()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static List<ContractAlterHistory> GetCarCompulsoryHistoryByKey()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<List<ContractAlterHistory>>>(Constaint.ServiceAddress, Constaint.APIurl.GetCarCompulsoryAlterHistory, FormTypeModel.Key);
                return result.data;
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
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
                var actionName = "QRMS.API.ContractAlterHistoryAPI.GetCarCompulsoryHistoryByKey()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static List<ContractAlterHistory> GetTravelHistoryByKey()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<List<ContractAlterHistory>>>(Constaint.ServiceAddress, Constaint.APIurl.GetTravelAlterHistory, FormTypeModel.Key);
                return result.data;
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
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
                var actionName = "QRMS.API.ContractAlterHistoryAPI.GetTravelHistoryByKey()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static List<ContractAlterHistory> GetHealthCancerHistoryByKey()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<List<ContractAlterHistory>>>(Constaint.ServiceAddress, Constaint.APIurl.GetHealthCancerAlterHistory, FormTypeModel.Key);
                return result.data;
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
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
                var actionName = "QRMS.API.ContractAlterHistoryAPI.GetHealthCancerHistoryByKey()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static List<ContractAlterHistory> GetHealthFatalDiseaseHistoryByKey()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<List<ContractAlterHistory>>>(Constaint.ServiceAddress, Constaint.APIurl.GetHealthFatalDiseaseAlterHistory, FormTypeModel.Key);
                return result.data;
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
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
                var actionName = "QRMS.API.ContractAlterHistoryAPI.GetHealthFatalDiseaseHistoryByKey()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static List<ContractAlterHistory> GetHomeInsuranceHistoryByKey()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<List<ContractAlterHistory>>>(Constaint.ServiceAddress, Constaint.APIurl.GetTravelAlterHistory, FormTypeModel.Key);
                return result.data;
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
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
                var actionName = "QRMS.API.ContractAlterHistoryAPI.GetHomeInsuranceHistoryByKey()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}