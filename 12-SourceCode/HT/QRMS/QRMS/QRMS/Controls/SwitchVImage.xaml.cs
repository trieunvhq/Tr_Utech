 
using System; 
using System.Windows.Input;
using Xamarin.Forms; 

namespace QRMS.Controls
{
    public partial class SwitchVImage : StackLayout
    {
        private readonly ImageSource _uncheckImage;
        private readonly ImageSource _checkedImage;

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(SwitchVImage), false, BindingMode.TwoWay);
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(SwitchVImage), string.Empty, BindingMode.TwoWay);

        public bool IsChecked
        {

            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public string Text
        {
            get { return GetValue(TextProperty)?.ToString(); }
            set { SetValue(TextProperty, value); }
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
        private readonly ICommand _handleCheckedChangeCommand;
        private bool _tapped;

        public SwitchVImage()
        {
            InitializeComponent();
        }

        public SwitchVImage(string checkedImage, string uncheckImage)
        {
            InitializeComponent();
            _checkedImage = checkedImage;
            _uncheckImage = uncheckImage;

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
            else if (TextProperty.PropertyName.Equals(propertyName))
            {
                lblText.Text = Text;
            }
            else if (IsEnabledProperty.PropertyName.Equals(propertyName))
            {
                if (IsEnabled)
                {
                    lblText.TextColor = Color.FromHex("#1D1D1F");
                }
                else
                {
                    lblText.TextColor = Color.FromHex("#ACACB1");
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

        protected virtual void OnTap(SwitchVImage SwitchVImage, object obj)
        {
            IsChecked = !IsChecked;
        }
    }
}