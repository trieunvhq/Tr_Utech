namespace BPL.Models.Web
{
    using System;
    using System.Collections.Generic;

    public class PurchaseOrderItemInfoAmisModel
    {

        public int? PurchaseOrderID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string ReciveDate { get; set; }        
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string Unit { get; set; }        
        public Decimal? Quantity { get; set; }
    }
}
