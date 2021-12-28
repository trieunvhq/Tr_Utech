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
    public class TNDSBB_INS_FEEAPI
    {
        public static TNDSBBdataModel GetFeeTNDSBB()
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<TNDSBBdataModel>>(Constaint.ServiceAddress, "/api/tndsbbfee/getfee",
                             new
                             {
                                 commonTypeCode = FormTypeModel.VehicleType,
                                 purposeCode = FormTypeModel.VehicleIns.purposeCode,
                                 vehType = FormTypeModel.VehicleIns.vehType,
                                 seatNumber = FormTypeModel.VehicleIns.seatNumber,
                                 weightTon = FormTypeModel.VehicleIns.weightTon
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
                var actionName = "QRMS.API.TNDSBB_INS_FEEAPI.GetFeeTNDSBB()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static TNDSBBdataModel GetFeeTNDSBB(string veh_purpose_code = "", string veh_type_code = "", short? veh_seat_no = null, decimal? veh_max_load = null)
        {
            try
            {
                //decimal TNDSBBPrice = 0;
                var result = APIHelper.GetObjectFromAPI<BaseModel<TNDSBBdataModel>>(Constaint.ServiceAddress, "/api/tndsbbfee/getfee",
                             new
                             {
                                 commonTypeCode = FormTypeModel.VehicleType,
                                 purposeCode = veh_purpose_code,
                                 vehType = veh_type_code,
                                 seatNumber = veh_seat_no,
                                 weightTon = veh_max_load
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
                var actionName = "QRMS.API.TNDSBB_INS_FEEAPI.GetFeeTNDSBB()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static List<MTN_MOTO_CAR_INS_FEEModel> GetLimitedLevel(string commonTypeCode)
        {
            try
            {
                List<MTN_MOTO_CAR_INS_FEEModel> lstItem = new List<MTN_MOTO_CAR_INS_FEEModel>();
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<MTN_MOTO_CAR_INS_FEEModel>>>(Constaint.ServiceAddress, "/api/tndsbbfee/getlimitedlevel",
                             new
                             {
                                 commonTypeCode = commonTypeCode
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
                var actionName = "QRMS.API.TNDSBB_INS_FEEAPI.GetLimitedLevel()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static decimal GetPercentFee(string veh_purpose_code = "", decimal? fee = null)
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<decimal>>(Constaint.ServiceAddress, "/api/tndsbbfee/getpercentfee",
                    new
                    {
                        commonTypeCode = FormTypeModel.VehicleType,
                        purposeCode = veh_purpose_code,
                        fee = fee
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
                var actionName = "QRMS.API.TNDSBB_INS_FEEAPI.GetPercentFee()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return 0;
            }
        }

        public static List<TNVNInsFeeModel> GetTNVNFees(string Code)
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<TNVNInsFeeModel>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.TNVNLevel, new { Code = Code });
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
                var actionName = "QRMS.API.TNDSBB_INS_FEEAPI.GetTNVNFees()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                DependencyService.Get<ILogger>().Log(ex.ToString());
                return null;
            }
        }
        public static async Task<List<TNVNInsFeeModel>> GetTNVNFeesAsync(string Code)
        {
            try
            {
                var result = await APIHelper.GetObjectFromAPIAsync<BaseModel<List<TNVNInsFeeModel>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.TNVNLevel, new { Code = Code });
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
                var actionName = "QRMS.API.TNDSBB_INS_FEEAPI.GetTNVNFeesAsync()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                DependencyService.Get<ILogger>().Log(ex.ToString());
                return null;
            }
        }
    }
}
