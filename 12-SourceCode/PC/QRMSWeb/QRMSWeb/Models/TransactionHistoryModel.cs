using QRMSWeb.Constants;
using System;

namespace QRMSWeb.Models
{
    public class TransactionHistoryModel
    {
        public long ID { get; set; }
        public string TransactionType { get; set; }
        public string OrderNo { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string LocationCode_From { get; set; }
        public string LocationName_From { get; set; }
        public string WarehouseCode_From { get; set; }
        public string WarehouseName_From { get; set; }
        public string WarehouseType_From { get; set; }
        public string LocationCode_To { get; set; }
        public string LocationName_To { get; set; }
        public string WarehouseCode_To { get; set; }
        public string WarehouseName_To { get; set; }
        public string WarehouseType_To { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Unit { get; set; }
        public string EXT_OtherCode { get; set; }
        public string EXT_Serial { get; set; }
        public string EXT_PartNo { get; set; }
        public string EXT_LotNo { get; set; }
        public Nullable<System.DateTime> EXT_MfDate { get; set; }
        public Nullable<System.DateTime> EXT_RecDate { get; set; }
        public Nullable<System.DateTime> EXT_ExpDate { get; set; }
        public string EXT_QRCode { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
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