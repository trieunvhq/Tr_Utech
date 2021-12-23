using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace QRMS.API
{
    public class BannerAPI
    {
        public static List<ImageModel> GetImage(string appType)
        {
            try
            {
                List<ImageModel> lstItem = new List<ImageModel>();
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<ImageBannerModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetImageBanner, new { appType = appType });
                if (result != null)
                {
                    foreach (var item in result.data)
                    {
                        var byteArray = Convert.FromBase64String(item.ImageBase64);
                        var stream = new MemoryStream(byteArray);
                        lstItem.Add(new ImageModel()
                        {
                            Position = item.Position,
                            ImageBase64 = stream == null ? null : ImageSource.FromStream(() => stream)
                        });
                    }
                }
                return lstItem;
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
                var appTypeToLog = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.BannerAPI.GetImage()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }
    }
}
