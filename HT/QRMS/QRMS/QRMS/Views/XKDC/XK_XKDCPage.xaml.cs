 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.XKDC;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace QRMS.Views
{
    public partial class XK_XKDCPage : ContentPage
    {
        //MyScan _MyScan;
        private string _PuschaseNo = "";

        public XK_XKDCPageModel ViewModel { get; set; }
        public XK_XKDCPage(string id, string no, DateTime d)
        {
            _PuschaseNo = no;
            InitializeComponent();

            grid.Children.Remove(absPopup_DangXuat);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new XK_XKDCPageModel(id, no, d);
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel._XK_XKDCPage = this;
            //_MyScan = new MyScan(1, ViewModel);
            //

            //_MyScan = new MyScan();
            //_MyScan._XK_XKDCPageModel = ViewModel;

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
                    if ((ViewModel.Historys != null && ViewModel.Historys.Count > 0)
                       || (ViewModel.DonHangs != null && ViewModel.DonHangs.Count > 0))
                    {
                        await Load_popup_DangXuat("Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?", "Có lưu", "không lưu");

                    }
                    else
                    {
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                        await Controls.LoadingUtility.HideAsync();
                    } 
                });
            });

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

        void lst_combobox_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
           

        public SaleOrderItemScanBPL model_;
        public decimal soluong_;
        public int i;
        public QRModel qr;
        public string str;
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
            else if (lbTieuDe_absPopup.Text == "Bạn đã nhập kho vượt quá số lượng đơn mua")
            {
                ViewModel.XuLyTiepLuu(true, model_, soluong_, i, qr, str);
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã lưu thất bại")
            {
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã lưu thành công")
            {
            }

        }
        private async void BtnHuyBo_popup_DangXuat_Clicked(object sender, EventArgs e)
        {
            await absPopup_DangXuat.FadeTo(0, 200);
            if (grid.Children.Contains(absPopup_DangXuat))
                _ = grid.Children.Remove(absPopup_DangXuat);

            if (lbTieuDe_absPopup.Text == "Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?")
            {
                App.Dblocal.DeleteHistoryAll();
                App.Dblocal.DeletePurchaseOrderAsyncWithKey(_PuschaseNo);

                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                await Controls.LoadingUtility.HideAsync();
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã nhập kho vượt quá số lượng đơn mua")
            {
                ViewModel.XuLyTiepLuu(false, model_, soluong_, i, qr, str);
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã lưu thất bại")
            {
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã lưu thành công")
            {
            }
        }

    }
}

