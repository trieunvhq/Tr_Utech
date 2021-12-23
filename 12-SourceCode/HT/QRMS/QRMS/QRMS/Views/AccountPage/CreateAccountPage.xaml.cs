using Acr.UserDialogs;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Resources;
using QRMS.ViewModels;
using QRMS.ViewModels.BH_DuLich.DL_NNNTVN;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace QRMS.Views.AccountPage
{
    public partial class CreateAccountPage : ContentPage
    {
        public CreateAccountPageModel ViewModel { get; set; } = new CreateAccountPageModel();

        public CreateAccountPage()
        {
            MySettings.Title = AppResources.TaoTaiKhoanMoi;
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Shell.SetTabBarIsVisible(this, false);

            ViewModel.Initialize();
            BindingContext = ViewModel;


            ViewModel.NextCommand = new Command(() =>
            {
                OnNextButtonClicked(this, System.EventArgs.Empty);
            });
            ViewModel.BackCommand = new Command(() =>
            {
                OnBackButtonClicked(this, System.EventArgs.Empty);
            });

            inputTinhThanh.SelectedIndexChanged = (o, e) =>
            {
                ViewModel.QuanHuyens.Clear();
                if (ViewModel.SelectedTinhThanh != null)
                {
                    ViewModel.LoadDistrict(ViewModel.SelectedTinhThanh.ID);
                }
            };
            inputQuanHuyen.SelectedIndexChanged = (o, e) =>
            {
                ViewModel.XaPhuongs.Clear();
                if (ViewModel.SelectedQuanHuyen != null)
                {
                    ViewModel.LoadWard(ViewModel.SelectedQuanHuyen.ID);
                }
            };


            inputNgaySinh.MaximumDate = DateTime.Now;
        }

        protected override void OnAppearing()
        {
            Console.WriteLine("CreateAccountPage");
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnDisappearing();
        }

        void OnNextButtonClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var isError = false;

                inputSoCMT.ErrorMessage = AppResources.NhapSCMTCCCDHC;
                inputEmail.ErrorMessage = AppResources.TravelCustomerEmailError;
                inputNgaySinh.IsError = false;

                inputHoVaTen.IsError = string.IsNullOrEmpty(ViewModel.NAME);
                isError = inputHoVaTen.IsError;
                inputSoCMT.IsError = string.IsNullOrEmpty(ViewModel.IDENTITY_NO);
                isError = isError == true ? true : inputSoCMT.IsError;
                inputSDT.IsError = string.IsNullOrEmpty(ViewModel.PHONE);
                isError = isError == true ? true : inputSDT.IsError;
                inputEmail.IsError = string.IsNullOrEmpty(ViewModel.EMAIL);
                isError = isError == true ? true : inputEmail.IsError;
                inputGioiTinh.IsError = ViewModel.SelectedGioiTinh == null;
                isError = isError == true ? true : inputGioiTinh.IsError;


                inputXaPhuong.IsError = ViewModel.SelectedXaPhuong == null;
                isError = isError == true ? true : inputXaPhuong.IsError;
                inputQuanHuyen.IsError = ViewModel.SelectedQuanHuyen == null;
                isError = isError == true ? true : inputQuanHuyen.IsError;
                inputTinhThanh.IsError = ViewModel.SelectedTinhThanh == null;
                isError = isError == true ? true : inputTinhThanh.IsError;
                inputDiaChi.IsError = string.IsNullOrEmpty(ViewModel.CUST_ADDRESS);
                isError = isError == true ? true : inputDiaChi.IsError;


                if (!string.IsNullOrEmpty(ViewModel.EMAIL))
                {
                    if (MobileLib.IsEmail(ViewModel.EMAIL) == false)
                    {
                        isError = true;
                        inputEmail.IsError = true;
                        inputEmail.ErrorMessage = AppResources.IsEmailError;
                    }
                }
                
                if (ViewModel.Cust_Birthday.Date.Year + 18 < DateTime.Now.Year)
                {
                }
                else if (ViewModel.Cust_Birthday.Date.Year + 18 == DateTime.Now.Year)
                {
                    if (ViewModel.Cust_Birthday.Date.Month < DateTime.Now.Month)
                    {
                    }
                    else if (ViewModel.Cust_Birthday.Date.Month == DateTime.Now.Month)
                    {
                        if (ViewModel.Cust_Birthday.Date.Day > DateTime.Now.Day)
                        {
                            inputNgaySinh.IsError = true;
                            isError = isError = true;
                        }
                    }
                    else
                    {
                        inputNgaySinh.IsError = true;
                        isError = isError = true;
                    }
                }
                else
                {
                    inputNgaySinh.IsError = true;
                    isError = isError = true;
                }

                if (isError == false)
                {
                    ViewModel.ExecuteNextPage();
                }
                else
                {
                    try {
                        //scroll.ScrollToAsync(0, 0, true);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
#if DEBUG
                UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
            }
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            ViewModel.CAMT();
        }

        void ImageButton_Clicked_1(System.Object sender, System.EventArgs e)
        {
            ViewModel.CAMS();
        }



        private void inputEmail_Unfocused(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel.EMAIL))
            {
                if (MobileLib.IsEmail(ViewModel.EMAIL) == false)
                {
                    inputEmail.IsError = true;
                    inputEmail.ErrorMessage = AppResources.IsEmailError;
                }
            }
            else
            {
                inputEmail.ErrorMessage = AppResources.TravelCustomerEmailError;
            }
        }

        void OnBackButtonClicked(System.Object sender, System.EventArgs e)
        {
            ViewModel.ExecuteBackPage();
        }

        void OnShowListButtonClicked(System.Object sender, System.EventArgs e)
        {
            ViewModel.ExecuteShowPage();
        }

        private void inputSDT_Unfocused(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel.PHONE))
            {
                ViewModel.PHONE = System.Text.RegularExpressions.Regex.Replace(ViewModel.PHONE, @"[^a-zA-Z0-9]", string.Empty);
            }
        }

        void ChupAnhT_Tapped(System.Object sender, System.EventArgs e)
        {
            ViewModel.CAMT();
        }

        void ChonAnhT_Tapped(System.Object sender, System.EventArgs e)
        {
            ViewModel.PickT();
        }
        void ChupAnhS_Tapped(System.Object sender, System.EventArgs e)
        {
            ViewModel.CAMS();
        }

        void ChonAnhS_Tapped(System.Object sender, System.EventArgs e)
        {
            ViewModel.PickS();
        }

        void inputSoCMT_Unfocused(System.Object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel.IDENTITY_NO))
            {
                if (MobileLib.IsVI(ViewModel.IDENTITY_NO) == false)
                {
                    inputSoCMT.IsError = true;
                    inputSoCMT.ErrorMessage = AppResources.SCMTCCCDHCKHL;
                }
            }
            else
            {
                inputSoCMT.ErrorMessage = AppResources.NhapSCMTCCCDHC;
            }
        }
    }
}
