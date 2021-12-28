using System;
using System.Runtime.CompilerServices;
using QRMS.Helper;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class Input_PickerView : StackLayout
    {
        public decimal Value_decimal { get; set; }
         public string Text { get; set; }
        public static readonly BindableProperty sKeyBoardProperty = BindableProperty.Create(nameof(sKeyBoard), typeof(string), typeof(EntryInputView), string.Empty, BindingMode.TwoWay);
        public string sKeyBoard
        {
            get
            {
                return (string)GetValue(sKeyBoardProperty)?.ToString();
            }
            set
            {
                SetValue(sKeyBoardProperty, value);
                switch(value)
                {
                    case "Numeric":
                        inputEntry.Keyboard = Keyboard.Numeric;
                        break;
                    case "":
                        break;
                }    
            }
        }
        public string Title
        {
            set
            {
                if(value == null || value == "")
                { }
                else
                {
                    var formattedString = new FormattedString();
                    formattedString.Spans.Add(new Span { Text = value });
                    formattedString.Spans.Add(new Span { Text = " *", TextColor = Color.FromHex("#D31A1F") });
                    lblTitle.FormattedText = formattedString;
                }
            }
        }
        public bool IsTitleVisible
        {
            set
            {
                lblTitle.IsVisible = value;
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
                inputPicker.Title = value;
            }
        }

        public string ItemsSourceBinding
        {
            set
            {
                inputPicker.SetBinding(Picker.ItemsSourceProperty, value);
            }
        }

        public string SelectedItemBinding
        {
            set
            {
                inputPicker.SetBinding(Picker.SelectedItemProperty, value);
            }
        }

        public BindingBase ItemDisplayBinding
        {
            set
            {
                inputPicker.ItemDisplayBinding = value;
            }
        }

        public EventHandler SelectedIndexChanged
        {
            set
            {
                inputPicker.SelectedIndexChanged += value;
                inputEntry.Text = Text;
            }
        }

        public bool IsError { get; set; }

        public Input_PickerView()
        {
            InitializeComponent();
            inputPicker.TextColor = Color.Black;
            inputPicker.SelectedIndexChanged += (o, e) =>
            {
                IsError = false;
            };
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
                    inputEntry.BackgroundColor = Color.FromHex("#FEEEEF");
                }
                else
                {
                    borderView.BorderColor = inputPicker.IsFocused ? Color.FromHex("#F49A0E") : Color.FromHex("#DCE0E2");
                    inputEntry.BackgroundColor = Color.FromHex("#FFFFFF");
                }
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
            else if (propertyName == nameof(Text))
            {
                inputEntry.Text = Text;
            }
        }

        private void OnPickerFocused(object sender, FocusEventArgs e)
        {
            IsError = false;
        }

        private void OnPickerImageTapped(object sender, EventArgs e)
        {
            inputPicker.Focus();
        }
        void OnEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            IsError = false;
            Focused?.Invoke(this, EventArgs.Empty);
        }

        void OnEntryUnfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Text = Text?.Trim();
            if (IsFormatDecimal)
            {
                inputEntry.Text = StringUtils.FormatDecimal(inputEntry.Text);

            }    
            Unfocused?.Invoke(this, EventArgs.Empty);
        }

        public bool IsFormatDecimal { get; set; }
        public event EventHandler Unfocused;
        public event EventHandler Focused;
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {           
            if (IsFormatDecimal)
            {
                if (decimal.TryParse(inputEntry.Text, out decimal result) && result.ToString() != Text)
                {
                    Text = result.ToString();
                    Value_decimal = result;
                }
            }
            else
            {
            }
            if(inputPicker.SelectedItem != null)
            {
                if (Text == ((QRMS.Models.CommonValueModel)inputPicker.SelectedItem).VALUE_NAME)
                {

                }
                else
                {
                    inputPicker.SelectedItem = null;
                }
            }     
        }

        void inputPicker_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if(inputPicker.SelectedItem != null)
            {
                QRMS.Models.CommonValueModel temp1_ = (QRMS.Models.CommonValueModel)inputPicker.SelectedItem;
                inputEntry.Text = temp1_.VALUE_NAME;
                try
                {
                    Value_decimal = Convert.ToDecimal(temp1_.VALUE);
                }
                catch
                { Value_decimal = 0; }
                Text = inputEntry.Text;
                inputPicker.SelectedItem = temp1_;
            }    
        }
    }
}