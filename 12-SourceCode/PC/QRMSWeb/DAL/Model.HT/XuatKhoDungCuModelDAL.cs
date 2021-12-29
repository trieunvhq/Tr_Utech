using System;
namespace DAL.Model.HT
{
    public class XuatKhoDungCuModelDAL
    {
        public int ID { get; set; }
        public int TransferOrderID { get; set; }
        public string TransferOrderNo { get; set; }
        public Nullable<System.DateTime> InstructionDate { get; set; }
        public string TransferStatus { get; set; }
        public string TransferType { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string RecordStatus { get; set; }
        public decimal SoLuongDaNhap { get; set; }
        public int SoLuongBox { get; set; }
    }
}
