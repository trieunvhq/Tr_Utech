using System;

namespace QRMSWeb.Models
{
    public class SaleOrderItemModel
    {
        public int ID { get; set; }
        public int SaleOrderID { get; set; }
        public string SaleOrderNo { get; set; }
        public Nullable<System.DateTime> SaleOrderDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string ItemType { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UserCreate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UserUpdate { get; set; }
        
    }
}