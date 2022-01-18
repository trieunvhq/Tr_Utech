using System;
using SQLite;
using Xamarin.Forms;

namespace QRMS.Models
{
    [Table("NhapKhoDungCuModel")]
    public class NhapKhoDungCuModel
    {
        public int ID { get; set; }
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public string WarehouseCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal Quantity { get; set; }
        public string sQuantity { get; set; }
        public string Unit { get; set; }
        public string InputStatus { get; set; }
        public string RecordStatus { get; set; }
        public decimal SoLuongDaNhap { get; set; }
        public string sSoLuongDaNhap { get; set; }
        public int SoLuongBox { get; set; }
        public string Color { get; set; }
        public string ColorSLDaNhap { get; set; }
    }
}
