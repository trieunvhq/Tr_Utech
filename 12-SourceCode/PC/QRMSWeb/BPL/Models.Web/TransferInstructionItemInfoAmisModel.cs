namespace BPL.Models.Web
{
    using System;
    using System.Collections.Generic;

    public class TransferInstructionItemInfoAmisModel
    {
        public int? TransferOrderID { get; set; }
        public string TransferOrderNo { get; set; }
        public string TransferOrderType { get; set; }
        public string TransferOrderDate { get; set; }
        public string FWarehouseCode { get; set; }
        public string FWarehouseName { get; set; }
        public string TWarehouseCode { get; set; }
        public string TWarehouseName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public Decimal? Quantity { get; set; }
        public string Unit { get; set; }
    }
}
