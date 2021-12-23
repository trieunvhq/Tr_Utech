using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class LookupPJCAPI
    {
        public static List<LookupInsuranceAgentModel> GetListPJC(int divisionId, int? provinceId, int? agentId)
        {
            try
            {
                List<LookupInsuranceAgentModel> listItem = new List<LookupInsuranceAgentModel>();
                var NumPage = 1;
                var result = APIHelper.PostObjectToAPI<BaseModel<List<LookupInsuranceAgentModel>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.GetListPJC,
                        new
                        {
                            GroupDivisionId = divisionId,
                            ProvinceId = provinceId,
                            InsuranceAgentId = agentId,
                            Page = NumPage
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
                            var result2 = APIHelper.PostObjectToAPI<BaseModel<List<LookupInsuranceAgentModel>>>
                                (Constaint.ServiceAddress, Constaint.APIurl.GetListPJC,
                                new
                                {
                                    GroupDivisionId = divisionId,
                                    ProvinceId = provinceId,
                                    InsuranceAgentId = agentId,
                                    Page = i
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
                var actionName = "QRMS.API.LookupPJCAPI.GetListPJC()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static List<LookupInsuranceAgentModel> GetListPJCNear(double X, double Y)
        {
            try
            {
                List<LookupInsuranceAgentModel> listItem = new List<LookupInsuranceAgentModel>();
                var NumPage = 1;
                var result = APIHelper.PostObjectToAPI<BaseModel<List<LookupInsuranceAgentModel>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.GetListPJCNear,
                        new
                        {
                            Latitude = X,
                            Longitude = Y,
                            Page = NumPage
                        });
                if (result.data != null && result.data.Count > 0)
                {
                    foreach (var item in result.data)
                    {
                        listItem.Add(item);
                    }

                    //if (result.data[0].PageTotal > 1)
                    //{
                    //    NumPage++;
                    //    for (int i = NumPage; i <= result.data[0].PageTotal; i++)
                    //    {
                    //        var result2 = APIHelper.PostObjectToAPI<BaseModel<List<LookupInsuranceAgentModel>>>
                    //            (Constaint.ServiceAddress, Constaint.APIurl.GetListPJCNear,
                    //            new
                    //            {
                    //                Latitude = X,
                    //                Longitude = Y,
                    //                Page = i
                    //            });
                    //        if (result2.data != null && result2.data.Count > 0)
                    //        {
                    //            foreach (var item in result2.data)
                    //            {
                    //                listItem.Add(item);
                    //            }
                    //        }
                    //    }
                    //}
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
                var actionName = "QRMS.API.LookupPJCAPI.GetListPJCNear()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}
