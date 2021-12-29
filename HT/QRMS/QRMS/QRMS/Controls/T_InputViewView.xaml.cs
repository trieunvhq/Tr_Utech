 
using QRMS.Constants;
using QRMS.Helper;
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class T_InputViewView : StackLayout
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(T_InputViewView), string.Empty, BindingMode.TwoWay);

        public Color BackgroundEntryColor
        {
            set
            {
                inputEntry.BackgroundColor = value;
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty)?.ToString(); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly BindableProperty ImgProperty = BindableProperty.Create(nameof(Img), typeof(string), typeof(T_InputViewView), null, BindingMode.TwoWay);

        public string Img
        {
            get { return (string)GetValue(ImgProperty); }
            set { SetValue(ImgProperty, value); }
        }
        public static readonly BindableProperty IsBBProperty = BindableProperty.Create(nameof(IsBB), typeof(bool), typeof(T_InputViewView), true, BindingMode.TwoWay);

        public bool IsBB
        {
            get { return (bool)GetValue(IsBBProperty); }
            set { SetValue(IsBBProperty, value); }
        }
        public string Title
        {
            set
            {
                lbTiitle.Text = value;
                lbTiitle2.Text = " *";
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

        public int MaxLength
        {
            set
            {
                inputEntry.MaxLength = value;
            }
        }

        public EventHandler<TextChangedEventArgs> TextChanged
        {
            set
            {
                inputEntry.TextChanged += value;
            }
        } 
        public bool IsFormatDecimal { get; set; }
        public bool IsForceDot { get; set; }

        public bool IsError { get; set; }
        public event EventHandler Focused;
        public event EventHandler Unfocused;

        public T_InputViewView()
        {
            InitializeComponent();
            inputEntry.TextColor = Color.Black;
            //inputEntry.TextChanged += (o, e) =>
            //{
            //    if (IsForceDot)
            //    {
            //        var text = inputEntry.Text.Replace(',', '.');
            //        if (text != inputEntry.Text) inputEntry.Text = text;
            //    }
            //};
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TextProperty.PropertyName)
            {
                if (IsFormatDecimal)
                    inputEntry.Text = StringUtils.FormatDecimal(Text);
                else
                    inputEntry.Text = Text;
            }
            else if (propertyName == nameof(IsError))
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
            else if (propertyName == nameof(IsEnabled))
            {
                inputEntry.IsEnabled = IsEnabled;
            }
            else if (propertyName == nameof(Img))
            {
                img.Source = Img;
            }
            else if (propertyName == TextProperty.PropertyName)
            {
                IsError = false;
            }
            else if (propertyName == IsBBProperty.PropertyName)
            {
                if (IsBB)
                {
                    lbTiitle2.Text = " *";
                }
                else
                {
                    lbTiitle2.Text = "";
                }
            } 
        }

        void OnEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        { 
            IsError = false;
            Focused?.Invoke(this, EventArgs.Empty);
        }

        void OnEntryUnfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //Text = Text?.Trim();
            if (IsFormatDecimal)
            {
                inputEntry.Text = StringUtils.FormatDecimal(inputEntry.Text);
                Text = inputEntry.Text;
            }
            else
            {
                Text = inputEntry.Text;
            }    
            Unfocused?.Invoke(this, EventArgs.Empty);
        }
        private int tt = 0;
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            inputEntry.TextChanged -= OnEntryTextChanged;
            string str = "";
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                if (e.NewTextValue.Length > 0)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue[e.NewTextValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                    {
                        if (!StringUtils.IsCheckDiaChi(e.NewTextValue[e.NewTextValue.Length - 1].ToString()))
                        {
                            str = e.NewTextValue;
                        }
                        else
                        {
                            str = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                        }
                    }
                    else
                    { str = e.NewTextValue; }

                    inputEntry.Text = str;
                    Text = str;

                }

                //bool isValid = e.NewTextValue.ToCharArray().All(x => !SpecialCharacters.Contains(x));
                //str = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }

            inputEntry.TextChanged += OnEntryTextChanged;
        }
    }
}