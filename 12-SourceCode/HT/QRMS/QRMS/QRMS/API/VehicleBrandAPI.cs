using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class VehicleBrandAPI
    {
        public static List<VehicleBrandModel> GetListVehBrand()
        {
            try
            {
                List<VehicleBrandModel> listItem = new List<VehicleBrandModel>();
                var VehBrandNumPage = 1;
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<VehicleBrandModel>>>(Constaint.ServiceAddress, "/api/vehiclebrand/getall", new { page = VehBrandNumPage });
                if (result.data != null && result.data.Count > 0)
                {
                    foreach (var item in result.data)
                    {
                        listItem.Add(new VehicleBrandModel()
                        {
                            ID = item.ID,
                            CODE = item.CODE,
                            NAME = item.NAME,
                            SHORT_NAME = item.SHORT_NAME
                        });
                    }

                    if (result.data[0].PageTotal > 1)
                    {
                        VehBrandNumPage++;
                        for (int i = VehBrandNumPage; i <= result.data[0].PageTotal; i++)
                        {
                            var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<VehicleBrandModel>>>(Constaint.ServiceAddress, "/api/vehiclebrand/getall", new { page = i });
                            if (result2.data != null && result2.data.Count > 0)
                            {
                                foreach (var item in result2.data)
                                {
                                    listItem.Add(new VehicleBrandModel()
                                    {
                                        ID = item.ID,
                                        CODE = item.CODE,
                                        NAME = item.NAME,
                                        SHORT_NAME = item.SHORT_NAME
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
                var actionName = "QRMS.API.VehicleBrandAPI.GetListVehBrand()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
        public static List<VehicleBrandGarageFacilityModel> GetListVehicleBrandGarageFacility(int provinceId, int? vehicleBrandId)
        {
            try
            {
                List<VehicleBrandGarageFacilityModel> listItem = new List<VehicleBrandGarageFacilityModel>();
                var NumPage = 1;
                var result = APIHelper.PostObjectToAPI<BaseModel<List<VehicleBrandGarageFacilityModel>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.VehicleBrand,
                        new
                        {
                            CarCompanyId = vehicleBrandId,
                            ProvinceId = provinceId,
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
                            var result2 = APIHelper.PostObjectToAPI<BaseModel<List<VehicleBrandGarageFacilityModel>>>
                                (Constaint.ServiceAddress, Constaint.APIurl.VehicleBrand,
                                new
                                {
                                    CarCompanyId = vehicleBrandId,
                                    ProvinceId = provinceId,
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
                var actionName = "QRMS.API.VehicleBrandAPI.GetListVehicleBrandGarageFacility()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
        public static List<VehicleBrandGarageFacilityModel> GetListGaraNear(double X, double Y)
        {
            try
            {
                List<VehicleBrandGarageFacilityModel> listItem = new List<VehicleBrandGarageFacilityModel>();
                var NumPage = 1;
                var result = APIHelper.PostObjectToAPI<BaseModel<List<VehicleBrandGarageFacilityModel>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.GetListGaraNear,
                        new
                        {
                            latitude = X,
                            longitude = Y,
                            page = NumPage
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
                var actionName = "QRMS.API.VehicleBrandAPI.GetListGaraNear()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}
