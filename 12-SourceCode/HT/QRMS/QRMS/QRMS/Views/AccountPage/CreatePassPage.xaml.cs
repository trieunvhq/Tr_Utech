 
using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Resources;
using QRMS.ViewModels;
using QRMS.ViewModels.Account;
using QRMS.ViewModels.BH_DuLich.DL_NNNTVN;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace QRMS.Views.AccountPage
{
    public partial class CreatePassPage : ContentPage
    {
        CreatePassViewmodel ViewModel = new CreatePassViewmodel();
        public CreateAccountPageModel _CreateAccountPageModel;
        public CreatePassPage(CreateAccountPageModel createAccountPageModel)
        {
            _CreateAccountPageModel = createAccountPageModel;
            MySettings.Title = AppResources.TaoMatKhauDangNhap;
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
                lbNote1.Text = "Thông tin mật khẩu của bạn nên bao gồm ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt như: !, @, #, $, %, &..\"";
                lbNote2.Text = "Có ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt";
                lbNote3.Text = "Cả hai mật khẩu phải trùng nhau.";
            }
            else
            {
                lbNote1.Text = "Thông tin mật khẩu của bạn nên bao gồm ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt như: !, @, #, $, %, &..\"";
                lbNote2.Text = "Có ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt";
                lbNote3.Text = "Cả hai mật khẩu phải trùng nhau";
            }
        }
          

        void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var isError = false;

                if(txtPassword.Text==null || txtPassword.Text.Length < 8)
                {
                    isError = true;
                    lbNote2.TextColor = Color.Red;
                    framePass.BorderColor = Color.Red;
   
                }
                else
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
                                            (Constaint.ServiceAddress, Constaint.APIurl.create,
                                            new
                                            {
                                                FULL_NAME = _CreateAccountPageModel.NAME,
                                                EMAIL = _CreateAccountPageModel.EMAIL.ToLower(),
                                                MOBILE = _CreateAccountPageModel.PHONE.ToLower(),
                                                DOB = _CreateAccountPageModel.Cust_Birthday,
                                                GENDER_ID = _CreateAccountPageModel.SelectedGioiTinh.Key,
                                                GENDER_NAME = _CreateAccountPageModel.SelectedGioiTinh.Value,
                                                IMAGE_IDENTIFY_FRONT = _CreateAccountPageModel.IMAGE_IDCARD_FRONT,
                                                IMAGE_IDENTIFY_BACK = _CreateAccountPageModel.IMAGE_IDCARD_BACK,
                                                IDENTITY_NO = _CreateAccountPageModel.IDENTITY_NO,
                                                PROVINCE_ID = _CreateAccountPageModel.SelectedTinhThanh.ID,
                                                DISTRICT_ID = _CreateAccountPageModel.SelectedQuanHuyen.ID,
                                                WARD_ID = _CreateAccountPageModel.SelectedXaPhuong.ID,
                                                ADDRESS = _CreateAccountPageModel.CUST_ADDRESS,
                                                Password = txtPassword2.Text
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
                                                        Xamarin.Forms.Application.Current.MainPage.DisplayAlert(AppResources.ThongBaoDangNhap1, AppResources.ThongBaoDangNhap2, "OK");

                                                        break;
                                                    case "2":
                                                        Xamarin.Forms.Application.Current.MainPage.DisplayAlert(AppResources.ThongBaoDangNhap1, AppResources.ThongBaoTaoTaiKhoan8, "OK");
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
