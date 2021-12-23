 
using System;
using Acr.UserDialogs;
using FFImageLoading.Svg.Forms;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Resources;
using QRMS.ViewModels;
using QRMS.ViewModels.Account; 
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views.AccountPage
{
    public partial class TaiKhoanPage : ContentPage
    {
        private bool isload = false;
        public TaiKhoanPageModel ViewModel { get; set; }

        public TaiKhoanPage()
        {
            FormTypeModel.VehicleInsuranceType = FormTypeModel.InsurProductCode;
            MySettings.Title = AppResources.BHTNDSXM;
            isload = true;
            InitializeComponent();
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);

            ViewModel = new TaiKhoanPageModel();
            ViewModel.Initialize();

            BindingContext = ViewModel; 
            ViewModel.BackCommand = new Command(() =>
            {
                ViewModel.ExecuteBackPage();
            });
             

            isload = false;
        }
         
        protected override void OnAppearing()
        {
            isload = true; 
            base.OnAppearing();
            ViewModel.OnAppearing();
            isload = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnDisappearing();
        }

        void OnChiaSeDanhGiaButtonClicked(System.Object sender, System.EventArgs e)
        {
        }

        void BtnBoQuaClicked(System.Object sender, System.EventArgs e)
        {
        }
         
        void star2_Tapped(System.Object sender, System.EventArgs e)
        {
            string str_ = ((SvgCachedImage)sender).ClassId;
            switch(str_)
            {
                case "1":
                    ViewModel.star2_1 = "star_cam.svg";
                    ViewModel.star2_2 = "star_den.svg";
                    ViewModel.star2_3 = "star_den.svg";
                    ViewModel.star2_4 = "star_den.svg";
                    ViewModel.star2_5 = "star_den.svg"; 
                    break;
                case "2":
                    ViewModel.star2_1 = "star_cam.svg";
                    ViewModel.star2_2 = "star_cam.svg";
                    ViewModel.star2_3 = "star_den.svg";
                    ViewModel.star2_4 = "star_den.svg";
                    ViewModel.star2_5 = "star_den.svg";
                    break;
                case "3":
                    ViewModel.star2_1 = "star_cam.svg";
                    ViewModel.star2_2 = "star_cam.svg";
                    ViewModel.star2_3 = "star_cam.svg";
                    ViewModel.star2_4 = "star_den.svg";
                    ViewModel.star2_5 = "star_den.svg";
                    break;
                case "4":
                    ViewModel.star2_1 = "star_cam.svg";
                    ViewModel.star2_2 = "star_cam.svg";
                    ViewModel.star2_3 = "star_cam.svg";
                    ViewModel.star2_4 = "star_cam.svg";
                    ViewModel.star2_5 = "star_den.svg";
                    break;
                case "5":
                    ViewModel.star2_1 = "star_cam.svg";
                    ViewModel.star2_2 = "star_cam.svg";
                    ViewModel.star2_3 = "star_cam.svg";
                    ViewModel.star2_4 = "star_cam.svg";
                    ViewModel.star2_5 = "star_cam.svg";
                    break;
            }    
        }
    }
}
