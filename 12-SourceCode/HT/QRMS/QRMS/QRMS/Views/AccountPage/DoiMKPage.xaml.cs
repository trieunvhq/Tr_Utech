 
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
using Application = Xamarin.Forms.Application;

namespace QRMS.Views.AccountPage
{
    public partial class DoiMKPage : ContentPage
    {
        DoiMKPageModel ViewModel = new DoiMKPageModel();
        public DoiMKPage()
        {
            MySettings.Title = AppResources.ThayDoiMatKhau;
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

        }


        protected override void OnAppearing()
        {
            Console.WriteLine("DoiMKPage");
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
        void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var isError = false;
                ViewModel.Note1 = false;
                ViewModel.Note2 = false;
                ViewModel.Note3 = false;

                string str1_ = "";
                string str2_ = "";

                string str2_1 = "";
                string str2_2 = "";
                string str3_1 = "";
                string str3_2 = "";
                string str2_3 = "";
                if (MySettings.Vi_En)
                {
                    str2_1 = "Vui lòng nhập mật khẩu mới";
                    str2_3 = "Mật khẩu mới không được trùng với mật khẩu hiện tại";
                    str3_1 = "Vui lòng xác nhận lại mật khẩu mới";
                    str2_2 = "Có ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt";
                    str3_2 = "Cả hai mật khẩu phải trùng nhau.";

                }
                else
                {
                    str2_3 = "Mật khẩu mới không được trùng với mật khẩu hiện tại";
                    str2_1 = "Vui lòng nhập mật khẩu mới";
                    str3_1 = "Vui lòng xác nhận lại mật khẩu mới";
                    str2_2 = "Có ít nhất 8 ký tự bao gồm chữ hoa/thường,số & ký tự đặc biệt";
                    str3_2 = "Cả hai mật khẩu phải trùng nhau.";
                }
                //
                if (MySettings.Vi_En)
                {
                    str1_ = "Vui lòng nhập mật khẩu hiện tại";
                    str2_ = "Sai mật khẩu hiện tại";
                }
                else
                {
                    str1_ = "Vui lòng nhập mật khẩu hiện tại";
                    str2_ = "Sai mật khẩu hiện tại";
                }
                if (txtPassword1.Text == null)
                {
                    ViewModel.ThongBao1 = str1_;
                    isError = true;
                    ViewModel.Note1 = true;
                    framePass1.BorderColor = Color.Red;

                }
                else if ( txtPassword1.Text.Length < 8)
                {
                    ViewModel.ThongBao1 = str2_;
                    isError = true;
                    ViewModel.Note1 = true;
                    framePass1.BorderColor = Color.Red;

                }
                if (txtPassword2.Text == null)
                {
                    ViewModel.ThongBao2 = str2_1;
                    isError = true;
                    ViewModel.Note2 = true; 
                    framePass2.BorderColor = Color.Red;

                }
                else if (txtPassword2.Text.Length < 8)
                {
                    ViewModel.ThongBao2 = str2_2;
                    isError = true;
                    ViewModel.Note2 = true;
                    framePass2.BorderColor = Color.Red;

                }
                if (txtPassword3.Text == null)
                {
                    ViewModel.ThongBao3 = str3_1;
                    isError = true;
                    ViewModel.Note3 = true;
                    framePass3.BorderColor = Color.Red;

                }
                else if (txtPassword3.Text.Length < 8 && txtPassword2.Text!=txtPassword3.Text)
                {
                    ViewModel.ThongBao3 = str3_2;
                    isError = true;
                    ViewModel.Note3 = true;
                    framePass3.BorderColor = Color.Red;

                }
                if(!string.IsNullOrWhiteSpace(txtPassword1.Text) && !string.IsNullOrWhiteSpace(txtPassword2.Text)
                    && txtPassword1.Text== txtPassword2.Text)
                { 
                    ViewModel.ThongBao2 = str2_3;
                    isError = true;
                    ViewModel.Note2 = true;
                    framePass2.BorderColor = Color.Red;
                }    
                if (!isError)
                {
                    string password = txtPassword2.Text;
                    bool containsAtLeastOneUppercase = password.Any(char.IsUpper);
                    bool containsAtLeastOneLowercase = password.Any(char.IsLower);
                    bool containsAtLeastOneSpecialChar = password.Any(ch => !Char.IsLetterOrDigit(ch));
                    bool containsAtLeastOneDigit = password.Any(char.IsDigit);
                    if (containsAtLeastOneUppercase && containsAtLeastOneLowercase
                        && containsAtLeastOneSpecialChar && containsAtLeastOneDigit)
                    {
                        ViewModel.Note2 = false; 
                        framePass2.BorderColor = Color.FromHex("#DCE0E2");

                        if (txtPassword2.Text == null || txtPassword2.Text.Length < 8)
                        {
                            isError = true;
                            ViewModel.Note3 = true; 
                            framePass3.BorderColor = Color.Red;
                        }
                        else
                        {
                            if (txtPassword3.Text == txtPassword2.Text)
                            {
                                //
                                ViewModel.Note3 = false; 
                                framePass3.BorderColor = Color.FromHex("#DCE0E2");

                                //
                                if(!isError)
                                    ViewModel.ExecuteNextPage();
                            }
                            else
                            {
                                isError = true;
                                ViewModel.Note3 = true; 
                                framePass3.BorderColor = Color.Red;
                            }
                        }
                    }
                    else
                    {
                        isError = true;
                        ViewModel.Note2 = true; 
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

        void txtPassword1_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            framePass1.BorderColor = Color.FromHex("#DCE0E2");
            ViewModel.Note1 = false;
        }

        void txtPassword2_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            framePass2.BorderColor = Color.FromHex("#DCE0E2");
            ViewModel.Note2 = false; 
        }

        void txtPassword3_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            framePass3.BorderColor = Color.FromHex("#DCE0E2"); 
            ViewModel.Note3 = false;
        }
    }
}
