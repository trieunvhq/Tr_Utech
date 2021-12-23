 
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class T_PickerView : StackLayout
    {
        
        public static readonly BindableProperty ImgProperty = BindableProperty.Create(nameof(Img), typeof(string), typeof(T_PickerView), null, BindingMode.TwoWay);

        public string Img
        {
            get { return (string)GetValue(ImgProperty); }
            set { SetValue(ImgProperty, value); }
        }
        
        public string Title
        {
            set
            {
                if (value == null || value == "")
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
            }
        }

        public event EventHandler Picker_Unfocused;
        public bool IsError { get; set; }

        public T_PickerView()
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
                    container.BackgroundColor = Color.FromHex("#FEEEEF");
                }
                else
                {
                    borderView.BorderColor = inputPicker.IsFocused ? Color.FromHex("#F49A0E") : Color.FromHex("#DCE0E2");
                    container.BackgroundColor = Color.FromHex("#FFFFFF");
                }
            }
            else if (propertyName == nameof(IsEnabled))
            {
                if (IsEnabled)
                {
                    container.BackgroundColor = Color.FromHex("#ffffff");
                }
                else
                {
                    container.BackgroundColor = Color.FromHex("#EDEFF1");
                }
            } 
            else if (propertyName == nameof(Img))
            {
                img.Source = Img;
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

        void inputPicker_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Picker_Unfocused?.Invoke(this, EventArgs.Empty);
        }
    }
}