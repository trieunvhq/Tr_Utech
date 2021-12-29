using System;
using System.Runtime.CompilerServices;
using QRMS.Resources;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class DateCustomView : Grid
    {
        private bool isload = false;
        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DatePickerView), DateTime.Now.Date, BindingMode.TwoWay);
        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(DatePickerView), DatePicker.MinimumDateProperty.DefaultValue, BindingMode.TwoWay);
        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(DatePickerView), DatePicker.MaximumDateProperty.DefaultValue, BindingMode.TwoWay);
        public static readonly BindableProperty IsBBProperty = BindableProperty.Create(nameof(IsBB), typeof(bool), typeof(DatePickerView), true, BindingMode.TwoWay);

        //
        //public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder", returnType: typeof(string), declaringType: typeof(DateCustomView), defaultValue: default(string));
        //public string Placeholder { get; set; }
        //

        public bool IsBB
        {
            get { return (bool)GetValue(IsBBProperty); }
            set { SetValue(IsBBProperty, value); }
        }
        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set
            {
                SetValue(DateProperty, value);
                datepicker.Date = value;
                if (_IsMin == 1)
                {
                    if (inputEntry.Text != value.ToString("HH:mm  dd/MM/yyyy"))
                    { inputEntry.Text = value.ToString("HH:mm  dd/MM/yyyy"); }

                }
                else if (_IsMin == 2)
                {
                    if (inputEntry.Text != value.ToString("dd/MM/yyyy"))
                    { inputEntry.Text = value.ToString("dd/MM/yyyy"); }
                }
                else if (_IsMin == 3)
                {
                    if (inputEntry.Text != value.ToString("HH:mm"))
                    { inputEntry.Text = value.ToString("HH:mm"); }
                }
            }
        }
        public DateTime MinimumDate
        {
            get { return (DateTime)GetValue(MinimumDateProperty); }
            set
            {
                SetValue(MinimumDateProperty, value);
            }
        }
        public DateTime MaximumDate
        {
            get { return (DateTime)GetValue(MaximumDateProperty); }
            set
            {
                SetValue(MaximumDateProperty, value);
            }
        }

        // 
        private string _ImgName = "";
        public string ImgName
        {
            set
            {
                _ImgName = value;
            }
        }

        //
        private bool _ChangeIcon = false;
        public bool ChangeIcon
        {
            set
            {
                _ChangeIcon = value;
            }
        }

        //
        private int _IsMin = 0;
        public int IsMin
        {
            set
            {
                _IsMin = value;
                if (_IsMin == 1)
                {
                    inputEntry.Placeholder = "HH:MM  DD/MM/YYYY";
                    img.Source = "icon_date_empty.png";
                }
                else if (_IsMin == 2)
                {
                    if (_ChangeIcon && _ImgName != "")
                    {
                        inputEntry.Placeholder = AppResources.NgayThangNam;
                        root.Children.Remove(timepicker);
                        img.Source = _ImgName;
                    }
                    else
                    {
                        inputEntry.Placeholder = AppResources.NgayThangNam;
                        root.Children.Remove(timepicker);
                        img.Source = "icon_date_empty.png";
                    }

                }
                else if (_IsMin == 3)
                {
                    if (_ChangeIcon && _ImgName != "")
                    {
                        inputEntry.Placeholder = "HH:MM";
                        root.Children.Remove(datepicker);
                        img.Source = _ImgName;
                    }
                    else
                    {
                        inputEntry.Placeholder = "HH:MM";
                        root.Children.Remove(datepicker);
                        img.Source = "icon_time.png";
                    }
                }
            }
        }
        public string Title
        {
            set
            {
                if (value.Length > 0)
                {
                    row.Height = 20;
                }
                else
                {
                    row.Height = 0;
                }
                lblTitle.Text = value;
                lblTitle2.Text = " *";
            }
        }

        public string ErrorMessage
        {
            set
            {
                lblError.Text = value;
            }
        }

        public string Placeholder
        {
            set
            {
                inputEntry.Placeholder = value;
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

        public DateCustomView()
        {
            isload = true;
            InitializeComponent();
            inputEntry.TextColor = Color.Black;
            isload = false;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            isload = true;
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
                    inputEntry.BorderColor = inputEntry.IsFocused ? Color.FromHex("#F49A0E") : Color.FromHex("#DCE0E2");
                    inputEntry.BackgroundColor = Color.FromHex("#FFFFFF");
                }

            }
            else if (propertyName == nameof(Date))
            {
                datepicker.Date = Date;     
            }
            else if (propertyName == nameof(IsEnabled))
            {
                if (IsEnabled)
                {
                    inputEntry.BackgroundColor = Color.FromHex("#ffffff");
                }
                else
                {
                    inputEntry.BackgroundColor = Color.FromHex("#EDEFF1");
                }
            }
            else if (propertyName == IsBBProperty.PropertyName)
            {
                if (IsBB)
                {
                    lblTitle2.Text = " *";
                }
                else
                {
                    lblTitle2.Text = "";
                }
            }
            //
            if (_IsMin == 1)
            {
                if (Date != null && inputEntry.Text != Date.ToString("HH:mm  dd/MM/yyyy"))
                {
                    inputEntry.Text = Date.ToString("HH:mm  dd/MM/yyyy");
                }
            }
            else if (_IsMin == 2)
            {
                if (Date != null && inputEntry.Text != Date.ToString("dd/MM/yyyy"))
                {
                    inputEntry.Text = Date.ToString("dd/MM/yyyy");
                }
            }
            else if (_IsMin == 3)
            {
                if (Date != null && inputEntry.Text != Date.ToString("HH:mm"))
                {
                    inputEntry.Text = Date.ToString("HH:mm");
                }
            } 
            //
            if (datepicker != null)
            {
                if ((MinimumDate != null && datepicker.MinimumDate == null) || datepicker.MinimumDate.Date != MinimumDate.Date)
                {
                    if (_IsMin == 1)
                    {
                        datepicker.MinimumDate = MinimumDate;
                    }
                    else if (_IsMin == 2)
                    { 
                        datepicker.MinimumDate = MinimumDate;
                    }
                    else if (_IsMin == 3)
                    {
                    }
                }
                //
                if ((MaximumDate != null && datepicker.MaximumDate == null) || datepicker.MaximumDate.Date != MaximumDate.Date)
                {
                    if (_IsMin == 1)
                    {
                        datepicker.MaximumDate = MaximumDate;
                    }
                    else if (_IsMin == 2)
                    {
                        datepicker.MaximumDate = MaximumDate;
                    }
                    else if (_IsMin == 3)
                    {
                    }
                }
            }
            isload = false;
        }

        void OnEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            IsError = false;
        }

        void DatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            if (isload)
                return;
            if (_IsMin == 1)
            {
                Date = datepicker.Date + timepicker.Time;
            }
            else if (_IsMin == 2)
            {
                Date = datepicker.Date;
            }
            else if (_IsMin == 3)
            {
                Date = new DateTime(2021, 1, 1) + timepicker.Time;
            }
        }

        void timepicker_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (isload)
                return;
        }

        void timepicker_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (isload)
                return;
            if (_IsMin == 1 || _IsMin == 3)
            {
                Date = new DateTime(2021, 1, 1) + timepicker.Time;
            }
            else if (_IsMin == 2)
            {
                datepicker.Focus();
            }
            IsError = false;
            inputEntry.BorderColor = inputEntry.IsFocused ? Color.FromHex("#F49A0E") : Color.FromHex("#DCE0E2");
            inputEntry.BackgroundColor = Color.FromHex("#FFFFFF");
            lblError.IsVisible = IsError;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (_IsMin == 1)
            {
                timepicker.Focus();
            }
            else if (_IsMin == 2)
            {
                datepicker.Focus();
            }
            else if (_IsMin == 3)
            {
                timepicker.Focus();
            }
            else
            {
                datepicker.Focus();
            }    
        }

        void datepicker_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (isload)
                return;
            if (_IsMin == 1)
            {
                Date = (datepicker.Date + timepicker.Time);
            }
            else if (_IsMin == 2)
            {
                Date = datepicker.Date;
            }
            else if (_IsMin == 3)
            {
                Date = new DateTime(2021, 1, 1) + timepicker.Time;
            }
            IsError = false;
            inputEntry.BorderColor = inputEntry.IsFocused ? Color.FromHex("#F49A0E") : Color.FromHex("#DCE0E2");
            inputEntry.BackgroundColor = Color.FromHex("#FFFFFF");
            lblError.IsVisible = IsError;
        }
    }
}