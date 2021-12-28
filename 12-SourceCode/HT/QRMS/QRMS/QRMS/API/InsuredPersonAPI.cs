using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRMS.API
{
    public class InsuredPersonAPI
    {

        public static async Task<int> UpdateInsureds(int CartID,string ContractCode,InsuredPerson person)
        {
            if (person == null) return 0;
            else
            {
                var dataAPI = await APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                    (Constaint.ServiceAddress, Constaint.APIurl.UpdateHealthInsureds,
                    new
                    {
                        userID = FormTypeModel.UserID,
                        CartID = CartID,
                        ContractCode = ContractCode,
                        InsuredPerson = person
                    }).ConfigureAwait(false);
                return dataAPI.data;
            }
        }

        public static List<InsuredPersonModel> ListInsuredPerson(string contractCode)
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<List<InsuredPersonModel>>>
                    (Constaint.ServiceAddress, Constaint.APIurl.GetListInsuredPerson, contractCode);
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
                var actionName = "QRMS.API.InsuredPersonAPI.ListInsuredPerson()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static async Task<List<InsuredPerson>> ListInsuredPersonsAsync(int contractID, string contractCode,string contractType)
        {
            try
            {
                string url = Constaint.APIurl.GetInsPersonContract;
                object obj;
                if ("carted" == contractType?.ToLower())
                {
                    url = Constaint.APIurl.GetInsPersonCart;
                    obj = new { cartID = contractID, contractCode, loadIMG = true };
                }
                else
                {
                    obj = new { contractID, contractCode, loadIMG = true };
                }
                var result = await APIHelper.GetObjectFromAPIAsync<BaseModel<List<InsuredPerson>>>
                    (Constaint.ServiceAddress, url,obj);

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
                var actionName = "QRMS.API.InsuredPersonAPI.ListInsuredPersonAsync()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static async Task<int> DeleteInsuredPersonAsync(string ContractKey, string ContractKeyType, string contractType, params string[] Identity)
        {
            try
            {
                if (string.IsNullOrEmpty(ContractKey)) return 0;
                else
                {
                    BaseModel<int> CallAPI = await APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                        (Constaint.ServiceAddress, Constaint.APIurl.DeleteInsPerson, new { key = ContractKey, keyType = ContractKeyType,contractType, Identity }).ConfigureAwait(false);
                    return CallAPI.data;
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
                var actionName = "QRMS.API.InsuredPersonAPI.DeleteInsuredPersonAsync()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return -1;
            }
        }

        public static async Task<int> DeleteInsuredPersonAsync(string ContractKey, string ContractKeyType, string contractType, params int[] InsuredID)
        {
            try
            {
                if (string.IsNullOrEmpty(ContractKey)) return 0;
                else
                {
                    BaseModel<int> CallAPI = await APIHelper.PostObjectToAPIAsync<BaseModel<int>>
                        (Constaint.ServiceAddress, Constaint.APIurl.DeleteInsPerson, new { key = ContractKey, keyType = ContractKeyType,contractType, InsuredID }).ConfigureAwait(false);
                    return CallAPI.data;
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
                var actionName = "QRMS.API.InsuredPersonAPI.DeleteInsuredPersonAsync()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return -1;
            }
        }
    }
}
