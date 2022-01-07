namespace BPL.Models.Web.PrintLabel
{
    using System;
    using System.Collections.Generic;

    public class LabelPrintModel
    {
        public int ID { get; set; }
        public string PrintOrderNo { get; set; }
        public Nullable<System.DateTime> PrintOrderDate { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string ItemType { get; set; }
        public string PrintStatus { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUser { get; set; }

        public List<LabelPrintItemModel> LabelPrintItemModels { get; set; } = new List<LabelPrintItemModel>();

        public HDLIB.WebPaging.TPaging<LabelPrintItemModel> LabelPrintItemPagging = new HDLIB.WebPaging.TPaging<LabelPrintItemModel>();
    }
}
