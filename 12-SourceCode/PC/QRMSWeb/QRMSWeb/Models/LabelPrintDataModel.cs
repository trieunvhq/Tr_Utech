namespace QRMSWeb.Models
{
    using QRMSWeb.Services;
    using QRMSWeb.Utils;
    using System;
    using System.Collections.Generic;

    public abstract class QRCodeModel
    {
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CustomerCode { get; set; }
        public string Serial { get; set; }
        public string PartNo { get; set; }
        public string LotNo { get; set; }
        public string MfDate { get; set; }
        public string RecDate { get; set; }
        public string ExpDate { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
    }

    public interface ILabelModel
    {
        string GenerateCodePayload();
    }

    public class LabelPrintQRCodeModel : QRCodeModel, ILabelModel
    {
        public string GenerateCodePayload()
        {

            return $"{ItemType};{ItemCode};{Serial};{PartNo};{LotNo};{MfDate};{RecDate};{ExpDate};{Quantity};{Unit}";
        }
    }
}
