using System;
namespace BPL.Models
{
    public class SaleOrderItemScanModelBPL
    {
        public int ID { get; set; }
        public int SaleOrderID { get; set; }
        public string SaleOrderNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string RecordStatus { get; set; }
        public decimal SoLuongDaNhap { get; set; }
        public int SoLuongBox { get; set; }
    }
}
