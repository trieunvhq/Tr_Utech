 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace QRMS.Views
{
    public partial class DC_SCANPage : ContentPage
    {
        //MyScan _MyScan; 

        public DC_SCANPageModel ViewModel { get; set; }
        public DC_SCANPage(string tukho_, string denkho_)
        { 
            InitializeComponent();

            grid.Children.Remove(absPopup_DangXuat);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new DC_SCANPageModel(tukho_, denkho_);
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel._DC_SCANPage = this;
            //_MyScan = new MyScan(1, ViewModel);
            //
            //_MyScan = new MyScan();
            //_MyScan._DC_SCANPageModel = ViewModel;
            row_trencung.Height = 20;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    if (MySettings.h_QRMS >= 812)
                    {
                        row_trencung.Height = 40;
                    }
                    else
                    {
                    }
                }
                else
                {
                    row_trencung.Height = 10 + MySettings.Height_Notch;
                }
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                }
                else
                {
                    row_trencung.Height = 10 + MySettings.Height_Notch;
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            BtnQuayLai_CLicked(null, null);
            return true;
        }
        async void BtnQuayLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Load_popup_DangXuat("Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?", "Có lưu", "không lưu");
                     
                });
            });

        }
          

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
          
        async void BtnLuuLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    ViewModel.LuuLais();
                    await Controls.LoadingUtility.HideAsync();
                });
            });
        }


         
        public async Task Load_popup_DangXuat(string tieude, string nutdongy, string huybo)
        {
            await Controls.LoadingUtility.HideAsync();
            if (huybo == "")
            {
                btnHuyBo_absPopup.IsVisible = false;
            }
            else
            {
                btnHuyBo_absPopup.IsVisible = true;
            }
            //
            btnDongY_absPopup.Text = nutdongy;
            btnHuyBo_absPopup.Text = huybo;
            lbTieuDe_absPopup.Text = tieude;
            if (!grid.Children.Contains(absPopup_DangXuat))
                grid.Children.Add(absPopup_DangXuat);
            grid.RaiseChild(absPopup_DangXuat);
            await absPopup_DangXuat.FadeTo(1, 200);
            grid.RaiseChild(absPopup_DangXuat);
        }
        private async void BtnDongY_popup_DangXuat_Clicked(object sender, EventArgs e)
        {
            await absPopup_DangXuat.FadeTo(0, 200);
            if (grid.Children.Contains(absPopup_DangXuat))
                _ = grid.Children.Remove(absPopup_DangXuat);
            if (lbTieuDe_absPopup.Text == "Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?")
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                await Controls.LoadingUtility.HideAsync();
            }  
        }
        private async void BtnHuyBo_popup_DangXuat_Clicked(object sender, EventArgs e)
        {
            await absPopup_DangXuat.FadeTo(0, 200);
            if (grid.Children.Contains(absPopup_DangXuat))
                _ = grid.Children.Remove(absPopup_DangXuat);

            if (lbTieuDe_absPopup.Text == "Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?")
            {
                App.Dblocal.DeleteHistory_CKDC(ViewModel._LenhDC, ViewModel._TuKho, ViewModel._DenKho);

                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                await Controls.LoadingUtility.HideAsync();
            } 
        }
    }
}
