 
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
    public partial class NhapKhoDungCuPage : ContentPage
    {
        MyScan _MyScan;
        public string _PurchaseOrderNo = "";
        public string _WarehouseCode = "";
        public DateTime? _PurchaseOrderDate;

        public NhapKhoDungCuPageModel ViewModel { get; set; }
        public NhapKhoDungCuPage(string PurchaseOrderNo, string WarehouseCode, DateTime? PurchaseOrderDate)
        { 
            InitializeComponent();
            _PurchaseOrderNo = PurchaseOrderNo;
            _WarehouseCode = WarehouseCode;
            _PurchaseOrderDate = PurchaseOrderDate;
            grid.Children.Remove(absPopup_DangXuat);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false); 
            ViewModel = new NhapKhoDungCuPageModel(this); 
            ViewModel.Initialize();
            BindingContext = ViewModel;
            _MyScan = new MyScan();
            _MyScan._NhapKhoDungCuPageModel = ViewModel;

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
                    //bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
                    await Load_popup_DangXuat("Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?", "Có lưu", "không lưu"); 
                   
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
                if(_MyScan!=null)
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
                MySettings.InsertLogs(0, DateTime.Now, "BtnQuet_CLicked", ee.Message, "NhapKhoDungCuPage", MySettings.UserName);

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

        public bool iscoluu;
        public NhapKhoDungCuModel model_;
        public decimal soluong_;
        public int i;
        public QRModel qr;
        public string str;
        public async Task Load_popup_DangXuat(string tieude, string nutdongy, string huybo)
        { 
            await Controls.LoadingUtility.HideAsync();
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

        }
        private async void BtnHuyBo_popup_DangXuat_Clicked(object sender, EventArgs e)
        {
            await absPopup_DangXuat.FadeTo(0, 200);
            if (grid.Children.Contains(absPopup_DangXuat))
                _ = grid.Children.Remove(absPopup_DangXuat);

            if (lbTieuDe_absPopup.Text == "Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?")
            {
                App.Dblocal.DeleteHistoryAll();
                App.Dblocal.DeletePurchaseOrderAsyncWithKey(_PurchaseOrderNo);

                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                await Controls.LoadingUtility.HideAsync();
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã nhập kho vượt quá số lượng đơn mua")
            {
                ViewModel.XuLyTiepLuu(false, model_, soluong_, i, qr, str);
            }
        }
    }
}
