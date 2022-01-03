using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB.Common
{
    public class DateTimeHelper
    {
        public static DateTime? ConvertStringDateTimeToDate(string dateTime, string formatDT = "dd-MM-yyyy")
        {
            try {
                dateTime = dateTime?.Trim();
                formatDT = formatDT?.Trim();
                if (string.IsNullOrEmpty(dateTime) || string.IsNullOrEmpty(formatDT)) return null;
                return DateTime.ParseExact(dateTime, formatDT, CultureInfo.InvariantCulture);
            }catch(Exception e)
            {
                return null;
            }
        }
    }
}
