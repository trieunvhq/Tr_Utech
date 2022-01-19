 
using System;
using Acr.UserDialogs;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class ChonPhieuNhapPage : ContentPage
    {
        MyScan _MyScan; 

        public ChonPhieuNhapPageModel ViewModel { get; set; }
        public ChonPhieuNhapPage()
        { 
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new ChonPhieuNhapPageModel();
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel._ChonPhieuNhapPage = this;
            _MyScan = new MyScan();
            _MyScan._ChonPhieuNhapPageModel = ViewModel;

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
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                    await Controls.LoadingUtility.HideAsync();
                });
            });

        }


        async void BtnLuuLai_CLicked(System.Object sender, System.EventArgs e)
        {
            await Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                { 
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NhapKhoDungCuPage(
                        ViewModel._PurchaseOrderNo, ViewModel._WarehouseCode, ViewModel._PurchaseOrderDate ,ViewModel._WarehouseName));
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

        public void CloseCam()
        {
            try
            {
                if (_MyScan != null)
                    _MyScan.CloseBarcodeReader();
            }
            catch { }
        }
        void BtnQuet_CLicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                ViewModel.IsThongBao = false;
                ViewModel.IsMatDoc_Camera = true;
                if (_MyScan != null)
                {
                    try
                    {
                        _MyScan.CloseBarcodeReader();
                    }
                    catch { }
                }
                _MyScan.OpenBarcodeReader();
            }
            catch (Exception ee)
            {
                UserDialogs.Instance.AlertAsync(ee.Message, "Exception", "OK").ConfigureAwait(false);
                MySettings.InsertLogs(0, DateTime.Now, "BtnQuet_CLicked", ee.Message, "ChonPhieuNhapPage", MySettings.UserName);

            }
        }

        public void ResetCamera()
        {

            //ZXingScannerView scannerView = new ZXingScannerView();
            //scannerView.OnScanResult += (result) => { scanView_OnScanResult(result); };
            ////scannerView.OnScanResult += scanView_OnScanResult();
            //grid.Children.Add(scannerView, 0, 8, 0, 10);
            //camera.IsScanning = true;


            camera.IsScanning = true;
            camera.IsAnalyzing = true;
        }
        void BtnCamera_CLicked(System.Object sender, System.EventArgs e)
        {
            ViewModel.IsMatDoc_Camera = false;
            ResetCamera();
            //row.Height = 150;
            ViewModel.isDangQuet = false;
            ViewModel.IsTat = false;
            ViewModel.IsThongBao = false;
            ViewModel.IsQuet = true;
        }


        void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                camera.IsAnalyzing = false;
                ViewModel.ScanComplate(result.Text);
                camera.IsAnalyzing = true;
            });
        }

        void btnDongQuet_Clicked(System.Object sender, System.EventArgs e)
        {
            //row.Height = 50;
            ViewModel.IsTat = true;
            ViewModel.IsThongBao = false;
            ViewModel.IsQuet = false;
            //ViewModel.StopDemThoiGianGGS();
        }


    }
}
