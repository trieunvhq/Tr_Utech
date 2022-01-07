namespace BPL.Models.Web.PrintLabel
{
    using System;
    using System.Collections.Generic;

    public class LabelPrintItemModel
    {
        public int ID { get; set; }
        public int LabelPrintID { get; set; }
        public string PrintOrderNo { get; set; }
        public Nullable<System.DateTime> PrintOrderDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string OtherCode { get; set; }
        public string Serial { get; set; }
        public string PartNo { get; set; }
        public string LotNo { get; set; }
        public Nullable<System.DateTime> MfDate { get; set; }
        public Nullable<System.DateTime> RecDate { get; set; }
        public Nullable<System.DateTime> ExpDate { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Unit { get; set; }
        public string PrintStatus { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUser { get; set; }
    }
}
