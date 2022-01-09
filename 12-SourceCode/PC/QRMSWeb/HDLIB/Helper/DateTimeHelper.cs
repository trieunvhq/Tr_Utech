using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB.Helper
{
    public class DateTimeHelper
    {
        public static DateTime? ConvertStringDateTimeToDate(string dateTime, string formatDT = "dd-MMM-yyyy HH:mm:ss tt")
        {
            dateTime = dateTime?.Trim();
            if (string.IsNullOrEmpty(dateTime)) return null;
            try {
                formatDT = formatDT?.Trim();
                if (string.IsNullOrEmpty(formatDT))
                {
                    return DateTime.ParseExact(dateTime, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                return DateTime.ParseExact(dateTime, formatDT, CultureInfo.InvariantCulture);
            }catch(Exception e)
            {
                try {
                    return DateTime.Parse(dateTime);
                }
                catch (Exception) { }

                return null;
            }
        }
        public static string GetYearVNFormated(object dateTime)
        {
            if (dateTime == null) return "";
            try
            {
                var objDate = (DateTime)dateTime;
                return objDate.ToString("yyyy");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        public static string GetYearVNFormated(DateTime? dateTime)
        {
            if (dateTime == null) return "";
            return dateTime?.ToString("yyyy");
        }

        public static string GetYearVNFormated(string strDateTime, string orgFormat = "yyyy-MM-dd")
        {
            if (string.IsNullOrEmpty(strDateTime?.Trim())) return "";
            try
            {
                var dateTime = DateTime.ParseExact(strDateTime, orgFormat, CultureInfo.InvariantCulture);
                return GetYearVNFormated(dateTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        public static string GetDateVNFormated(object dateTime)
        {
            if (dateTime == null) return "";
            try
            {
                var objDate = (DateTime)dateTime;
                return objDate.ToString("dd'/'MM'/'yyyy");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        public static string GetDateVNFormated(DateTime? dateTime)
        {
            if (dateTime == null) return "";
            return dateTime?.ToString("dd'/'MM'/'yyyy");
        }

        public static string GetDateVNFormated(string strDateTime, string orgFormat = "yyyy-MM-dd")
        {
            if (string.IsNullOrEmpty(strDateTime?.Trim())) return "";
            try
            {
                var dateTime = DateTime.ParseExact(strDateTime, orgFormat, CultureInfo.InvariantCulture);
                return GetDateVNFormated(dateTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
        public static DateTime? ConvertDateVNFormatedToDate(string strDate)
        {
            if (string.IsNullOrEmpty(strDate?.Trim())) return null;

            try
            {
                strDate = strDate?.Trim().Replace("-", $"/");
                var cultureInfo = new CultureInfo("fr-FR");
                DateTime dateTime11 = DateTime.ParseExact(strDate, "dd/MM/yyyy", cultureInfo);
                return dateTime11;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return null;
        }
        public static string GetFirstDayOfThisMonth()
        {
            var firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return GetDateVNFormated(firstDay);
        }
    }
}
