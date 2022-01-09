using System;
namespace BPL.Models
{
    public class SaleOrderBPLModel
    {
        public int ID { get; set; }
        public Nullable<int> SaleOrderAmisID { get; set; }
        public string SaleOrderNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> SaleOrderDate { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string ExportStatus { get; set; }
        public string GetDataStatus { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UserCreate { get; set; }
        public string UserUpdate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}
