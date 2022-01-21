using System;
using SQLite;

namespace QRMS.Models.NK
{
    [Table("NhapKhoModel")]
    public class NhapKhoModel
    {
        public int ID { get; set; }
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string Serial { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string InputStatus { get; set; }
        public string RecordStatus { get; set; }
        public decimal SoLuongDaNhap { get; set; }
        public int SoLuongBox { get; set; }
    }
}
