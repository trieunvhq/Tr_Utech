namespace BPL.Models.Web
{
    using System;
    using System.Collections.Generic;

    public class SaleOrderItemInfoAmisModel
    {

        public int? SaleOrderID { get; set; }
        public string SaleOrderNo { get; set; }
        public string SaleOrderDate { get; set; }
        public string DeliveryDate { get; set; }        
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string ItemType { get; set; }
        public Decimal? Quantity { get; set; }
    }
}
