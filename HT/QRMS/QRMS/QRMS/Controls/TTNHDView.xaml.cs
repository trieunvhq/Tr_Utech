using System;
using System.Windows.Input;
using QRMS.Resources;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class TTNHDView : Grid
    {
        private readonly ImageSource _uncheckImage;
        private readonly ImageSource _checkedImage;

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(TTNHDView), false, BindingMode.TwoWay);

        public static readonly BindableProperty IsMacDinhProperty =
            BindableProperty.Create(nameof(IsMacDinh), typeof(bool), typeof(TTNHDView), true, BindingMode.TwoWay);

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(TTNHDView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty TenCongTyProperty =
            BindableProperty.Create(nameof(TenCongTy), typeof(string), typeof(TTNHDView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty MaSoThueProperty =
            BindableProperty.Create(nameof(MaSoThue), typeof(string), typeof(TTNHDView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty DiaChiCongTyProperty =
            BindableProperty.Create(nameof(DiaChiCongTy), typeof(string), typeof(TTNHDView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty DiaChiNhanHoaDonProperty =
            BindableProperty.Create(nameof(DiaChiNhanHoaDon), typeof(string), typeof(TTNHDView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty MyColorProperty =
            BindableProperty.Create(nameof(MyColor), typeof(Color), typeof(TTNHDView), null, BindingMode.TwoWay);

        public static readonly BindableProperty IsCTDProperty =
            BindableProperty.Create(nameof(IsCTD), typeof(bool), typeof(TTNHDView), false, BindingMode.TwoWay);

       

        public bool IsCTD
        {
            get { return (bool)GetValue(IsCTDProperty); }
            set { SetValue(IsCTDProperty, value); }
        }

        public bool IsMacDinh
        {
            get { return (bool)GetValue(IsMacDinhProperty); }
            set { SetValue(IsMacDinhProperty, value); }
        }
        public bool IsChecked
        {

            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public Color MyColor
        {
            get { return (Color)GetValue(MyColorProperty); }
            set { SetValue(MyColorProperty, value); }
        }
        public string Name
        {
            get { return GetValue(NameProperty)?.ToString(); }
            set { SetValue(NameProperty, value); }
        }
        public string TenCongTy
        {
            get { return GetValue(TenCongTyProperty)?.ToString(); }
            set { SetValue(TenCongTyProperty, value); }
        }

        public string MaSoThue
        {
            get { return GetValue(MaSoThueProperty)?.ToString(); }
            set { SetValue(MaSoThueProperty, value); }
        }
        public string DiaChiCongTy
        {
            get { return GetValue(DiaChiCongTyProperty)?.ToString(); }
            set { SetValue(DiaChiCongTyProperty, value); }
        }
        public string DiaChiNhanHoaDon
        {
            get { return GetValue(DiaChiNhanHoaDonProperty)?.ToString(); }
            set { SetValue(DiaChiNhanHoaDonProperty, value); }
        }
        public double Size
        {
            set
            {
                switchImage.HeightRequest = switchImage.WidthRequest = value;
            }
        }

        public bool IsVer
        {
            set
            {
                if (value)
                    switchImage.VerticalOptions = LayoutOptions.Start;
            }
        }
        public event EventHandler<bool> CheckedChanged;
        public event EventHandler ClickedOpen;
        public event EventHandler ClickedTittle;
        private readonly ICommand _handleCheckedChangeCommand;
        private bool _tapped;

        public int ID { get; set; }
        public int ACCOUNT_ID { get; set; }

        public TTNHDView()
        {
            InitializeComponent();
            _checkedImage = "giftcode_checked.png";
            _uncheckImage = "giftcode_unchecked.png";

            // Standard status
            if (IsCTD)
            {
                lbGach.IsVisible = false;
                switchImage.Source = "ico_next.png";
                switchImage.Margin = new Thickness(14, 7, 0, 7);
            }
            else
            {
                lbGach.IsVisible = true;
                switchImage.Source = _uncheckImage;
                switchImage.Margin = new Thickness(10, 5, 0, 5);
            }

            //GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command((obj) => OnTap(this, obj)) });
            _handleCheckedChangeCommand = new Command(() => ExecuteCheckedChangeCommand());
        }


        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (IsCheckedProperty.PropertyName.Equals(propertyName))
            { 
                if (_tapped != IsChecked)
                {
                    _tapped = IsChecked;
                    _handleCheckedChangeCommand.Execute(this);
                    CheckedChanged?.Invoke(this, IsChecked);
                }
            }
            else if (NameProperty.PropertyName.Equals(propertyName))
            {
                lbName.Text = Name;
            }
            else if (IsCTDProperty.PropertyName.Equals(propertyName))
            {
                if (IsCTD)
                {
                    lbGach.IsVisible = false;
                    switchImage.Source = "ico_next.png";
                    switchImage.Margin = new Thickness(14, 7, 0, 7);
                }
                else
                {
                    lbGach.IsVisible = true;
                    switchImage.Source = _uncheckImage;
                    switchImage.Margin = new Thickness(10, 5, 0, 5);
                }
            }
            else if (TenCongTyProperty.PropertyName.Equals(propertyName))
            {
                lbTenCongTy.Text = TenCongTy;
            }
            else if (MaSoThueProperty.PropertyName.Equals(propertyName))
            {
                lbMaSoThue.Text = MaSoThue;
            }
            else if (DiaChiCongTyProperty.PropertyName.Equals(propertyName))
            {
                lbDiaChiCongTy.Text = DiaChiCongTy;
            }
            else if (DiaChiNhanHoaDonProperty.PropertyName.Equals(propertyName))
            {
                lbDiaChiNhanHoaDon.Text = DiaChiNhanHoaDon;
            }
            else if (IsEnabledProperty.PropertyName.Equals(propertyName))
            {
                //if (IsEnabled)
                //{
                //    lblText.TextColor = Color.FromHex("#1D1D1F");
                //}
                //else
                //{
                //    lblText.TextColor = Color.FromHex("#ACACB1");
                //}
            }
            else if (IsMacDinhProperty.PropertyName.Equals(propertyName))
            {
                if (IsMacDinh)
                {
                    lbMacDinh.Text = AppResources.MacDinh;
                }
                else
                {
                    lbMacDinh.Text = "";
                }
            }
        }

        private void ExecuteCheckedChangeCommand()
        { 
            if (IsCTD)
            {
            }
            else
            {
                if (IsChecked)
                {
                    switchImage.Source = _checkedImage;
                }
                else
                {
                    switchImage.Source = _uncheckImage;
                }
            }
        }

        protected virtual void OnTap(TTNHDView TTNHDView, object obj)
        {
            //IsChecked = !IsChecked;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            ClickedOpen?.Invoke(this, e);
        }

        void Tittle_Tapped(System.Object sender, System.EventArgs e)
        {
            ClickedTittle?.Invoke(this, e);
        }


        void Image_Tapped(System.Object sender, System.EventArgs e)
        {
            if (IsCTD)
            {
            }
            else
            {
                return;
            }
            ClickedTittle?.Invoke(this, e);
        }

        void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
        }

        void SW_Tapped(System.Object sender, System.EventArgs e)
        {
            IsChecked = !IsChecked;
            IsChecked = !IsChecked;
        }
    }
}