using QRMS.Helper;
using Xamarin.Forms;

namespace QRMS.Controls
{
    public class CustomEntry : Entry
    {
        public CustomEntry(){
            this.IsSpellCheckEnabled = false;
        }
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(CustomEntry), 0);
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(CustomEntry), 0);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry), Color.Transparent);
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(CustomEntry), new Thickness());

        public static readonly BindableProperty TTSpecicalCharProperty = BindableProperty.Create(nameof(TTSpecicalChar), typeof(int), typeof(CustomEntry), 0);

        public int TTSpecicalChar
        {
            get { return (int)GetValue(TTSpecicalCharProperty); }
            set { SetValue(TTSpecicalCharProperty, value); }
        }

        public static readonly BindableProperty IsFormatDecimalProperty = BindableProperty.Create(nameof(IsFormatDecimal), typeof(bool), typeof(CustomEntry), false);

        public bool IsFormatDecimal
        {
            get { return (bool)GetValue(IsFormatDecimalProperty); }
            set { SetValue(IsFormatDecimalProperty, value); }
        }


        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public int BorderThickness
        {
            get { return (int)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        } 
        protected override void OnTextChanged(string oldValue, string newValue)
        {  
            if (IsFormatDecimal)
            {
                Text = StringUtils.FormatDecimal(Text);
                //if (decimal.TryParse(this.Text, out decimal result) && result.ToString() != Text)
                //{
                //    //Text = string.Format(CultureInfo.InvariantCulture, "{0:#,##0}", result); 
                //}
            }
            else
            {
                string str = "";
                if (!string.IsNullOrWhiteSpace(newValue))
                {
                    if (TTSpecicalChar == 0)
                    {//Ho ten
                        if (newValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(newValue[newValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckHoTen(newValue[newValue.Length - 1].ToString()))
                                {
                                    str = newValue;
                                }
                                else
                                {
                                    str = newValue.Remove(newValue.Length - 1);
                                }
                            }
                            else
                            { str = newValue; }

                            Text = str; 
                        }
                    }
                    else if (TTSpecicalChar == 1)
                    {//DiaChi
                        if (newValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(newValue[newValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckDiaChi(newValue[newValue.Length - 1].ToString()))
                                {
                                    str = newValue;
                                }
                                else
                                {
                                    str = newValue.Remove(newValue.Length - 1);
                                }
                            }
                            else
                            { str = newValue; }

                            Text = str; 
                        }
                    }
                    else if (TTSpecicalChar == 10)
                    {//CMT
                        if (newValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(newValue[newValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                str = newValue.Remove(newValue.Length - 1);
                            }
                            else
                            { str = newValue; }

                            Text = str; 
                        }
                    }
                    else if (TTSpecicalChar == 11)
                    {//MST
                        if (newValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(newValue[newValue.Length - 1].ToString(), "[0-9]"))
                            {
                                if (!StringUtils.IsCheckMST(newValue[newValue.Length - 1].ToString()))
                                {
                                    str = newValue;
                                }
                                else
                                {
                                    str = newValue.Remove(newValue.Length - 1);
                                }
                            }
                            else
                            { str = newValue; }

                            Text = str; 
                        }
                    }
                    else if (TTSpecicalChar == 12)
                    {//MAIL
                        if (newValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(newValue[newValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckEMAIL(newValue[newValue.Length - 1].ToString()))
                                {
                                    str = newValue;
                                }
                                else
                                {
                                    str = newValue.Remove(newValue.Length - 1);
                                }
                            }
                            else
                            { str = newValue; }

                            Text = str; 

                        }
                    }
                    else if (TTSpecicalChar == 15)
                    {//TenNganHang
                        if (newValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(newValue[newValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckTenNganHang(newValue[newValue.Length - 1].ToString()))
                                {
                                    str = newValue;
                                }
                                else
                                {
                                    str = newValue.Remove(newValue.Length - 1);
                                }
                            }
                            else
                            { str = newValue; }

                            Text = str; 
                        }
                    }
                    else if (TTSpecicalChar == 16)
                    {//Tra cứu GCN
                        if (newValue.Length > 0)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(newValue[newValue.Length - 1].ToString(), "[a-zA-Z0-9]"))
                            {
                                if (!StringUtils.IsCheckGCN(newValue[newValue.Length - 1].ToString()))
                                {
                                    str = newValue;
                                }
                                else
                                {
                                    str = newValue.Remove(newValue.Length - 1);
                                }
                            }
                            else
                            {
                                str = newValue;
                            } 
                            Text = str; 
                        }
                    } 
                }
            }
        }
    }
}