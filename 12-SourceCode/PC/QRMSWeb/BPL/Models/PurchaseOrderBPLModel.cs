using System;

namespace BPL.Models
{
    public class PurchaseOrderBPLModel
    {
        public int ID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
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
