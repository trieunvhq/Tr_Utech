using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Reports
{
    public class LabelPrintReportModel
    {
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

        public string ItemTypeName { get; set; }

        public string QRCodeBase64  { get; set; }
        public string QRCode { get; set; }
    }
}