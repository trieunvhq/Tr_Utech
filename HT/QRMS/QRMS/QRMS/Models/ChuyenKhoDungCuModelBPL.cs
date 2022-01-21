using System;
using SQLite;

namespace QRMS.Models
{
    [Table("ChuyenKhoDungCuModelBPL")]
    public class ChuyenKhoDungCuModelBPL
    {
        public int ID { get; set; }
        public int TransferOrderID { get; set; }
        public string TransferOrderNo { get; set; }
        public Nullable<System.DateTime> InstructionDate { get; set; }
        public string GetDataStatus { get; set; }
        public string TransferStatus { get; set; }
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
        public string TransferType { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal Quantity { get; set; }
        public string sQuantity { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UserCreate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UserUpdate { get; set; }
        public string OtherCode { get; set; }
        public string Serial { get; set; }
        public string PartNo { get; set; }
        public string LotNo { get; set; }
        public Nullable<System.DateTime> MfDate { get; set; }
        public Nullable<System.DateTime> RecDate { get; set; }
        public Nullable<System.DateTime> ExpDate { get; set; }
        public int? IdxOrder { get; set; }
        public decimal SoLuongDaChuyen { get; set; }
        public string sSoLuongDaChuyen { get; set; }
        public int SoLuongBox { get; set; }



        public string Color { get; set; }
        public string ColorSLDaNhap { get; set; }

    }
}
