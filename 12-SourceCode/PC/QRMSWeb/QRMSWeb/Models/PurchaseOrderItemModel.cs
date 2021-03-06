using QRMSWeb.Constants;
using System;

namespace QRMSWeb.Models
{
    public class PurchaseOrderItemModel
    {
        public int ID { get; set; }
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string WareHouseCode { get; set; }
        public string WareHouseName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string InputStatus { get; set; }
        public string PrintStatus { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UserCreate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UserUpdate { get; set; }

        //ext
        public string ItemTypeName
        {
            get {

                if (ConstItemType.DungCu.Equals(ItemType))
                {
                    return "Dụng cụ, thiết bị";
                }
                else if(ConstItemType.NguyenLieu.Equals(ItemType))
                {
                    return "Nguyên liệu";
                }
                else if (ConstItemType.ThanhPham.Equals(ItemType))
                {
                    return "Thành phẩm";
                }
                return "";
            }
        }
        public string PrintStatusName
        {
            get
            {

                if (ConstItemType.DungCu.Equals(ItemType))
                {
                    return "Dụng cụ, thiết bị";
                }
                else if (ConstItemType.NguyenLieu.Equals(ItemType))
                {
                    return "Nguyên liệu";
                }
                else if (ConstItemType.ThanhPham.Equals(ItemType))
                {
                    return "Thành phẩm";
                }
                return "";
            }
        }
    }
}