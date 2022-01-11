 
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
    public partial class KK_SCANPage : ContentPage
    {
        MyScan _MyScan; 

        public KK_SCANPageModel ViewModel { get; set; }
        public KK_SCANPage(string no)
        { 
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new KK_SCANPageModel();
            ViewModel.WarehouesCode = no;
            ViewModel.Initialize();
            BindingContext = ViewModel;
            ViewModel._KK_SCANPage = this;
            //_MyScan = new MyScan(1, ViewModel);
            //
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
                    var answer = await UserDialogs.Instance.ConfirmAsync("Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?", "Thông báo", "Có lưu", "Không lưu");
                    if (answer)
                    {
                    }
                    else
                    { 
                        App.Dblocal.DeleteHistory_KKDC(ViewModel.WarehouesCode, ViewModel.WarehouesCode);
                    }

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

        void BtnQuet_CLicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (_MyScan != null)
                    _MyScan.CloseBarcodeReader();
                _MyScan = new MyScan();
                _MyScan._KK_SCANPageModel = ViewModel;
                _MyScan.OpenBarcodeReader();
            }
            catch (Exception ee)
            {
                UserDialogs.Instance.AlertAsync(ee.Message, "Exception", "OK").ConfigureAwait(false);
                MySettings.InsertLogs(0, DateTime.Now, "BtnQuet_CLicked", ee.Message, "KK_SCANPage", MySettings.UserName);

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
