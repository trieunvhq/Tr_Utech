using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPL.Models.Web.PrintLabel
{
    public class LabelPrintReportModel
    {
        public int ID { get; set; }
        public int LabelPrintID { get; set; }
        public string PrintOrderNo { get; set; }
        public string PrintOrderDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string OtherCode { get; set; }
        public string Serial { get; set; }
        public string PartNo { get; set; }
        public string LotNo { get; set; }
        public string MfDate { get; set; }
        public string RecDate { get; set; }
        public string ExpDate { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string PrintStatus { get; set; }
        public string RecordStatus { get; set; }
        public string CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateUser { get; set; }

        public string ItemTypeName { get; set; }

        public string QRCodeBase64  { get; set; }
    }
}