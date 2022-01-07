using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB.Common
{
    public class NumberHelper
    {
        public static decimal? ConvertStringNumberVNToDecimal(string strVNNumber, int integerLen = 15, int decimalLen = 3)
        {
            if (string.IsNullOrEmpty(strVNNumber?.Trim())) return null;
            try
            {
                strVNNumber = strVNNumber.Trim().Replace(".", "").Replace(",", ".");
                if (0.Equals(strVNNumber))
                {
                    return 0;
                }
                strVNNumber = ConvertStringNumberBiggerMaxValue(strVNNumber, integerLen, decimalLen);
                CultureInfo formatNumberToEN = new CultureInfo("en-US");
                return decimal.Parse(strVNNumber, formatNumberToEN);

            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
        public static int? ConvertStringNumberVNToInt(string strVNNumber)
        {
            if (string.IsNullOrEmpty(strVNNumber?.Trim())) return null;
            try
            {
                strVNNumber = strVNNumber.Trim().Replace(".", "").Replace(",", ".");
                strVNNumber = ConvertStringNumberBiggerMaxValue(strVNNumber, 11, 0);
                var numParts = strVNNumber.Split('.');
                strVNNumber = numParts[0];
                if (0.Equals(strVNNumber))
                {
                    return 0;
                }

                CultureInfo formatNumberToEN = new CultureInfo("en-US");
                return int.Parse(strVNNumber, formatNumberToEN);

            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
        public static short? ConvertStringNumberVNToShort(string strVNNumber)
        {
            if (string.IsNullOrEmpty(strVNNumber?.Trim())) return null;
            try
            {

                strVNNumber = strVNNumber.Trim().Replace(".", "").Replace(",", ".");
                strVNNumber = ConvertStringNumberBiggerMaxValue(strVNNumber, 5, 0);
                var numParts = strVNNumber.Split('.');
                strVNNumber = numParts[0];
                if (0.Equals(strVNNumber))
                {
                    return 0;
                }

                CultureInfo formatNumberToEN = new CultureInfo("en-US");
                return short.Parse(strVNNumber, formatNumberToEN);

            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public static string ConvertStringNumberBiggerMaxValue(string strNumber, int maxLengthInterger = 18, int maxLengthDecimal = 0)
        {
            if (String.IsNullOrEmpty(strNumber?.Trim()) || maxLengthInterger < 1)
            {
                return string.Empty;
            }
            strNumber = strNumber.Trim().Replace(" ", "");
            if (maxLengthDecimal < 0) maxLengthDecimal = 0;
            string[] splitNumber = strNumber.Split('.');
            string intergerPart = string.IsNullOrEmpty(splitNumber[0]?.Trim()) ? "0" : splitNumber[0]?.Trim();

            if (splitNumber[0].Length > maxLengthInterger)
            {
                intergerPart = splitNumber[0].Substring(0, maxLengthInterger);
            }

            if (splitNumber.Length > 1 && maxLengthDecimal > 0)
            {
                string decimalPart = splitNumber[1];
                if (maxLengthDecimal < splitNumber[1].Length)
                {
                    decimalPart = splitNumber[1].Substring(0, maxLengthDecimal);
                }
                return intergerPart + "." + decimalPart;
            }
            else
            {
                return intergerPart;
            }
        }
    }
}
