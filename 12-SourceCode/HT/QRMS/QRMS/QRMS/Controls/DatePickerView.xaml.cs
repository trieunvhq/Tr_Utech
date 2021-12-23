using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class DatePickerView : StackLayout
    {
        //    public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DatePickerView), DatePicker.DateProperty.DefaultValue, BindingMode.TwoWay);
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

        public string Title
        {
            set
            {
                var formattedString = new FormattedString();
                formattedString.Spans.Add(new Span { Text = value });
                formattedString.Spans.Add(new Span { Text = " *", TextColor = Color.FromHex("#D31A1F") });
                lblTitle.FormattedText = formattedString;
            }
        }

        public string ErrorMessage
        {
            set
            {
                lblError.Text = value;
            }
        }

        public bool IsError { get; set; }

        public ImageSource Icon
        {
            set
            {
                imgIcon.Source = value;
            }
        }

        public string Format
        {
            set
            {
                inputDatePicker.Format = value;
            }
        }

        public DatePickerView()
        {
            InitializeComponent();
            inputDatePicker.TextColor = Color.Black;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(IsError))
            {
                lblError.IsVisible = IsError;
                if (IsError)
                {
                    borderView.BorderColor = Color.FromHex("#F5323C");
                    container.BackgroundColor = Color.FromHex("#FEEEEF");
                }
                else
                {
                    borderView.BorderColor = inputDatePicker.IsFocused ? Color.FromHex("#F49A0E") : Color.FromHex("#DCE0E2");
                    container.BackgroundColor = Color.FromHex("#FFFFFF");
                }
            }
            else if (propertyName == DateProperty.PropertyName)
            {
                IsError = false;
            }
        }

        private void OnDatePickerFocused(object sender, FocusEventArgs e)
        {
            IsError = false;
        }

        private void OnPickerImageTapped(object sender, EventArgs e)
        {
            inputDatePicker.Focus();
        }
    }
}