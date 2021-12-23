
using System;
using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.QMK;
using QRMS.Resources;
using QRMS.Views.AccountPage;
using Xamarin.Forms;

namespace QRMS.ViewModels.Account
{
    public class DoiMKPageModel : BaseViewModel
    {
        public string TieuDe { get; set; }
        public string MatKhau { get; set; }
        public string MatKhauMoi { get; set; }
        public string MatKhauMoiLai { get; set; }
        
        public string ThongBao1 { get; set; }
        public string ThongBao2 { get; set; }
        public string ThongBao3 { get; set; }
        public bool Note1 { get; set; } = false;
        public bool Note2 { get; set; } = false;
        public bool Note3 { get; set; } = false;

        public override void OnAppearing()
        {
            base.OnAppearing();
            if (MySettings.Vi_En)
            {
                TieuDe = "Mật khẩu mới của bạn không được trùng với những mật khẩu đã sử dụng trước đây. Thông tin mật khẩu của bạn nên bao gồm ít nhất 8 ký tự bao gồm chữ hoa/ thường,số & ký tự đặc biệt như: !, @, #, $, %, &...";
                ThongBao1 = "Chưa nhập mật khẩu hiện tại";
                ThongBao2 = "Có ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt";
                ThongBao3 = "Cả hai mật khẩu phải trùng nhau.";

            }
            else
            {
                TieuDe = "Mật khẩu mới của bạn không được trùng với những mật khẩu đã sử dụng trước đây. Thông tin mật khẩu của bạn nên bao gồm ít nhất 8 ký tự bao gồm chữ hoa/ thường,số & ký tự đặc biệt như: !, @, #, $, %, &...";
                ThongBao1 = "Chưa nhập mật khẩu hiện tại";
                ThongBao2 = "Có ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt";
                ThongBao3 = "Cả hai mật khẩu phải trùng nhau.";
            }
        }
        public void ExecuteBackPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void ExecuteNextPage()
        {
            try
            {

                Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                {
                    var submit = APIHelper.PostObjectToAPIAsync<BaseModel<object>>
                            (Constaint.ServiceAddress, Constaint.APIurl.changepassword,
                            new
                            {
                                AccountId = FormTypeModel.UserID,
                                CurrentPassword = MatKhau,
                                NewPassword = MatKhauMoi,
                            });
                    _ = submit.ContinueWith(next =>
                    {
                        if (submit.Result.ErrorCode != null && submit.Result.ErrorCode != "")
                        {
                            Controls.LoadingUtility.HideAsync();
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                switch (submit.Result.ErrorCode)
                                {
                                    case "0":
                                        var page = new DK_TK_TCPage();
                                        Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(page);
                                        break;
                                    case "1":
                                        ThongBao1 = "Sai mật khẩu hiện tại";
                                        Note1 = true; 
                                        break;
                                    case "2":
                                        Xamarin.Forms.Application.Current.MainPage.DisplayAlert(AppResources.ThongBaoDangNhap1, "Không tìm thấy tài khoẻn có ID = " + FormTypeModel.UserID, "OK");
                                        break;
                                    case "3":
                                        Xamarin.Forms.Application.Current.MainPage.DisplayAlert(AppResources.ThongBaoDangNhap1, "Cập nhật mật khẩu mới thất bại", "OK");
                                        break;
                                    case "99":
                                        Xamarin.Forms.Application.Current.MainPage.DisplayAlert(AppResources.ThongBaoDangNhap1, AppResources.LoiKhongXacDinh, "OK");
                                        break;
                                }
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();
                            });
                        }
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
