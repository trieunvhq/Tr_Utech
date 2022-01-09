using QRMSWeb.Constants;
using System;

namespace QRMSWeb.Models
{
    public class ItemModel
    {
        public int ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
    }
}