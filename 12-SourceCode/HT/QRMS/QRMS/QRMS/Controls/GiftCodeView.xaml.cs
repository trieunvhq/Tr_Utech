
using System;
using System.Windows.Input;
using QRMS.Resources;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class GiftCodeView : Grid
    {
        private readonly ImageSource _uncheckImage;
        private readonly ImageSource _checkedImage;

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(GiftCodeView), false, BindingMode.TwoWay);

        public static readonly BindableProperty IsVisibleCheckedProperty =
            BindableProperty.Create(nameof(IsVisibleChecked), typeof(bool), typeof(GiftCodeView), true, BindingMode.TwoWay);

        public static readonly BindableProperty NAMEProperty =
            BindableProperty.Create(nameof(NAME), typeof(string), typeof(GiftCodeView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(GiftCodeView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty Text1Property =
            BindableProperty.Create(nameof(Text1), typeof(string), typeof(GiftCodeView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty Text2Property =
            BindableProperty.Create(nameof(Text2), typeof(string), typeof(GiftCodeView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty Text3Property =
            BindableProperty.Create(nameof(Text3), typeof(string), typeof(GiftCodeView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty Text4Property =
            BindableProperty.Create(nameof(Text4), typeof(string), typeof(GiftCodeView), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty MyColorProperty =
            BindableProperty.Create(nameof(MyColor), typeof(Color), typeof(GiftCodeView), null, BindingMode.TwoWay);
        public string NAME
        {
            get { return GetValue(NAMEProperty)?.ToString(); }
            set { SetValue(NAMEProperty, value); }
        }
        public bool IsVisibleChecked
        { 
            get { return (bool)GetValue(IsVisibleCheckedProperty); }
            set { SetValue(IsVisibleCheckedProperty, value); }
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
        public string Text
        {
            get { return GetValue(TextProperty)?.ToString(); }
            set { SetValue(TextProperty, value); }
        }
        public string Text1
        {
            get { return GetValue(Text1Property)?.ToString(); }
            set { SetValue(Text1Property, value); }
        }

        public string Text2
        {
            get { return GetValue(Text2Property)?.ToString(); }
            set { SetValue(Text2Property, value); }
        }
        public string Text3
        {
            get { return GetValue(Text3Property)?.ToString(); }
            set { SetValue(Text3Property, value); }
        }
        public string Text4
        {
            get { return GetValue(Text4Property)?.ToString(); }
            set { SetValue(Text4Property, value); }
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
        private readonly ICommand _handleCheckedChangeCommand;
        private bool _tapped;

        public string NoiDung;
        public int GITCODE_ID { get; set; } 
        public string GITCODE_CODE { get; set; }
        public string GITCODE_NAME { get; set; } 
        public Nullable<decimal> MAX_DISCOUNT { get; set; } 
        public Nullable<decimal> DISCOUNT_PERCENT { get; set; }
        public string GITCODE_COLOR { get; set; }
        public int GITCODE_COUNT { get; set; }
        public Nullable<int> GITCODE_USED_COUNT { get; set; }
        public Nullable<decimal> MIN_PRICE_ORDER { get; set; }
        public System.DateTime END_DATE { get; set; }

        public GiftCodeView()
        {
            InitializeComponent();
            _checkedImage = "giftcode_checked.png";
            _uncheckImage = "giftcode_unchecked.png";

            // Standard status
            switchImage.Source = _uncheckImage;

            GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command((obj) => OnTap(this, obj)) });
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
            else if (NAMEProperty.PropertyName.Equals(propertyName))
            {
                lbNen2.Text = NAME;
                lblText1.Text = AppResources.Giam + " " + NAME;
            }
            else if (Text1Property.PropertyName.Equals(propertyName))
            {
                //lblText1.Text = Text1;
            }
            else if (Text2Property.PropertyName.Equals(propertyName))
            {
                lblText2.Text = Text2;
            }
            else if (Text3Property.PropertyName.Equals(propertyName))
            {
                lblText3.Text = Text3;
            }
            else if (Text4Property.PropertyName.Equals(propertyName))
            {
                lblText4.Text = Text4;
            }
            else if (MyColorProperty.PropertyName.Equals(propertyName))
            {
                lbNen1.BackgroundColor = MyColor;
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
            else if (IsVisibleCheckedProperty.PropertyName.Equals(propertyName))
            {
                if (IsVisibleChecked)
                { 
                }
                else
                {
                    switchImage.IsVisible = false;
                    btnDieuKien.IsVisible = false;
                }
            }
        }

        private void ExecuteCheckedChangeCommand()
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

        protected virtual void OnTap(GiftCodeView GiftCodeView, object obj)
        {
            IsChecked = !IsChecked;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            ClickedOpen?.Invoke(this, e);
        }
    }
}