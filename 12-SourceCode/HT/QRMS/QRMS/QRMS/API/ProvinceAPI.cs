using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRMS.API
{
    public class ProvinceAPI
    {
        private static List<ProvinceModel> lstProvine = new List<ProvinceModel>();

        public static List<ProvinceModel> GetListProvince()
        {
            try
            {
                if (lstProvine.Count > 0) return lstProvine;
                List<ProvinceModel> listItem = new List<ProvinceModel>();
                var ProvinceNumPage = 1;
                var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<ProvinceModel>>>
                    (Constaint.ServiceAddress, "/cus-api/province/getall", new { PAGE = ProvinceNumPage });
                if (result.Result.data != null && result.Result.data.Count > 0)
                {
                    foreach (var item in result.Result.data)
                    {
                        listItem.Add(new ProvinceModel()
                        {
                            ID = item.ID,
                            COUNTRY_ID = item.COUNTRY_ID.Value,
                            CODE = item.CODE,
                            NAME = item.NAME
                        });
                    }

                    if (result.Result.data[0].PageTotal > 1)
                    {
                        ProvinceNumPage++;
                        for (int i = ProvinceNumPage; i <= result.Result.data[0].PageTotal; i++)
                        {
                            var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<ProvinceModel>>>
                                (Constaint.ServiceAddress, "/cus-api/province/getall", new { PAGE = i });
                            if (result2.data != null && result2.data.Count > 0)
                            {
                                foreach (var item in result2.data)
                                {
                                    listItem.Add(new ProvinceModel()
                                    {
                                        ID = item.ID,
                                        COUNTRY_ID = item.COUNTRY_ID.Value,
                                        CODE = item.CODE,
                                        NAME = item.NAME
                                    });
                                }
                            }
                        }
                    }
                }
                lstProvine.AddRange(listItem);
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
                var actionName = "QRMS.API.ProvinceAPI.GetListProvince()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
        public static List<ProvinceModel> GetListProvinceWithKV(int groupDivisionId)
        {
            try
            {
                if (lstProvine.Count > 0) return lstProvine;
                List<ProvinceModel> listItem = new List<ProvinceModel>();
                var ProvinceNumPage = 1;
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<ProvinceModel>>>
                    (Constaint.ServiceAddress, Constaint.APIurl.Getdivisiondatabygroupdivision, groupDivisionId);
                if (result.data != null && result.data.Count > 0)
                {
                    foreach (var item in result.data)
                    {
                        listItem.Add(new ProvinceModel()
                        {
                            ID = item.ID,
                            COUNTRY_ID = item.COUNTRY_ID.Value,
                            CODE = item.CODE,
                            NAME = item.NAME
                        });
                    }

                    if (result.data[0].PageTotal > 1)
                    {
                        ProvinceNumPage++;
                        for (int i = ProvinceNumPage; i <= result.data[0].PageTotal; i++)
                        {
                            var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<ProvinceModel>>>
                                (Constaint.ServiceAddress, "/api/province/getall", new { page = i });
                            if (result2.data != null && result2.data.Count > 0)
                            {
                                foreach (var item in result2.data)
                                {
                                    listItem.Add(new ProvinceModel()
                                    {
                                        ID = item.ID,
                                        COUNTRY_ID = item.COUNTRY_ID.Value,
                                        CODE = item.CODE,
                                        NAME = item.NAME
                                    });
                                }
                            }
                        }
                    }
                }
                lstProvine.AddRange(listItem);
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
                var actionName = "QRMS.API.ProvinceAPI.GetListProvinceWithKV()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        
        public static Task<List<ProvinceModel>> GetProvincesAsync()
        {
            try
            {
                if (lstProvine.Count > 0) return Task.Run(() => { return lstProvine; });
                else
                {
                    var ProvinceNumPage = 1;
                    return APIHelper.GetObjectFromAPIAsync<BaseModel<List<ProvinceModel>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.GetProvince, new { page = ProvinceNumPage })
                        .ContinueWith(first =>
                        {
                            List<ProvinceModel> listItem = first.Result.data ?? new List<ProvinceModel>();
                            if (listItem.Count > 0)
                            {
                                if (listItem[0].PageTotal > 1)
                                {
                                    List<Task<BaseModel<List<ProvinceModel>>>> downloadTasksQuery = new List<Task<BaseModel<List<ProvinceModel>>>>();
                                    ProvinceNumPage++;
                                    for (int i = ProvinceNumPage; i <= listItem[0].PageTotal; i++)
                                    {
                                        var _index = i;
                                        var result2 = APIHelper.GetObjectFromAPIAsync<BaseModel<List<ProvinceModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetProvince, new { page = _index });
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
                            }
                            lstProvine.AddRange(listItem);
                            return listItem;
                        });
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
                var actionName = "QRMS.API.ProvinceAPI.GetListProvincesAsync()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}
