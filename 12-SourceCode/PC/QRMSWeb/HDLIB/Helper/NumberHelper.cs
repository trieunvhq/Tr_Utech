using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB.Helper
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
                Console.WriteLine(ex.Message);
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
        public static string GetNumberFormatValue(string data, bool hasNegative = false)
        {
            if (string.IsNullOrEmpty(data?.Trim())) return "";
            data = data.Trim();
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                var _number = decimal.Parse(data.Trim(), formatNumberToVN);
                if (!hasNegative && _number < 0)
                {
                    _number = Math.Abs(_number);
                }
                //return _number == 0 ? "0" : _number.ToString("#.##0");
                return _number == 0 ? "0" : _number.ToString("#,##0", formatNumberToVN).Replace(",", ".");
                // return _number.ToString("#.##0");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
        public static string GetNumberFormatValue(decimal? data, bool hasNegative = false)
        {
            if (data == null) return "";
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                /*string value = data.ToString();
                
                var _number = decimal.Parse(value.Trim(), formatNumberToVN);

                //return _number == 0 ? "0" : _number.ToString("#.##0,###");
                */
                if (!hasNegative && data < 0)
                {
                    data = Math.Abs(data ?? 0);
                }
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                return data == 0 ? "0" : data?.ToString("#,##0", formatNumberToVN).Replace(",", ".");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
        public static string GetNumberFormatValue(object data, bool hasNegative = false)
        {
            if (data == null) return "";
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }

                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                //
                try
                {
                    var _number = (decimal)data;
                    if (!hasNegative && _number < 0)
                    {
                        _number = Math.Abs(_number);
                    }
                    //return _number == 0 ? "0" : _number.ToString("#.##0,###");
                    return _number == 0 ? "0" : _number.ToString("#,##0", formatNumberToVN).Replace(",", ".");
                }
                catch (Exception e)
                {
                    string value = data.ToString();
                    var _number = decimal.Parse(value, formatNumberToVN);
                    if (!hasNegative && _number < 0)
                    {
                        _number = Math.Abs(_number);
                    }
                    //return _number == 0 ? "0" : _number.ToString("#.##0,###");
                    return _number == 0 ? "0" : _number.ToString("#,##0", formatNumberToVN).Replace(",", ".");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        public static string GetNumberFullFormatValue(string data, bool hasNegative = false)
        {
            if (string.IsNullOrEmpty(data?.Trim())) return "";
            data = data.Trim().Replace(".", "").Replace(",", ".");
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                var _number = decimal.Parse(data.Trim(), formatNumberToVN);
                if (!hasNegative && _number < 0)
                {
                    _number = Math.Abs(_number);
                }
                // return _number == 0 ? "0" : _number.ToString("#,##0.###");
                return _number == 0 ? "0" : _number.ToString("#,##0.###", formatNumberToVN).Replace(".", "_").Replace(",", ".").Replace("_", ",");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
        public static string GetNumberFullFormatValue(decimal? data, bool hasNegative = false)
        {
            if (data == null) return "";
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                /*
                string value = data. .Value();
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                var _number = decimal.Parse(value.Trim(),formatNumberToVN);
                //return _number == 0 ? "0" : _number.ToString("#,##0.###");
                */
                if (!hasNegative && data < 0)
                {
                    data = Math.Abs(data ?? 0);
                }
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                return data == 0 ? "0" : data?.ToString("#,##0.###", formatNumberToVN).Replace(".", "_").Replace(",", ".").Replace("_", ",");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
        public static string GetNumberFullFormatValue(object data, bool hasNegative = false)
        {
            if (data == null) return "";
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                decimal _number = (decimal)data;
                if (!hasNegative && _number < 0)
                {
                    _number = Math.Abs(_number);
                }
                //var _number = decimal.Parse(value.Trim());
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                return _number == 0 ? "0" : _number.ToString("#,##0.###", formatNumberToVN).Replace(".", "_").Replace(",", ".").Replace("_", ",");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
    }
}
