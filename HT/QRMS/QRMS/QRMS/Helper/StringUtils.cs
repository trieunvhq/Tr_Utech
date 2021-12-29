using QRMS.Resources;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace QRMS.Helper
{
    public static class StringUtils
    {
        public static string ClearSpecial(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            while ((str.Length > 0) && (str[str.Length - 1] == ',' || str[str.Length - 1] == '.'))
            {
                str = str.Remove(str.Length - 1);
            }
            while ((str.Length > 0) && (str[0] == ',' || str[0] == '.'))
            {
                str = str.Remove(0);
            }

            while (str.Contains(",,") || str.Contains(",,") || str.Contains(".,") || str.Contains(",."))
            {
                str = str.Replace(",,", "").Replace("..", "").Replace(".,", "").Replace(",.", "");
            }
            return str;
        }
        public static string FormatCurrency(decimal? value)
        {
            if (value == null)
                return null;
            return string.Format(CultureInfo.InvariantCulture, "{0:#,##0}", (decimal)value) + " " + AppResources.CommonUnit;
        }

        public static string FormatCurrency(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            return string.Format(CultureInfo.InvariantCulture, "{0:#,##0}", decimal.Parse(value)) + " " + AppResources.CommonUnit;
        }

        internal static string FormatDecimal(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            string str_ = value;
            try { str_ = string.Format(CultureInfo.InvariantCulture, "{0:#,##0}", decimal.Parse(value)); }
            catch {
                //str_ = "0";
                }
            return str_;
        }
        public static bool IsSpecial(int tt,string str)
        {
            bool kq_ = false;
            switch(tt)
            {
                case 1:
                    return (Regex.IsMatch(str, "[a-zA-Z0-9]+", RegexOptions.IgnoreCase));  
                case 2:
                    break;
                case 3:
                    break;
            }
            return kq_;
        }
        public static string DecimalToStringMoney(Nullable<decimal> sotien)
        {
            if (sotien != null && sotien > 0)
            {
                if (sotien < 1000)
                {
                    return (int)(sotien) + "đ";
                }
                else if (sotien >= 1000 && sotien < 1000000)
                {
                    return (int)(sotien / 1000) + "K";
                }
                else if (sotien >= 1000000 && sotien < 1000000000)
                {
                    return (int)(sotien / 1000000) + "M";
                }
                else
                {
                    return (int)(sotien / 1000000000) + "T";
                }
            }
            return "";
        }
        private static readonly string VietNamChar
            = " aAeEoOuUiIdDyYáàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄ" +
            "óòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ";

        public static bool IsCheckHoTen(string str)
        {
            bool ishople = true;
            for (int i = 0; i < VietNamChar.Length; i++)
            {
                if(VietNamChar[i].ToString()== str)
                    ishople = false;
            }
            return ishople;
        }

        private static readonly string VietNamChar_DiaChi
            = ",/- aAeEoOuUiIdDyYáàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄ" +
            "óòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ";
        public static bool IsCheckDiaChi(string str)
        {
            bool ishople = true;
            for (int i = 0; i < VietNamChar_DiaChi.Length; i++)
            {
                if (VietNamChar_DiaChi[i].ToString() == str)
                    ishople = false;
            }
            return ishople;
        }


        private static readonly string VietNamChar_TenNganHang
            = "/—- aAeEoOuUiIdDyYáàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄ" +
            "óòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ";
        public static bool IsCheckTenNganHang(string str)
        {
            bool ishople = true;
            for (int i = 0; i < VietNamChar_TenNganHang.Length; i++)
            {
                if (VietNamChar_TenNganHang[i].ToString() == str)
                    ishople = false;
            }
            return ishople;
        }
        private static readonly string VietNamChar_MST
            = "-";
        public static bool IsCheckMST(string str)
        {
            bool ishople = true;
            for (int i = 0; i < VietNamChar_MST.Length; i++)
            {
                if (VietNamChar_MST[i].ToString() == str)
                    ishople = false;
            }
            return ishople;
        }

        private static readonly string VietNamChar_EMAIL
            = "._@";
        public static bool IsCheckEMAIL(string str)
        {
            bool ishople = true;
            for (int i = 0; i < VietNamChar_EMAIL.Length; i++)
            {
                if (VietNamChar_EMAIL[i].ToString() == str)
                    ishople = false;
            }
            return ishople;
        }
        private static readonly string VietNamChar_GCN
           = "-/";
        public static bool IsCheckGCN(string str)
        {
            bool ishople = true;
            for (int i = 0; i < VietNamChar_GCN.Length; i++)
            {
                if (VietNamChar_GCN[i].ToString() == str)
                    ishople = false;
            }
            return ishople;
        }

    }
}