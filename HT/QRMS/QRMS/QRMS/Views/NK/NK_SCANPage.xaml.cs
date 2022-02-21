
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using QRMS.Constants; 
using QRMS.Models;
using QRMS.Models.XKDC;
using QRMS.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific; 

namespace QRMS.Views
{
    public partial class NK_SCANPage : ContentPage
    {
        //MyScan _MyScan;
        public string No;
        public DateTime? Date;
        public string WarehouseCode;
        public string WarehouseName;
        public string WarehouseCode_To;
        public string WarehouseName_To;
        private bool _isDisconnect = true;

        public NK_SCANPageModel ViewModel { get; set; }
        public NK_SCANPage(string No_, DateTime? Date_
            , string WarehouseCode_, string WarehouseName_
            , string WarehouseCode_To_, string WarehouseName_To_)
        {
            InitializeComponent();
            No = No_;
            Date = Date_;
            WarehouseCode = WarehouseCode_;
            WarehouseName = WarehouseName_;
            WarehouseCode_To = WarehouseCode_To_;
            WarehouseName_To = WarehouseName_To_;

            grid.Children.Remove(absPopup_DangXuat);
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            On<iOS>().SetUseSafeArea(true);
            Shell.SetTabBarIsVisible(this, false);
            ViewModel = new NK_SCANPageModel(this);
            ViewModel.Initialize();
            BindingContext = ViewModel;
            //_MyScan = new MyScan();
            //_MyScan._NK_SCANPageModel = ViewModel;

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
            if (MySettings.Index_Page == 1)
            {
                grid.Children.Remove(lst_Xuat);
                grid.Children.Remove(lst_Chuyen);
                lbTieuDe.Text = "Nhập kho dụng cụ";
                spanPhieu.Text = "Phiếu nhập kho: ";
            }
            else if (MySettings.Index_Page == 2)
            {
                grid.Children.Remove(lst_Nhap);
                grid.Children.Remove(lst_Chuyen);
                lbTieuDe.Text = "Xuất kho dụng cụ";
                spanPhieu.Text = "Phiếu xuất kho: ";
            }
            else if (MySettings.Index_Page == 3)
            {
                grid.Children.Remove(lst_Xuat);
                grid.Children.Remove(lst_Nhap);
                lbTieuDe.Text = "Chuyển kho dụng cụ";
                spanPhieu.Text = "Phiếu chuyển kho: ";
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
            ViewModel.OnAppearing();
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

        protected override bool OnBackButtonPressed()
        {
            BtnQuayLai_CLicked(null, null);
            return true;
        }
        async void BtnQuayLai_CLicked(System.Object sender, System.EventArgs e)
        {
            if (Checklocal())
            {
                await Load_popup_DangXuat("Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?", "Có lưu", "không lưu");
            }
            else
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                await Controls.LoadingUtility.HideAsync();
            } 
        }


        async void BtnLuuLai_CLicked(System.Object sender, System.EventArgs e)
        {
            if (_isDisconnect)
            {
                await DisplayAlert("Thông báo", "Mất kết nối!", "OK");
                return;
            }

            ViewModel.LuuLais();
        }

        public NhapKhoDungCuModel model_;
        public decimal soluong_;
        public int i;
        public QRModel qr;
        public string str;
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

                while (Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack.Count > 3)
                {
                    Xamarin.Forms.Application.Current.MainPage.Navigation.RemovePage(Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack[Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                }
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();

                //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
        }
        private async void BtnHuyBo_popup_DangXuat_Clicked(object sender, EventArgs e)
        {
            await absPopup_DangXuat.FadeTo(0, 200);
            if (grid.Children.Contains(absPopup_DangXuat))
                _ = grid.Children.Remove(absPopup_DangXuat);

            if (lbTieuDe_absPopup.Text == "Chưa lưu dữ liệu quét. Bạn có muốn lưu dữ liệu tạm thời trên thiết bị quét không?")
            {
                if (MySettings.Index_Page == 1)
                {  
                    App.Dblocal.DeletePurchaseOrderAsyncWithKey(No,WarehouseCode);
                    App.Dblocal.DeleteAllHistory_NKDC(No, WarehouseCode);
                }
                else if (MySettings.Index_Page == 2)
                {
                    App.Dblocal.DeleteSaleOrderItemScanBPLAsyncWithKey(No,WarehouseCode);
                    App.Dblocal.DeleteAllHistory_XKDC(No, WarehouseCode);
                }
                else if (MySettings.Index_Page == 3)
                {
                    App.Dblocal.DeleteTransferInstructionAsyncWithKey(No, WarehouseCode, WarehouseCode_To);
                    App.Dblocal.DeleteHistory_CKDC(No, WarehouseCode,WarehouseCode_To);
                } 

                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                await Controls.LoadingUtility.HideAsync();
            }
            else if (lbTieuDe_absPopup.Text == "Đã đủ số lượng")
            {
                //ViewModel.XuLyTiepLuu(false, soluong_, i, qr, str);
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã lưu thất bại")
            {
            }
            else if (lbTieuDe_absPopup.Text == "Bạn đã lưu thành công")
            {
            }
        }

        void txtTest_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            ViewModel.ScanComplate(txtTest.Text.Trim().ToUpper());
        }

        private bool Checklocal()
        {
            try
            {
                
                if (MySettings.Index_Page == 1)
                {
                    List<NhapKhoDungCuModel> donhang_ = App.Dblocal.GetPurchaseOrderAsyncWithKey(No, WarehouseCode);
                   
                    List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_NKDC(No, WarehouseCode);
                    if (historys != null && historys.Count > 0)
                        return true;
                    else
                        return false;
                }
                else if (MySettings.Index_Page == 2)
                {
                    List<SaleOrderItemScanBPL> donhang_ = App.Dblocal.GetSaleOrderItemScanAsyncWithKey(No, WarehouseCode);
                    
                    List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_XKDC(No, WarehouseCode);
                    if (historys != null && historys.Count > 0)
                        return true;
                    else
                        return false;
                }
                else if (MySettings.Index_Page == 3)
                {
                    List<ChuyenKhoDungCuModelBPL> donhang_ = App.Dblocal.GetTransferInstructionAsyncWithKey(No, WarehouseCode, WarehouseCode_To);

                    List<TransactionHistoryModel> historys = App.Dblocal.GetAllHistory_CKDC(No, WarehouseCode, WarehouseCode_To);

                    if (historys != null && historys.Count > 0)
                        return true;
                    else
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true; 
            }

        }
    }
}
