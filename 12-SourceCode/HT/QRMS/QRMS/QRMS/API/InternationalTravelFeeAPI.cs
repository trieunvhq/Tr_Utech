using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class InternationalTravelFeeAPI
    {
        public static decimal GetInternationalTravelFee()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<decimal>>(Constaint.ServiceAddress, Constaint.APIurl.GetInternationalTravelFee, FormTypeModel.Key);
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
                var actionName = "QRMS.API.InternationalTravelFeeAPI.GetInternationalTravelFee()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return 0;
            }
        }

        public static decimal GetDomesticTravelFee()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<decimal>>(Constaint.ServiceAddress, Constaint.APIurl.GetDomesticTravelFee, FormTypeModel.Key);
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
                var actionName = "QRMS.API.InternationalTravelFeeAPI.GetDomesticTravelFee()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return 0;
            }
        }

        public static TravelFeeModel GetInternationalTravelForVNFee()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<TravelFeeModel>>(Constaint.ServiceAddress, Constaint.APIurl.GetInternationalTravelForVNFee, FormTypeModel.Key);
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
                var actionName = "QRMS.API.InternationalTravelFeeAPI.GetInternationalTravelForVNFee()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
        
        public static TravelFeeModel GetVNTravelForForeignerFee()
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<TravelFeeModel>>(Constaint.ServiceAddress, Constaint.APIurl.GetVNTravelForForeignerFee, FormTypeModel.Key);
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
                var actionName = "QRMS.API.InternationalTravelFeeAPI.GetVNTravelForForeignerFee()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}
