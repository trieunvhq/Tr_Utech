using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class VehicleModelAPI
    {
        public static List<VehicleModelModel> GetListVehModel(int vehBrandId)
        {
            try
            {
                List<VehicleModelModel> listItem = new List<VehicleModelModel>();
                var VehModelNumPage = 1;
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<VehicleModelModel>>>(Constaint.ServiceAddress, "/api/vehiclemodel/getall",
                    new
                    {
                        page = VehModelNumPage,
                        vehBrandID = vehBrandId
                    });
                if (result.data != null && result.data.Count > 0)
                {
                    foreach (var item in result.data)
                    {
                        listItem.Add(new VehicleModelModel()
                        {
                            ID = item.ID,
                            CODE = item.CODE,
                            NAME = item.NAME,
                            BRAND_ID = item.BRAND_ID,
                            VEHICLE_TYPE = item.VEHICLE_TYPE,
                            PRICE = item.PRICE,
                            SHORT_NAME = item.SHORT_NAME
                        });
                    }

                    if (result.data[0].PageTotal > 1)
                    {
                        VehModelNumPage++;
                        for (int i = VehModelNumPage; i <= result.data[0].PageTotal; i++)
                        {
                            var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<VehicleModelModel>>>(Constaint.ServiceAddress, "/api/vehiclemodel/getall",
                                new
                                {
                                    page = i,
                                    vehBrandID = vehBrandId
                                });
                            if (result2.data != null && result2.data.Count > 0)
                            {
                                foreach (var item in result2.data)
                                {
                                    listItem.Add(new VehicleModelModel()
                                    {
                                        ID = item.ID,
                                        CODE = item.CODE,
                                        NAME = item.NAME,
                                        BRAND_ID = item.BRAND_ID,
                                        VEHICLE_TYPE = item.VEHICLE_TYPE,
                                        PRICE = item.PRICE,
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
                var actionName = "QRMS.API.VehicleModelAPI.GetListVehModel()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}
