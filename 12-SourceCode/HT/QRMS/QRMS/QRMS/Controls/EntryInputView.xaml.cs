using QRMS.Constants;
using QRMS.Helper;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public partial class EntryInputView : StackLayout
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryInputView), string.Empty, BindingMode.TwoWay);

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
        public static readonly BindableProperty IsTittleProperty = BindableProperty.Create(nameof(IsTittle), typeof(bool), typeof(EntryInputView), true, BindingMode.TwoWay);

        public bool IsTittle
        {
            get { return (bool)GetValue(IsTittleProperty); }
            set { SetValue(IsTittleProperty, value); }
        }

        public static readonly BindableProperty IsBBProperty = BindableProperty.Create(nameof(IsBB), typeof(bool), typeof(EntryInputView), true, BindingMode.TwoWay);

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
        public static readonly BindableProperty TTSpecicalCharProperty = BindableProperty.Create(nameof(TTSpecicalChar), typeof(int), typeof(EntryInputView), 0, BindingMode.TwoWay);

        public int TTSpecicalChar
        {
            get { return (int)GetValue(TTSpecicalCharProperty); }
            set { SetValue(TTSpecicalCharProperty, value); }
        }
        public static readonly BindableProperty IsFormatDecimalProperty = BindableProperty.Create(nameof(IsFormatDecimal), typeof(bool), typeof(EntryInputView), false, BindingMode.TwoWay);

        public bool IsFormatDecimal
        {
            get { return (bool)GetValue(IsFormatDecimalProperty); }
            set { SetValue(IsFormatDecimalProperty, value); }
        }  

        public bool IsError { get; set; }
        public event EventHandler Focused;
        public event EventHandler Unfocused;

        public EntryInputView()
        {
            InitializeComponent();
            inputEntry.TextColor = Color.Black;
            //inputEntry.TextChanged += (o, e) =>
            //{
            //    OnEntryTextChanged(o,e);
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
            else
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
            else if (propertyName == nameof(IsEnabled))
            {
                inputEntry.IsEnabled = IsEnabled;
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
            else if (propertyName == IsTittleProperty.PropertyName)
            {
                if (IsTittle)
                {
                    lbTiitle2.Text = " *";
                }
                else
                {
                    lbTiitle.Text = "";
                    lbTiitle2.Text = "";
                }
            }
            else if (propertyName == TTSpecicalCharProperty.PropertyName)
            {
                inputEntry.TTSpecicalChar = TTSpecicalChar;
            }
            else if (propertyName == IsFormatDecimalProperty.PropertyName)
            {
                inputEntry.IsFormatDecimal = IsFormatDecimal;
            }
        }
        private bool setBeha = false;
        void OnEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (!setBeha && !IsFormatDecimal)
            {
                setBeha = true;
                // inputEntry.Behaviors.Add(new SpecialCharactersValidationBehavior_TT(this));
            }
            IsError = false;
            Focused?.Invoke(this, EventArgs.Empty);
        }

        void OnEntryUnfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            inputEntry.TextChanged -= OnEntryTextChanged;

            string str = inputEntry.Text;
            if(str!=null && str.Length>0)
            {
                if (!IsFormatDecimal)
                {

                    if (TTSpecicalChar == 0)
                    {//Ho ten
                        for (int i = 0; i < str.Length; ++i)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckHoTen(str[i].ToString()))
                                {
                                }
                                else
                                {
                                    str = str.Remove(i, 1);
                                    --i;
                                }
                            }
                        }

                    }
                    else if (TTSpecicalChar == 1)
                    {//DiaChi
                        for (int i = 0; i < str.Length; ++i)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckDiaChi(str[i].ToString()))
                                {
                                }
                                else
                                {
                                    str = str.Remove(i, 1);
                                }
                            }
                        }
                    }
                    else if (TTSpecicalChar == 10)
                    {//CMT
                        for (int i = 0; i < str.Length; ++i)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[a-zA-Z0-9]"))
                            {
                                str = str.Remove(i, 1);
                            }
                        }
                    }
                    else if (TTSpecicalChar == 11)
                    {//MST
                        for (int i = 0; i < str.Length; ++i)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[0-9]"))
                            {
                                if (!StringUtils.IsCheckMST(str[i].ToString()))
                                {
                                }
                                else
                                {
                                    str = str.Remove(i, 1);
                                }
                            }
                        }
                    }
                    else if (TTSpecicalChar == 12)
                    {//MAIL
                        for (int i = 0; i < str.Length; ++i)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckEMAIL(str[i].ToString()))
                                {
                                }
                                else
                                {
                                    str = str.Remove(i, 1);
                                }
                            }
                        }
                    }
                    else if (TTSpecicalChar == 15)
                    {//TenNganHang
                        for (int i = 0; i < str.Length; ++i)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckTenNganHang(str[i].ToString()))
                                {
                                }
                                else
                                {
                                    str = str.Remove(i, 1);
                                }
                            }
                        }
                    }
                    else if (TTSpecicalChar == 16)
                    {//Tra cứu GCN
                        for (int i = 0; i < str.Length; ++i)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckGCN(str[i].ToString()))
                                {
                                }
                                else
                                {
                                    str = str.Remove(i, 1);
                                }
                            }
                        }
                    }
                }
            }    
            inputEntry.Text = str;
            Text = inputEntry.Text;
            Text = Text?.Trim();
            if (IsFormatDecimal)
            {
                inputEntry.Text = StringUtils.FormatDecimal(inputEntry.Text);
            } 
            Unfocused?.Invoke(this, EventArgs.Empty);
            inputEntry.TextChanged += OnEntryTextChanged;
        }

        private int tt = 0;
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            inputEntry.TextChanged -= OnEntryTextChanged;
            if (IsFormatDecimal)
            {
                inputEntry.Text = StringUtils.FormatDecimal(inputEntry.Text);
                //if (decimal.TryParse(inputEntry.Text, out decimal result) && result.ToString() != Text)
                //{
                //    inputEntry.Text = string.Format(CultureInfo.InvariantCulture, "{0:#,##0}", result);
                //    Text = inputEntry.Text;
                //}
            }
            else
            {
                string str = "";
                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    if (TTSpecicalChar == 0)
                    {//Ho ten
                        if (e.NewTextValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue[e.NewTextValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckHoTen(e.NewTextValue[e.NewTextValue.Length - 1].ToString()))
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
                    }
                    else if (TTSpecicalChar == 1)
                    {//DiaChi
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
                    }
                    else if (TTSpecicalChar == 10)
                    {//CMT
                        if (e.NewTextValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue[e.NewTextValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                str = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                            }
                            else
                            { str = e.NewTextValue; }

                            inputEntry.Text = str;
                            Text = str;

                        }
                    }
                    else if (TTSpecicalChar == 11)
                    {//MST
                        if (e.NewTextValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue[e.NewTextValue.Length - 1].ToString(), "[0-9]"))
                            {
                                if (!StringUtils.IsCheckMST(e.NewTextValue[e.NewTextValue.Length - 1].ToString()))
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
                    }
                    else if (TTSpecicalChar == 12)
                    {//MAIL
                        if (e.NewTextValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue[e.NewTextValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckEMAIL(e.NewTextValue[e.NewTextValue.Length - 1].ToString()))
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
                    }
                    else if (TTSpecicalChar == 15)
                    {//TenNganHang
                        if (e.NewTextValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue[e.NewTextValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckTenNganHang(e.NewTextValue[e.NewTextValue.Length - 1].ToString()))
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
                    } 
                    else if (TTSpecicalChar == 16)
                    {//Tra cứu GCN
                        if (e.NewTextValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(e.NewTextValue[e.NewTextValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckGCN(e.NewTextValue[e.NewTextValue.Length - 1].ToString()))
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
                    }
                }
            }
            inputEntry.TextChanged += OnEntryTextChanged;
        }
    }
}