
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class DateTime_DateCustomView : Grid
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(DateTime_DateCustomView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DatePickerView), DateTime.Now.Date, BindingMode.TwoWay);
        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(DatePickerView), DatePicker.MinimumDateProperty.DefaultValue, BindingMode.TwoWay);
        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(DatePickerView), DatePicker.MaximumDateProperty.DefaultValue, BindingMode.TwoWay);

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }
        public DateTime MinimumDate
        {
            get { return (DateTime)GetValue(MinimumDateProperty); }
            set { SetValue(MinimumDateProperty, value); }
        }
        public DateTime MaximumDate
        {
            get { return (DateTime)GetValue(MaximumDateProperty); }
            set { SetValue(MaximumDateProperty, value); }
        }


        public string Text
        {
            get { return GetValue(TextProperty)?.ToString(); }
            set { SetValue(TextProperty, value); }
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

        public DateTime_DateCustomView()
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

        void DatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            Text = datepicker.Date.ToString("dd/MM/yyyy");
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            datepicker.Focus();
        }

        void datepicker_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Text = datepicker.Date.ToString("dd/MM/yyyy");
        }
    }
}