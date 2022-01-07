using System;
using QRMSWeb.Constants;
namespace QRMSWeb.Models
{
    public class PurchaseOrderModel
    {
        public int ID { get; set; }
        public Nullable<int> PurchaseOrderAmisID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ExportStatus { get; set; }
        public string InputStatus { get; set; }
        public string PrintStatus { get; set; }
        public string GetDataStatus { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UserCreate { get; set; }
        public string UserUpdate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }

        public string InputStatusName
        {
            get
            {

                if (ConstInputStatus.DangNhap.Equals(InputStatus))
                {
                    return "Đang nhập";
                }
                else if (ConstInputStatus.DaNhapDu.Equals(InputStatus))
                {
                    return "Đã nhập";
                }
                else if (ConstInputStatus.ChuaNhap.Equals(InputStatus))
                {
                    return "Chưa nhập";
                }
                return "";
            }
        }
    }
}