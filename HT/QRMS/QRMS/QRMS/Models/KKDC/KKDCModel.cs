using System;
namespace QRMS.Models.KKDC
{
    public class KKDCModel
    {
        public string TransactionType { get; set; }
        public string OrderNo { get; set; }
        public string WarehouseCode_From { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public Decimal SoLuongQuet { get; set; }
        public Decimal SoNhan { get; set; }
        public string Unit { get; set; }
        public string EXT_Serial { get; set; }
        public string EXT_PartNo { get; set; }
        public string EXT_LotNo { get; set; }

        public string Color { get; set; }
        public string ColorSLDaNhap { get; set; }
    }
}
