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
    }
}
