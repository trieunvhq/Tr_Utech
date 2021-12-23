using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRMS.API
{
    public class CommonValueAPI
    {
        private static List<CommonValueModel> lstCommonVal = new List<CommonValueModel>();
		public static List<CommonValueModel> GetListCommonValue(string common_type = "")
        {
            try
            {
                List<CommonValueModel> listItem = new List<CommonValueModel>();
                var VehTypeNumPage = 1;
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<CommonValueModel>>>(Constaint.ServiceAddress, "/api/commonvalue/getall", 
                    new { 
                        page = VehTypeNumPage,
                        type = common_type
                    });
                if (result.data != null && result.data.Count > 0)
                {
                    foreach (var item in result.data)
                    {
                        listItem.Add(new CommonValueModel()
                        {
                            ID = item.ID,
                            COMMON_TYPE_ID = item.COMMON_TYPE_ID,
                            COMMON_TYPE_CODE = item.COMMON_TYPE_CODE,
                            TYPE_NAME = item.TYPE_NAME,
                            VALUE_CODE = item.VALUE_CODE,
                            VALUE_NAME = item.VALUE_NAME,

                        });
                    }

                    if (result.data[0].PageTotal > 1)
                    {
                        VehTypeNumPage++;
                        for (int i = VehTypeNumPage; i <= result.data[0].PageTotal; i++)
                        {
                            var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<CommonValueModel>>>(Constaint.ServiceAddress, "/api/commonvalue/getall", new { page = i });
                            if (result2.data != null && result2.data.Count > 0)
                            {
                                foreach (var item in result2.data)
                                {
                                    listItem.Add(new CommonValueModel()
                                    {
                                        ID = item.ID,
                                        COMMON_TYPE_ID = item.COMMON_TYPE_ID,
                                        COMMON_TYPE_CODE = item.COMMON_TYPE_CODE,
                                        TYPE_NAME = item.TYPE_NAME,
                                        VALUE_CODE = item.VALUE_CODE,
                                        VALUE_NAME = item.VALUE_NAME,

                                    });
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
                var actionName = "QRMS.API.CommonValueAPI.GetListCommonValue()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static async Task<List<CommonValueModel>> GetListCommonValueByTypeAsync(string commonType)
        {
            try
            {
                if (string.IsNullOrEmpty(commonType)) return new List<CommonValueModel>();
                List<CommonValueModel> listItem = lstCommonVal.Where(a => a.COMMON_TYPE_CODE == commonType).ToList();
                if (listItem.Count > 0)
                    return listItem;
                else
                {
                    BaseModel<List<CommonValueModel>> apiCall = await APIHelper.GetObjectFromAPIAsync<BaseModel<List<CommonValueModel>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.ValueByType, new { CommonType = commonType });

                    listItem = apiCall.data;
                    lstCommonVal.AddRange(listItem);
                    lstCommonVal = lstCommonVal.GroupBy(a => a.ID).Select(a => a.FirstOrDefault()).ToList();

                    return listItem;
                }
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
                var actionName = "QRMS.API.CommonValueAPI.GetListCommonValueByTypeAsync()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static double GetVATRatio()
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<CommonValueModel>>>(Constaint.ServiceAddress, Constaint.APIurl.ValueByCode,
                    new { TypeCode = Constaint.CommonValue.VAT });

                double _return = 0; double.TryParse(result.data.FirstOrDefault().VALUE, out _return);

                return _return;
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
                var actionName = "QRMS.API.CommonValueAPI.GetVATRatio()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return 0;
            }
        }
        public static Task<ApprovalLimit> GetApprovalLimit(int accID, int prodID)
        {
            try
            {
                return APIHelper.GetObjectFromAPIAsync<BaseModel<ApprovalLimit>>
                    (Constaint.ServiceAddress, Constaint.APIurl.GetApprLimit, new { accID = accID, ProductID=prodID })
                    .ContinueWith(result => {return result.Result.data; });                
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
                var actionName = "QRMS.API.CommonValueAPI.GetApprovalLimit()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return (Task<ApprovalLimit>)Task.Run(()=> { return null; });
            }
        }

        public static decimal GetSameOwnerRate()
        {
            try
            {
                var response = APIHelper.GetObjectFromAPI<BaseModel<List<CommonValueModel>>>
                    (Constaint.ServiceAddress, Constaint.APIurl.ValueByType, new { CommonType = Constaint.CommonType.SameOwner });
                if (response?.data.Count > 0)
                {
                    return Convert.ToDecimal(response.data.FirstOrDefault().VALUE);
                }
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
                var actionName = "QRMS.API.CommonValueAPI.GetSameOwnerRate()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return 0;
            }
            return 0;
        }
    }
}
