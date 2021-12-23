
using System;
using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.QMK;
using QRMS.Views.AccountPage;
using Xamarin.Forms;

namespace QRMS.ViewModels.Account
{
    public class LayLaiMKViewmodel : BaseViewModel
    {
        public string DiaChiEmailSDT { get; set; }
        public override void OnAppearing()
        {
            base.OnAppearing();

        }
        public void ExecuteBackPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void ExecuteNextPage()
        {
            try
            {
                _ = Controls.LoadingUtility.ShowAsync().ContinueWith(a =>
                {
                    var result = APIHelper.PostObjectToAPIAsync<BaseModel<CusGetOtpForgotPasswordModel>>
                                              (Constaint.ServiceAddress, Constaint.APIurl.getotpcodeforgotpassword,
                                               new
                                               {
                                                   EMAIL_OR_MOBILE = DiaChiEmailSDT
                                               });
                    _ = result.ContinueWith(next =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Controls.LoadingUtility.HideAsync();
                            if (result.Result != null && result.Result.data != null
                            && result.Result.RespondCode == "0")
                            {
                                FormTypeModel.UserID = result.Result.data.AccountId;
                                var page = new NhapMaOTPPage(DiaChiEmailSDT, result.Result.data);
                                Application.Current.MainPage.Navigation.PushAsync(page);
                            }
                            else
                            {
                                UserDialogs.Instance.AlertAsync(result.Result.Message, "Thông báo", "OK");
                            }  
                        });
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.HideAsync();
                    DependencyService.Get<ILogger>().Log(ex.ToString());

                    // Log ex to db
                    var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                    var FCMTockenValue = String.Empty;
                    if (FCMToken)
                    {
                        FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                    }
                    var token = FCMTockenValue;
                    var appType = Constaint.App_Type.Agent;
                    var osType = Device.OS.ToString();
                    var namespaceInFile = GetType().Namespace;
                    var className = GetType().Name;
                    var methodName = "CallThreadContract";
                    var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                    var userId = FormTypeModel.UserID;
                    LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
        }
    }
}
