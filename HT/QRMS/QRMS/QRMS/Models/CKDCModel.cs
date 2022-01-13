using System;
namespace QRMS.Models
{
    public class CKDCModel
    {
        public string TransactionType { get; set; }
        public string OrderNo { get; set; }
        public string WarehouseCode_From { get; set; }
        public string WarehouseName_From { get; set; }
        public string WarehouseType_From { get; set; }
        public string WarehouseCode_To { get; set; }
        public string WarehouseName_To { get; set; }
        public string WarehouseType_To { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public Decimal SoLuongQuet { get; set; }
        public string sSoLuongQuet { get; set; }
        public Decimal SoNhan { get; set; }
        public string Unit { get; set; }

        public string Color { get; set; }
        public string ColorSLDaNhap { get; set; }
    }
}
