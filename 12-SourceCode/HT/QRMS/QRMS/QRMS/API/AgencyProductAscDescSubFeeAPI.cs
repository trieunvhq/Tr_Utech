using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class AgencyProductAscDescSubFeeAPI
    {
        public static List<AGENCY_PRODUCT_ASC_DESC_SUBFEEModel> GetAscDescFee(int agentId, string productCode)
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<AGENCY_PRODUCT_ASC_DESC_SUBFEEModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetAscDescFee,
                    new
                    {
                        agentId = agentId,
                        productCode = productCode
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
                var actionName = "QRMS.API.AgencyProductAscDescSubFeeAPI.GetAscDescFee()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}
