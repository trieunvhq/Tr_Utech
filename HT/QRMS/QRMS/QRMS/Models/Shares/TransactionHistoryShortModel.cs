using System;
namespace QRMS.Models.Shares
{
    public class TransactionHistoryShortModel
    {
        public string TransactionType { get; set; }
        public string OrderNo { get; set; }
        public string LocationCode_From { get; set; }
        public string LocationName_From { get; set; }
        public string WarehouseCode_From { get; set; }
        public string WarehouseName_From { get; set; }
        public string WarehouseType_From { get; set; }
        public string LocationCode_To { get; set; }
        public string LocationName_To { get; set; }
        public string WarehouseCode_To { get; set; }
        public string WarehouseName_To { get; set; }
        public string WarehouseType_To { get; set; }
        public string CustomerName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ExportStatus { get; set; }
        public string RecordStatus { get; set; }
        public string DATA { get; set; }
        public string Key { get; set; }
        public string Status { get; set; }
    }
}
