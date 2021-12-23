using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Resources;
using QRMS.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views.AccountPage
{
    public partial class TaoMKMoiPage : ContentPage
    {
        TaoMKMoiPagemodel ViewModel = new TaoMKMoiPagemodel();
        public TaoMKMoiPage()
        {
            MySettings.Title = AppResources.TaoMatKhauMoi;
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel.BackCommand = new Command(() =>
            {
                ViewModel.ExecuteBackPage();
            });
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Shell.SetTabBarIsVisible(this, false);
            if (MySettings.Vi_En)
            {

                lbNote2.Text = "Có ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt";
                lbNote3.Text = "Cả hai mật khẩu phải trùng nhau.";
            }
            else
            {

                lbNote2.Text = "Có ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt";
                lbNote3.Text = "Cả hai mật khẩu phải trùng nhau";
            }
        }


        void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var isError = false;

                if (txtPassword.Text == null || txtPassword.Text.Length < 8)
                {
                    isError = true;
                    lbNote2.TextColor = Color.Red;
                    framePass.BorderColor = Color.Red;

                }
                if (txtPassword2.Text == null || txtPassword2.Text.Length < 8)
                {
                    isError = true;
                    lbNote3.TextColor = Color.Red;
                    framePass2.BorderColor = Color.Red;

                }
                if (!isError)
                {
                    string password = txtPassword.Text;
                    bool containsAtLeastOneUppercase = password.Any(char.IsUpper);
                    bool containsAtLeastOneLowercase = password.Any(char.IsLower);
                    bool containsAtLeastOneSpecialChar = password.Any(ch => !Char.IsLetterOrDigit(ch));
                    bool containsAtLeastOneDigit = password.Any(char.IsDigit);
                    if (containsAtLeastOneUppercase && containsAtLeastOneLowercase
                        && containsAtLeastOneSpecialChar && containsAtLeastOneDigit)
                    {
                        lbNote2.TextColor = Color.FromHex("#6C757D");
                        framePass.BorderColor = Color.FromHex("#DCE0E2");

                        if (txtPassword2.Text == null || txtPassword2.Text.Length < 8)
                        {
                            isError = true;
                            lbNote3.TextColor = Color.Red;
                            framePass2.BorderColor = Color.Red;
                        }
                        else
                        {
                            if (txtPassword.Text == txtPassword2.Text)
                            {
                                //
                                lbNote3.TextColor = Color.FromHex("#6C757D");
                                framePass2.BorderColor = Color.FromHex("#DCE0E2");

                                //

                                Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                                {
                                    var submit = APIHelper.PostObjectToAPIAsync<BaseModel<object>>
                                            (Constaint.ServiceAddress, Constaint.APIurl.newpassword,
                                            new
                                            {
                                                AccountId = FormTypeModel.UserID,
                                                NewPassword = txtPassword.Text,

                                            });
                                    _ = submit.ContinueWith(next =>
                                    {
                                        if (submit.Result.ErrorCode != null && submit.Result.ErrorCode != "")
                                        {
                                            Controls.LoadingUtility.HideAsync();
                                            Device.BeginInvokeOnMainThread(() =>
                                            {
                                                switch (submit.Result.RespondCode)
                                                {
                                                    case "0":
                                                        MySettings.IsPass = true;
                                                        MySettings.IsVanTay = false;
                                                        MySettings.IsKhuonMat = false;
                                                        Xamarin.Forms.Application.Current.MainPage = new QRMS.Views.LoginPage();
                                                        break;
                                                    case "1":
                                                        Xamarin.Forms.Application.Current.MainPage.DisplayAlert(AppResources.ThongBaoDangNhap1, AppResources.KhongTimThayTaiKhoanCoID, "OK");
                                                        break;
                                                    case "2":
                                                        Xamarin.Forms.Application.Current.MainPage.DisplayAlert(AppResources.ThongBaoDangNhap1, AppResources.TaoMatKhauMoiThatBai, "OK");
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
                            else
                            {
                                isError = true;
                                lbNote3.TextColor = Color.Red;
                                framePass2.BorderColor = Color.Red;
                            }
                        }
                    }
                    else
                    {
                        isError = true;
                        lbNote2.TextColor = Color.Red;
                        framePass.BorderColor = Color.Red;

                        lbNote3.TextColor = Color.Red;
                        framePass2.BorderColor = Color.Red;
                    }
                }

                //

            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
#if DEBUG
                UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
            }
        }

        void txtPassword2_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            framePass2.BorderColor = Color.FromHex("#DCE0E2");
            lbNote3.TextColor = Color.FromHex("#6C757D");
        }

        void txtPassword_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            framePass.BorderColor = Color.FromHex("#DCE0E2");
            lbNote2.TextColor = Color.FromHex("#6C757D");
        }
    }
}
