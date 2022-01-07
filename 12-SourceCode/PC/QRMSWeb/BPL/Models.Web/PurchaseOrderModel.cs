using System;
namespace BPL.Models.Web
{
    using System;
    public class PurchaseOrderModel
    {
        public int ID { get; set; }
        public Nullable<int> PurchaseOrderAmisID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ExportStatus { get; set; }
        public string InputStatus { get; set; }
        public string PrintStatus { get; set; }
        public string GetDataStatus { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UserCreate { get; set; }
        public string UserUpdate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}
