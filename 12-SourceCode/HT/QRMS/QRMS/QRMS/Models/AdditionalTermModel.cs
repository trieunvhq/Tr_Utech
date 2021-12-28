using QRMS.Helper;
using QRMS.Resources;

namespace QRMS.Models
{
    public class AdditionalTermModel : Notifiable
    {
        public string Name { get; set; }
        public decimal Value { get; set; } = 300000000;
        public decimal AdjustedValue { get; set; }
        public float AdjustedRate { get; set; }
        public bool IsChecked { get; set; }
        public string DecoValue => StringUtils.FormatCurrency(Value.ToString());
    }
}