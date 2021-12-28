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
    public class WardAPI
    {
        private static List<WardModel> lstWard = new List<WardModel>();
        public static List<WardModel> GetListWard(int districtId)
        {
            try
            {
                List<WardModel> listItem = new List<WardModel>();
                var WardNumPage = 1;
                var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<WardModel>>>(Constaint.ServiceAddress, "cus-api/ward/getall",
                    new
                    {
                        PAGE = WardNumPage,
                        DISTRICT_ID = districtId
                    });
                if (result.Result.data != null && result.Result.data.Count > 0)
                {
                    foreach (var item in result.Result.data)
                    {
                        listItem.Add(new WardModel()
                        {
                            ID = item.ID,
                            DISTRICT_ID = item.DISTRICT_ID,
                            DISTRICT_CODE = item.DISTRICT_CODE,
                            CODE = item.CODE,
                            NAME = item.NAME
                        });
                    }

                    if (result.Result.data[0].PageTotal > 1)
                    {
                        WardNumPage++;
                        for (int i = WardNumPage; i <= result.Result.data[0].PageTotal; i++)
                        {
                            var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<WardModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetListWardAll,
                                new
                                {
                                    page = i,
                                    districtID = districtId
                                });
                            if (result2.data != null && result2.data.Count > 0)
                            {
                                foreach (var item in result2.data)
                                {
                                    listItem.Add(new WardModel()
                                    {
                                        ID = item.ID,
                                        DISTRICT_ID = item.DISTRICT_ID,
                                        DISTRICT_CODE = item.DISTRICT_CODE,
                                        CODE = item.CODE,
                                        NAME = item.NAME
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
                var actionName = "QRMS.API.WardAPI.GetListWard()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static Task<List<WardModel>> GetListWardAsync(int districtId)
        {
            if (districtId == 0) return Task.Run(() => { return new List<WardModel>(); });
            List<WardModel> listItem = lstWard.Where(a=>a.DISTRICT_ID == districtId).ToList();
            if (listItem.Count > 0)
                return Task.Run(() => { return listItem; });
            else
            {
                int WardNumPage = 1;
                Task<BaseModel<List<WardModel>>> firstCall = APIHelper.GetObjectFromAPIAsync<BaseModel<List<WardModel>>>
                    (Constaint.ServiceAddress, Constaint.APIurl.GetListWardAll, new { page = WardNumPage, districtID = districtId });

                return firstCall.ContinueWith(first =>
                {
                    var firstResult = firstCall.Result;

                    if (firstResult.data != null && firstResult.data.Count > 0)
                    {
                        listItem.AddRange(firstResult.data);

                        if (firstResult.data[0].PageTotal > 1)
                        {
                            List<Task<BaseModel<List<WardModel>>>> downloadTasksQuery = new List<Task<BaseModel<List<WardModel>>>>();
                            WardNumPage++;
                            for (int i = WardNumPage; i <= firstResult.data[0].PageTotal; i++)
                            {
                                var _index = i;
                                Task<BaseModel<List<WardModel>>> result2 = APIHelper.GetObjectFromAPIAsync<BaseModel<List<WardModel>>>
                                    (Constaint.ServiceAddress, Constaint.APIurl.GetListWardAll, new { page = WardNumPage, districtID = districtId });
                                downloadTasksQuery.Add(result2);
                            }

                            listItem = Task.WhenAll(downloadTasksQuery).ContinueWith(add =>
                            {
                                foreach (var item in downloadTasksQuery)
                                {
                                    listItem.AddRange(item.Result.data);
                                }
                                listItem = listItem.GroupBy(a => new { a.ID, a.CODE }).Select(a => a.FirstOrDefault()).ToList();

                                return listItem;
                            }).Result;
                        }
                        lstWard = lstWard.Union(listItem)
                                        .GroupBy(a => new { a.ID, a.CODE })
                                        .Select(a => a.FirstOrDefault())
                                        .ToList();
                    }
                    return listItem;
                });
            }
        }
    }
}
