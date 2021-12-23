using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace QRMS.AppLIB.Common
{
    public class MobileLib
    {
        //Check email
        public static bool IsEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email.Trim())) { return false; }
                //var addrEmail = new System.Net.Mail.MailAddress(email);
                //if (addrEmail.Host.Split('.').Length <= 1) { return false; }
                //return addrEmail.Address == email;
                var emailPattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-zA-Z]((\\.(?!\\.))|[-!#\\$%&\'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)" +
                    "(?<=[0-9a-zA-Z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";
                var str_ = "1234567890aqwertyuiopasdfghjklzxcvbnm.@_";
                char[] lst = email.ToCharArray();
                for(int i=0;i<lst.Length;++i)
                {
                    if(!str_.Contains(lst[i].ToString().ToLower()))
                    {
                        return false;
                    }
                }
                if (Regex.IsMatch(email, emailPattern))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool IsPhone(string str_)
        {
            try
            {
                if (str_.Length < 10 || str_.Length > 11)
                {
                    return false;
                }
                if (str_[0]!='0')
                {
                    return false;
                }
                try
                {
                    long sdt = Convert.ToInt64(str_);

                }
                catch
                {
                    return false;
                }
                return true;
                //if (str_.Substring(0, 3) == "086" || str_.Substring(0, 3) == "096" || str_.Substring(0, 3) == "097" ||
                //    str_.Substring(0, 3) == "033" || str_.Substring(0, 3) == "034" || str_.Substring(0, 3) == "035" ||
                //    str_.Substring(0, 3) == "038" || str_.Substring(0, 3) == "039" || str_.Substring(0, 3) == "089" ||
                //    str_.Substring(0, 3) == "070" || str_.Substring(0, 3) == "079" || str_.Substring(0, 3) == "077" ||
                //    str_.Substring(0, 3) == "088" || str_.Substring(0, 3) == "094" || str_.Substring(0, 3) == "098" ||
                //    str_.Substring(0, 3) == "032" || str_.Substring(0, 3) == "036" || str_.Substring(0, 3) == "037" ||
                //    str_.Substring(0, 3) == "083" || str_.Substring(0, 3) == "076" || str_.Substring(0, 3) == "078" ||
                //    str_.Substring(0, 3) == "084" || str_.Substring(0, 3) == "081" || str_.Substring(0, 3) == "092" ||
                //    str_.Substring(0, 3) == "085" || str_.Substring(0, 3) == "082" || str_.Substring(0, 3) == "056" ||
                //    str_.Substring(0, 3) == "058" || str_.Substring(0, 3) == "099" || str_.Substring(0, 3) == "059" ||
                //    str_.Substring(0, 3) == "090" || str_.Substring(0, 3) == "091" || str_.Substring(0, 3) == "093")
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool IsVI(string str)
        {
            if (string.IsNullOrWhiteSpace(str.Trim())) { return false; }
            var str_ = "1234567890aqwertyuiopasdfghjklzxcvbnm-";
            char[] lst = str.ToCharArray();
            for (int i = 0; i < lst.Length; ++i)
            {
                if (!str_.Contains(lst[i].ToString().ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsBienSo(string str)
        {
            if (string.IsNullOrWhiteSpace(str.Trim())) { return false; }
            var str_ = "1234567890aqwertyuiopasdfghjklzxcvbnm- .";
            char[] lst = str.ToCharArray();
            for (int i = 0; i < lst.Length; ++i)
            {
                if (!str_.Contains(lst[i].ToString().ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
        //Check mật khẩu mạnh
        public static bool IsStrongPassword(string pass, out string errorMsg)
        {
            errorMsg = string.Empty;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(pass))
            {
                //Password should contain at least one lower case letter.
                errorMsg = "LowerChar";
                return false;
            }
            else if (!hasUpperChar.IsMatch(pass))
            {
                //Password should contain at least one upper case letter.
                errorMsg = "UpperChar";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(pass))
            {
                //Password should not be lesser than 8.
                errorMsg = "MiniMaxChars";
                return false;
            }
            else if (!hasNumber.IsMatch(pass))
            {
                //Password should contain at least one numeric value.
                errorMsg = "Number";
                return false;
            }

            else if (!hasSymbols.IsMatch(pass))
            {
                //Password should contain at least one special case character.
                errorMsg = "Symbols";
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Color GetColorContractStatus(int? contractStatus)
        {
            var constatuscolor = Color.Default;
            switch (contractStatus)
            {
                case Constaint.Contract_Status.InComplete:
                    constatuscolor = Color.FromHex("#FF9900");
                    break;
                case Constaint.Contract_Status.WaitConfirm:
                    constatuscolor = Color.FromHex("#FF9999");
                    break;
                case Constaint.Contract_Status.WaitRenew:
                    constatuscolor = Color.FromHex("#F49A0E");
                    break;
                case Constaint.Contract_Status.NoConfirm:
                    constatuscolor = Color.FromHex("#EE0000");
                    break;
                case Constaint.Contract_Status.Confirmed:
                    constatuscolor = Color.FromHex("#00CCFF");
                    break;
                case Constaint.Contract_Status.Payed:
                    constatuscolor = Color.FromHex("#009966");
                    break;
                case Constaint.Contract_Status.Completed:
                    constatuscolor = Color.FromHex("#000099");
                    break;
                case Constaint.Contract_Status.Renewed:
                    constatuscolor = Color.FromHex("#999933");
                    break;
            }

            return constatuscolor;
        }

        public static string GetContractStatusString(string issueType, int? contractStatus)
        {
            var type = "";
            var status = "";
            if (issueType != null)
                Constaint.ContractType.TryGetValue(issueType, out type);
            if (contractStatus != null)
                Constaint.ContractStatus.TryGetValue(contractStatus.ToString(), out status);
            return !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(status) ? type + " - " + status : type + status;
        }

        public static Color GetColorViewNotifyStatus(string viewStatus)
        {
            var constatuscolor = Color.Default;
            switch (viewStatus)
            {
                case Constaint.Notify_View.Watched:
                    constatuscolor = Color.FromHex("#F5F6F7");
                    break;
                case Constaint.Notify_View.NotSeen:
                    constatuscolor = Color.FromHex("#FFFFFF");
                    break;
            }

            return constatuscolor;
        }

        public static Color GetColorChangeStatus(bool isChange)
        {
            var constatuscolor = Color.Default;
            if (isChange) { constatuscolor = Color.FromHex("#004FA1"); }
            else { constatuscolor = Color.FromHex("#000000"); }
            return constatuscolor;
        }

        public static string ConvertNumberToString(decimal? a)
        {
            try
            {
                string result = string.Empty;
                if (1000000 <= a && a < 1000000000)
                {
                    result = string.Format("{0:#,#} tr", (a / 1000000));
                }
                else if (1000000000 <= a && a < 1000000000000)
                {
                    result = string.Format("{0:#,#} tỷ", (a / 1000000000));
                }
                return result;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static double CalculateVAT(double priceB4tax, double alterFee, double VAT)
        {
            if (VAT == -1)
            {

            }

            return 0;
        }
        public static decimal CalculateVAT(decimal priceB4tax, decimal alterFee, decimal VAT)
        {
            return (decimal)CalculateVAT((double)priceB4tax, (double)alterFee, (double)VAT);
        }

        public static bool IsNumber(string num)
        {
            try
            {
                var a = Convert.ToDecimal(num);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string ChuyenDoiSoThuTu(int number)
        {
            try
            {
                string stt = string.Empty;
                var donVi = number % 10;
                if (donVi == 1)
                {
                    stt = number + "st ";
                }
                else if (donVi == 2)
                {
                    stt = number + "nd ";
                }
                else if (donVi == 3)
                {
                    stt = number + "rd ";
                }
                else
                {
                    stt = number + "th ";
                }
                return stt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}