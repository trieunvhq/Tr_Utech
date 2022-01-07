using System;
namespace QRMS.Models
{
    public class PurchaseOrderModelInput
    {
        public string WarehouseCode { get; set; }
        public DateTime from_day { get; set; }
        public DateTime to_day { get; set; }
    }
}
