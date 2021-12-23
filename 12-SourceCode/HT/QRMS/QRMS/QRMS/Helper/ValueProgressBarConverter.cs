using QRMS.AppLIB.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace QRMS.Helper
{
    public class ValueProgressBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if 30 sec is your maximum time
            // return (double)value/30;

            //if 60 sec if your maximum time
            return (double)value / 60;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }

    /// <summary>
    /// </summary>
    public class DecimalConverter : IValueConverter
    {

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal result = 0; decimal.TryParse(value?.ToString(), NumberStyles.Any, Constaint.cultureInfo, out result);
            if (result == 0)
            {
                return "";
            }
            var dec = Math.Round(result);
            return dec.ToString("#,#", Constaint.cultureInfo);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
            {
                strValue = "0";
            }
            decimal result = 0; decimal.TryParse(strValue, NumberStyles.Any, Constaint.cultureInfo, out result);
            return result;
        }
    }
    /// <summary>
    /// Convert decimal with fraction part
    /// </summary>
    public class RatioConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal result = 0; decimal.TryParse(value?.ToString(), NumberStyles.Any, Constaint.cultureInfo, out result);
            if (result == 0) return "";
            return result.ToString("#,#.#", Constaint.cultureInfo);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value as string;
            if (string.IsNullOrEmpty(strValue)) strValue = "0";
            decimal result = 0; decimal.TryParse(strValue, NumberStyles.Any, Constaint.cultureInfo, out result);
            return result;
        }
    }

    /// <summary>
    /// Convert to currency
    /// </summary>
    public class CurrencyConverter : IValueConverter
    {

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal.TryParse(value?.ToString(), NumberStyles.Any, Constaint.cultureInfo, out decimal result);
            if (result == 0)
            {
                return "";
            }
            var dec = Math.Round(result);
            return StringUtils.FormatCurrency(dec.ToString());
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
            {
                strValue = "0";
            }
            decimal result = 0; decimal.TryParse(strValue, NumberStyles.Any, Constaint.cultureInfo, out result);
            return result;
        }
    }

    /// <summary>
    /// Convert to currency
    /// </summary>
    public class AbsCurrencyConverter : IValueConverter
    {

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal.TryParse(value?.ToString(), NumberStyles.Any, Constaint.cultureInfo, out decimal result);
            if (result == 0)
            {
                return "";
            }
            var dec = Math.Abs(Math.Round(result));
            return StringUtils.FormatCurrency(dec.ToString());
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
            {
                strValue = "0";
            }
            decimal result = 0; decimal.TryParse(strValue, NumberStyles.Any, Constaint.cultureInfo, out result);
            return result;
        }
    }
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
