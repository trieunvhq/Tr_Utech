
using System.Threading.Tasks;
using QRMS.Constants;
using QRMS.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace QRMS.Views
{
    public partial class KKDC_CLPage : ContentPage
    {
        public KKDC_CLPageModel ViewModel { get; set; }

        private string _WarehouesCode = "";
        private string _WarehouesName = "";
        private bool _isDisconnect = true;

        public KKDC_CLPage(string WarehouesCode, string WarehouesName)
        {
            _WarehouesCode = WarehouesCode;
            _WarehouesName = WarehouesName;

            InitializeComponent();

            grid.Children.Remove(absPopup_DangXuat);

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);

            ViewModel = new KKDC_CLPageModel(this);
            ViewModel.Initialize();
            BindingContext = ViewModel;

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

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _isDisconnect = true;
                DisplayAlert("Thông báo", "Mất kết nối!", "OK");
            }
            else
            {
                _isDisconnect = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                _isDisconnect = true;
                await DisplayAlert("Thông báo", "Mất kết nối!", "OK");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }


        async void BtnQuayLai_CLicked(System.Object sender, System.EventArgs e)
        { 
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }
         

        async void BtnKiemKeDungCu_CLicked(System.Object sender, System.EventArgs e)
        {
            if (_isDisconnect)
            {
                await DisplayAlert("Thông báo", "Mất kết nối!", "OK");
                return;
            }

            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new KK_SCANPage(_WarehouesCode, _WarehouesName));
        }


        void BtnXoaDuLieuLocal_CLicked(System.Object sender, System.EventArgs e)
        {
            ViewModel.DeleteDBLocal();
        }

        async void BtnLuuDataSever_CLicked(System.Object sender, System.EventArgs e)
        {
            if (_isDisconnect)
            {
                await DisplayAlert("Thông báo", "Mất kết nối!", "OK");
                return;
            }

            ViewModel.SaveDBServer();
        }


        public async Task Load_popup_DangXuat(string tieude, string nutdongy, string huybo)
        {
            Device.BeginInvokeOnMainThread(async () =>
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
            });
        }


        async void BtnDongY_popup_DangXuat_Clicked(System.Object sender, System.EventArgs e)
        {
            await absPopup_DangXuat.FadeTo(0, 200);
            if (grid.Children.Contains(absPopup_DangXuat))
                _ = grid.Children.Remove(absPopup_DangXuat);

            if (lbTieuDe_absPopup.Text == "Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?")
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                await Controls.LoadingUtility.HideAsync();
            }
            else if (lbTieuDe_absPopup.Text == "Đã đủ số lượng")
            {
                //ViewModel.XuLyTiepLuu(true, soluong_, i, qr, str);
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã lưu thất bại")
            {
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã lưu thành công")
            {
            }

            if (MySettings.To_Page == "homepage")
            {
                MySettings.To_Page = "";
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
        }

        async void BtnHuyBo_popup_DangXuat_Clicked(System.Object sender, System.EventArgs e)
        {
            await absPopup_DangXuat.FadeTo(0, 200);
            if (grid.Children.Contains(absPopup_DangXuat))
                _ = grid.Children.Remove(absPopup_DangXuat);

            if (lbTieuDe_absPopup.Text == "Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?")
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                await Controls.LoadingUtility.HideAsync();
            }
            else if (lbTieuDe_absPopup.Text == "Đã đủ số lượng")
            {
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

