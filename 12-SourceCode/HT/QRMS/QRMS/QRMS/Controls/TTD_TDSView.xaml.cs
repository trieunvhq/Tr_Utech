 
using System;
using System.Windows.Input;
using QRMS.Resources;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class TTD_TDSView : Grid
    {  
       
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty TrangThaiProperty =
            BindableProperty.Create(nameof(TrangThai), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty SoGiayChungNhanProperty =
            BindableProperty.Create(nameof(SoGiayChungNhan), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty NgayCapProperty =
            BindableProperty.Create(nameof(NgayCap), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty NgayHieuLucProperty =
            BindableProperty.Create(nameof(NgayHieuLuc), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty NgayKetThucProperty =
            BindableProperty.Create(nameof(NgayKetThuc), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty HoVaTenKhachHangProperty =
            BindableProperty.Create(nameof(HoVaTenKhachHang), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty SoCMTCCCDHCProperty =
           BindableProperty.Create(nameof(SoCMTCCCDHC), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty SoDienThoaiProperty =
            BindableProperty.Create(nameof(SoDienThoai), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty EmailProperty =
            BindableProperty.Create(nameof(Email), typeof(string), typeof(TTD_TDSView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty MyColorProperty =
            BindableProperty.Create(nameof(MyColor), typeof(Color), typeof(TTD_TDSView), null, BindingMode.TwoWay);

        public static readonly BindableProperty LoaiProperty =
               BindableProperty.Create(nameof(Loai), typeof(string), typeof(TTD_TDSView), "SDBS", BindingMode.TwoWay);


        public static readonly BindableProperty LichSuProperty =
               BindableProperty.Create(nameof(LichSu), typeof(bool), typeof(TTD_TDSView), false, BindingMode.TwoWay);


        public bool LichSu
        {
            get { return (bool)GetValue(LichSuProperty); }
            set { SetValue(LichSuProperty, value); }
        }

        public string Loai
        {
            get { return (string)GetValue(LoaiProperty); }
            set { SetValue(LoaiProperty, value); }
        }
        public Color MyColor
        {
            get { return (Color)GetValue(MyColorProperty); }
            set { SetValue(MyColorProperty, value); }
        }
        public string Title
        {
            get { return GetValue(TitleProperty)?.ToString(); }
            set { SetValue(TitleProperty, value); }
        }
        public string TrangThai
        {
            get { return GetValue(TrangThaiProperty)?.ToString(); }
            set { SetValue(TrangThaiProperty, value); }
        }

        public string SoGiayChungNhan
        {
            get { return GetValue(SoGiayChungNhanProperty)?.ToString(); }
            set { SetValue(SoGiayChungNhanProperty, value); }
        }
        public string NgayCap
        {
            get { return GetValue(NgayCapProperty)?.ToString(); }
            set { SetValue(NgayCapProperty, value); }
        }
        public string NgayHieuLuc
        {
            get { return GetValue(NgayHieuLucProperty)?.ToString(); }
            set { SetValue(NgayHieuLucProperty, value); }
        }

        public string NgayKetThuc
        {
            get { return GetValue(NgayKetThucProperty)?.ToString(); }
            set { SetValue(NgayKetThucProperty, value); }
        }
        public string HoVaTenKhachHang
        {
            get { return GetValue(HoVaTenKhachHangProperty)?.ToString(); }
            set { SetValue(HoVaTenKhachHangProperty, value); }
        }

        //
        public string SoCMTCCCDHC
        {
            get { return GetValue(SoCMTCCCDHCProperty)?.ToString(); }
            set { SetValue(SoCMTCCCDHCProperty, value); }
        }

        public string SoDienThoai
        {
            get { return GetValue(SoDienThoaiProperty)?.ToString(); }
            set { SetValue(SoDienThoaiProperty, value); }
        }
        public string Email
        {
            get { return GetValue(EmailProperty)?.ToString(); }
            set { SetValue(EmailProperty, value); }
        }
        public event EventHandler ClickedGCN;
        public event EventHandler ClickedOpen; 
        public TTD_TDSView()
        {
            InitializeComponent();   
         
        }


        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (TitleProperty.PropertyName.Equals(propertyName))
            {
                lbTitle.Text = Title;
            }
            else if (TrangThaiProperty.PropertyName.Equals(propertyName))
            {
                lbTrangThai.Text = TrangThai;
            }
            else if (SoGiayChungNhanProperty.PropertyName.Equals(propertyName))
            {
                lbSoGiayChungNhan.Text = SoGiayChungNhan;
            }
            else if (NgayCapProperty.PropertyName.Equals(propertyName))
            {
                lbNgayCap.Text = NgayCap;
            }
            else if (NgayHieuLucProperty.PropertyName.Equals(propertyName))
            {
                lbNgayHieuLuc.Text = NgayHieuLuc;
            }
            else if (NgayKetThucProperty.PropertyName.Equals(propertyName))
            {
                lbNgayKetThuc.Text = NgayKetThuc;
            }
            else if (HoVaTenKhachHangProperty.PropertyName.Equals(propertyName))
            {
                lbHoVaTenKhachHang.Text = HoVaTenKhachHang;
            }
            else if (SoCMTCCCDHCProperty.PropertyName.Equals(propertyName))
            {
                lbSoCMTCCCDHC.Text = SoCMTCCCDHC;
            }
            else if (SoDienThoaiProperty.PropertyName.Equals(propertyName))
            {
                lbSoDienThoai.Text = SoDienThoai;
            }
            else if (EmailProperty.PropertyName.Equals(propertyName))
            {
                lbEmail.Text = Email;
            }
            else if (LoaiProperty.PropertyName.Equals(propertyName))
            {
                if(Loai=="SBDS")
                {
                    btnTiepTuc.Text = AppResources.YeuCauSuaDoi;
                }
                else if (Loai == "TT")
                {
                    btnTiepTuc.Text = AppResources.TaiTucDonBaoHiemNay;
                }
            }
            else if (LichSuProperty.PropertyName.Equals(propertyName))
            {
                if (LichSu)
                {
                    row1.Height = 0;
                    row2.Height = 0;
                    btnTiepTuc.IsVisible = false;
                }
                else 
                {
                    row1.Height = 1;
                    row2.Height = 50;
                    btnTiepTuc.IsVisible = true;
                }
            }
        }

         

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            //ClickedOpen?.Invoke(this, e);
        }

        void GCN_Tapped(System.Object sender, System.EventArgs e)
        {
            ClickedGCN?.Invoke(this, e);
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            ClickedOpen?.Invoke(this, e);
        }
    }
}