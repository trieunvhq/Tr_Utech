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
    public class DistrictAPI
    {
        private static List<DistrictModel> lstDistrict  = new List<DistrictModel>();

        public static List<DistrictModel> GetListDistrict(int provinceId)
        {
            try
            {
                List<DistrictModel> listItem = new List<DistrictModel>();
                var DistrictNumPage = 1;
                var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<DistrictModel>>>(Constaint.ServiceAddress, "cus-api/district/getall",
                    new
                    {
                        PAGE = DistrictNumPage,
                        PROVINCE_ID = provinceId
                    });
                if (result.Result.data != null && result.Result.data.Count > 0)
                {
                    foreach (var item in result.Result.data)
                    {
                        listItem.Add(new DistrictModel()
                        {
                            ID = item.ID,
                            PROVINCE_ID = item.PROVINCE_ID,
                            PROVINCE_CODE = item.PROVINCE_CODE,
                            CODE = item.CODE,
                            NAME = item.NAME
                        });
                    }

                    if (result.Result.data[0].PageTotal > 1)
                    {
                        DistrictNumPage++;
                        for (int i = DistrictNumPage; i <= result.Result.data[0].PageTotal; i++)
                        {
                            var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<DistrictModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetListDistrictAll,
                                new
                                {
                                    page = i,
                                    provinceID = provinceId
                                });
                            if (result2.data != null && result2.data.Count > 0)
                            {
                                foreach (var item in result2.data)
                                {
                                    listItem.Add(new DistrictModel()
                                    {
                                        ID = item.ID,
                                        PROVINCE_ID = item.PROVINCE_ID,
                                        PROVINCE_CODE = item.PROVINCE_CODE,
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
                var actionName = "QRMS.API.DistrictAPI.GetListDistrict()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static Task<List<DistrictModel>> GetListDistrictAsync(int provinceId)
        {
            //nếu ID của tỉnh thành không có thì trả về mảng rỗng
            if (provinceId == 0) return Task.Run(() => { return new List<DistrictModel>(); });

            //lấy danh sách quận huyện đã có hoặc load từ API
            List<DistrictModel> listItem = lstDistrict.Where(a => a.PROVINCE_ID == provinceId).ToList();
            if (listItem.Count > 0)
                return Task.Run(() => { return listItem; });
            else
            {
                var DistrictNumPage = 1;
                Task<BaseModel<List<DistrictModel>>> firstCall = APIHelper.GetObjectFromAPIAsync<BaseModel<List<DistrictModel>>>
                    (Constaint.ServiceAddress, Constaint.APIurl.GetListDistrictAll, new { page = DistrictNumPage, provinceID = provinceId });

                return firstCall.ContinueWith(first =>
                {
                    var firstResult = firstCall.Result;

                    if (firstResult.data != null && firstResult.data.Count > 0)
                    {
                        listItem.AddRange(firstResult.data);

                        if (firstResult.data[0].PageTotal > 1)
                        {
                            List<Task<BaseModel<List<DistrictModel>>>> downloadTasksQuery = new List<Task<BaseModel<List<DistrictModel>>>>();
                            DistrictNumPage++;
                            for (int i = DistrictNumPage; i <= firstResult.data[0].PageTotal; i++)
                            {
                                var _index = i;
                                var result2 = APIHelper.GetObjectFromAPIAsync<BaseModel<List<DistrictModel>>>
                                (Constaint.ServiceAddress, Constaint.APIurl.GetListDistrictAll, new { page = i, provinceID = provinceId });
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
                    lstDistrict = lstDistrict.Union(listItem).GroupBy(a => new { a.ID, a.CODE }).Select(a => a.FirstOrDefault()).ToList();
                    return listItem;

                });
            }
        }
    }
}
