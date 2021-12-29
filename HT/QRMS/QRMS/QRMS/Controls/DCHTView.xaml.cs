 
using System;
using System.Windows.Input;
using QRMS.Resources;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class DCHTView : Grid
    {

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(DCHTView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty TrangThaiProperty =
            BindableProperty.Create(nameof(TrangThai), typeof(string), typeof(DCHTView), string.Empty, BindingMode.TwoWay);
         
        public static readonly BindableProperty HoVaTenKhachHangProperty =
            BindableProperty.Create(nameof(HoVaTenKhachHang), typeof(string), typeof(DCHTView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty SoCMTCCCDHCProperty =
           BindableProperty.Create(nameof(SoCMTCCCDHC), typeof(string), typeof(DCHTView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty SoDienThoaiProperty =
            BindableProperty.Create(nameof(SoDienThoai), typeof(string), typeof(DCHTView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty EmailProperty =
            BindableProperty.Create(nameof(Email), typeof(string), typeof(DCHTView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty MyColorProperty =
            BindableProperty.Create(nameof(MyColor), typeof(Color), typeof(DCHTView), null, BindingMode.TwoWay);

       
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
        public event EventHandler ClickedOpen;
        public DCHTView()
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
        }



        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            //ClickedOpen?.Invoke(this, e);
        }
         
        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            ClickedOpen?.Invoke(this, e);
        }
    }
}