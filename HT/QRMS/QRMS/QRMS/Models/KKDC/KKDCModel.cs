using System;
namespace QRMS.Models.KKDC
{
    public class KKDCModel
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
        public Decimal SoLuongKiemKe { get; set; }
        public Decimal SoNhan { get; set; }
        public string Unit { get; set; }
    }
}
