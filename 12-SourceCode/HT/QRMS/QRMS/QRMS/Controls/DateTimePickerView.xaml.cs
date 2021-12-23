using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class DateTimePickerView : StackLayout
    {
        //public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DatePickerView), DatePicker.DateProperty.DefaultValue, BindingMode.TwoWay);
        //public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(DatePickerView), TimePicker.TimeProperty.DefaultValue, BindingMode.TwoWay);
        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DatePickerView), DateTime.Now, BindingMode.TwoWay);
        public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(DatePickerView), new TimeSpan(), BindingMode.TwoWay);
        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(DateTimePickerView), DatePicker.MinimumDateProperty.DefaultValue, BindingMode.TwoWay);
        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(DateTimePickerView), DatePicker.MaximumDateProperty.DefaultValue, BindingMode.TwoWay);

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }
        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
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

        public bool IsEnabledTime
        {
            set
            {
                containerTime.IsEnabled = value;
            }
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

        public string Format
        {
            set
            {
                inputDatePicker.Format = value;
            }
        }

        public bool IsDisableTime
        {
            set
            {
                containerTime.IsEnabled = !value;
                containerTime.BackgroundColor = value ? Color.FromHex("#EDEFF1") : Color.FromHex("#FFFFFF");
            }
        }

        public event EventHandler<DateTime> OnDateChanged;

        public DateTimePickerView()
        {
            InitializeComponent();
            inputDatePicker.TextColor = Color.Black;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TimeProperty.PropertyName)
            {
                Date = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds);
            }
            else if (propertyName == DateProperty.PropertyName)
            {
                if (!inputDatePicker.IsFocused)
                {
                    var time = new TimeSpan(Date.Hour, Date.Minute, Date.Second);
                    if (Time != time)
                        Time = time;
                }
                OnDateChanged?.Invoke(this, new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds));
            }
            else if (propertyName == nameof(IsError))
            {
                lblError.IsVisible = IsError;
                if (IsError)
                {
                    borderViewDate.BorderColor = Color.FromHex("#F5323C");
                    containerDate.BackgroundColor = Color.FromHex("#FEEEEF");
                    borderViewTime.BorderColor = Color.FromHex("#F5323C");
                    containerTime.BackgroundColor = Color.FromHex("#FEEEEF");
                }
                else
                {
                    borderViewDate.BorderColor = inputDatePicker.IsFocused ? Color.FromHex("#F49A0E") : Color.FromHex("#DCE0E2");
                    containerDate.BackgroundColor = Color.FromHex("#FFFFFF");
                    borderViewTime.BorderColor = inputTimePicker.IsFocused ? Color.FromHex("#F49A0E") : Color.FromHex("#DCE0E2");
                    containerTime.BackgroundColor = Color.FromHex("#FFFFFF");
                }
            }
            else if (propertyName == DateProperty.PropertyName || propertyName == TimeProperty.PropertyName)
            {
                IsError = false;
            }
        }

        private void OnDatePickerFocused(object sender, FocusEventArgs e)
        {
            IsError = false;
        }

        private void OnDatePickerImageTapped(object sender, EventArgs e)
        {
            inputDatePicker.Focus();
        }

        private void OnTimePickerImageTapped(object sender, EventArgs e)
        {
            inputTimePicker.Focus();
        }
    }
}