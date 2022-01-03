using System;
namespace QRMS.Models
{
    public class TransferInstructionModel
    {
        public int ID { get; set; }
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
        public string Remark { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UserCreate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UserUpdate { get; set; }

    }
}
