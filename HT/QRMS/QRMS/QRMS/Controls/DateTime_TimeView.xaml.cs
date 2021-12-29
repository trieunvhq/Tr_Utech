
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class DateTime_TimeView : Grid
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(DateTime_TimeView), string.Empty, BindingMode.TwoWay);

        public string Text
        {
            get { return GetValue(TextProperty)?.ToString(); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(DateTime_TimeView), TimeSpan.FromSeconds(0), BindingMode.TwoWay);

        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public string ErrorMessage
        {
            set
            {
                lblError.Text = value;
            }
        }
         

        public Keyboard Keyboard
        {
            set
            {
                inputEntry.Keyboard = value;
            }
        }

        public bool IsError { get; set; }

        public DateTime_TimeView()
        {
            InitializeComponent();
            inputEntry.TextColor = Color.Black;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(IsError))
            {
                lblError.IsVisible = IsError;
                if (IsError)
                {
                    inputEntry.BorderColor = Color.FromHex("#F5323C");
                    inputEntry.BackgroundColor = Color.FromHex("#FEEEEF");
                }
                else
                {
                    inputEntry.BackgroundColor = Color.FromHex("#FFFFFF");
                }
            }
        }

        void OnEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            IsError = false;
        }
         

        void timepicker_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Time = timepicker.Time;
        }

        void timepicker_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Text = timepicker.Time.ToString();
            Time = timepicker.Time;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            timepicker.Focus();
        } 
    }
}