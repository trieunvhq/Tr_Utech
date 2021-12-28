using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class TravelFeeModel
    {
        public decimal ForeignCurrencyAmountInsurance { get; set; }
        public decimal VNCurrencyAmountInsurance { get; set; }
        public decimal ForeignCurrencyFee { get; set; }
        public decimal VNCurrencyFee { get; set; }
        public decimal LuggageFee { get; set; }
        public decimal TotalFee { get; set; }
    }
}
