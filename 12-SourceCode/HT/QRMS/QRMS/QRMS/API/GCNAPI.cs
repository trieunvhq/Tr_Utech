using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class GCNAPI
    {
        public static List<InsuranceContractModel> GetListGCN(string stringName)
        {
            try
            {
                List<InsuranceContractModel> listItem = new List<InsuranceContractModel>();
                var NumPage = 1;
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<InsuranceContractModel>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.GCN,
                        new
                        {
                            value = stringName,
                            page = NumPage
                        });
                if (result.data != null && result.data.Count > 0)
                {
                    foreach (var item in result.data)
                    {
                        listItem.Add(item);
                    }

                    if (result.data[0].PageTotal > 1)
                    {
                        NumPage++;
                        for (int i = NumPage; i <= result.data[0].PageTotal; i++)
                        {
                            var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<InsuranceContractModel>>>
                                (Constaint.ServiceAddress, Constaint.APIurl.GCN,
                                new
                                {
                                    value = stringName,
                                    page = i
                                });
                            if (result2.data != null && result2.data.Count > 0)
                            {
                                foreach (var item in result2.data)
                                {
                                    listItem.Add(item);
                                }
                            }
                        }
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
                var actionName = "QRMS.API.GCNAPI.GetListGCN()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}
