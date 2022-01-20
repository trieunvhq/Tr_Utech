 
using System; 
using System.Threading.Tasks; 
using QRMS.Constants; 
using QRMS.Models;
using QRMS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific; 

namespace QRMS.Views
{
    public partial class NhapKhoDungCuPage : ContentPage
    {
        //MyScan _MyScan;
        public string _PurchaseOrderNo = "";
        public string _WarehouseCode = "";
        public string _WarehouseName= "";
        public DateTime? _PurchaseOrderDate;

        public NhapKhoDungCuPageModel ViewModel { get; set; }
        public NhapKhoDungCuPage(string PurchaseOrderNo, string WarehouseCode, DateTime? PurchaseOrderDate, string WarehouseName)
        { 
            InitializeComponent();
            _PurchaseOrderNo = PurchaseOrderNo;
            _WarehouseCode = WarehouseCode;
            _PurchaseOrderDate = PurchaseOrderDate;
            _WarehouseName = WarehouseName;
            grid.Children.Remove(absPopup_DangXuat);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false); 
            ViewModel = new NhapKhoDungCuPageModel(this); 
            ViewModel.Initialize();
            BindingContext = ViewModel;
            //_MyScan = new MyScan();
            //_MyScan._NhapKhoDungCuPageModel = ViewModel;

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
                    if((ViewModel.Historys != null && ViewModel.Historys.Count > 0)
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

        public void CloseCam()
        {
            try
            {
                //if (_MyScan != null)
                //    _MyScan.CloseBarcodeReader();
            }
            catch { }
        }
        void BtnQuet_CLicked(System.Object sender, System.EventArgs e)
        {
            try
            { 
                //ViewModel.IsThongBao = false;
                //ViewModel.IsMatDoc_Camera = true;
                //if(_MyScan!=null)
                //{
                //    try
                //    {
                //        _MyScan.CloseBarcodeReader();
                //    }
                //    catch { }
                //}    
                //_MyScan.OpenBarcodeReader();
            }
            catch (Exception ee)
            { 
                MySettings.InsertLogs(0, DateTime.Now, "BtnQuet_CLicked", ee.Message, "NhapKhoDungCuPage", MySettings.UserName);

            }
        }
           
        public NhapKhoDungCuModel model_;
        public decimal soluong_;
        public int i;
        public QRModel qr;
        public string str;
        public async Task Load_popup_DangXuat(string tieude, string nutdongy, string huybo)
        { 
            await Controls.LoadingUtility.HideAsync();
            if(huybo=="")
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
                App.Dblocal.DeletePurchaseOrderAsyncWithKey(_PurchaseOrderNo);

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
