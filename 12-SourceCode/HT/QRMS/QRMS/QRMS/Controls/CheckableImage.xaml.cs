using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRMS.Controls
{
    public partial class CheckableImage : StackLayout
    {
        private readonly ImageSource _uncheckImage;
        private readonly ImageSource _checkedImage;

        public static readonly BindableProperty IsCheckedProperty = 
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckableImage), false, BindingMode.TwoWay);
        public static readonly BindableProperty TextProperty = 
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckableImage),string.Empty, BindingMode.TwoWay);

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
                checkableImage.HeightRequest = checkableImage.WidthRequest = value;
            }
        }
        public double BoxHeight
        {
            set
            {
                checkableImage.HeightRequest = value;
            }
        }
        public double BoxWidth
        {
            set
            {
                checkableImage.WidthRequest = value;
            }
        }
        public bool IsVer
        {
            set
            {
                if (value)
                    checkableImage.VerticalOptions = LayoutOptions.Start;
            }
        }
        public event EventHandler<bool> CheckedChanged;
        private readonly ICommand _handleCheckedChangeCommand;
        private bool _tapped;

        public CheckableImage()
        {
            InitializeComponent();
        }

        public CheckableImage(string checkedImage, string uncheckImage)
        {
            InitializeComponent();
            _checkedImage = checkedImage;
            _uncheckImage = uncheckImage;

            // Standard status
            checkableImage.Source = _uncheckImage;

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
                checkableImage.Source = _checkedImage;
            }
            else
            {
                checkableImage.Source = _uncheckImage;
            }
        }

        protected virtual void OnTap(CheckableImage checkableImage, object obj)
        {
            IsChecked = !IsChecked;
        }
    }
}