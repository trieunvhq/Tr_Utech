using QRMS.AppLIB.Common;
using QRMS.Helper;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRMS.API
{
    public class MATERIAL_DKBS_INS_FEEAPI
    {
        public static List<MATERIAL_DKBS_INS_FEEModel> GetTerms()
        {
            try
            {
                List<MATERIAL_DKBS_INS_FEEModel> lstItem = new List<MATERIAL_DKBS_INS_FEEModel>();
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<MATERIAL_DKBS_INS_FEEModel>>>(Constaint.ServiceAddress, "api/material-dkbs-ins-fee/getterm",
                             new
                             {
                                 commonTypeCode = FormTypeModel.VehicleType
                             });
                if (result.data != null && result.data.Count > 0)
                {
                    lstItem = result.data;
                    return lstItem;
                }
                return null;
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
                var actionName = "QRMS.API.MATERIAL_DKBS_INS_FEEAPI.GetTerms()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}