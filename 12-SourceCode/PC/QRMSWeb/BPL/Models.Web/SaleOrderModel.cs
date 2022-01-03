namespace BPL.Models.Web
{
    using System;
    using System.Collections.Generic;

    public class SaleOrderModel
    {
        public int ID { get; set; }
        public string SaleOrderNo { get; set; }
        public Nullable<System.DateTime> SaleOrderDate { get; set; }
        public string ExportStatus { get; set; }
        public string GetDataStatus { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UserCreate { get; set; }
        public string UserUpdate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }

        List<SaleOrderItemModel> SaleOrderItemModels { get; set; }
    }
}
