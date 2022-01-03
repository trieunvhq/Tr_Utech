using System;
namespace QRMS.Models
{
    public class QRModel
    {
        public string DC { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CustomerCode { get; set; }
        public string OtherCode { get; set; }
        public string Serial { get; set; }
        public string PartNo { get; set; }
        public string LotNo { get; set; }
        public Nullable<System.DateTime> MfDate { get; set; }
        public Nullable<System.DateTime> RecDate { get; set; }
        public Nullable<System.DateTime> ExpDate { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Unit { get; set; }
    }
}
