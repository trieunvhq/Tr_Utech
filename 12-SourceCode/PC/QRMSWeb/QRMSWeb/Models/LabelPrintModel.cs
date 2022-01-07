namespace QRMSWeb.Models
{
    using QRMSWeb.Services;
    using QRMSWeb.Utils;
    using System;
    using System.Collections.Generic;

    public class LabelPrintModel
    {
        public int ID { get; set; }
        public string PrintOrderNo { get; set; }
        public Nullable<System.DateTime> PrintOrderDate { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string ItemType { get; set; }

        public string PrintStatus { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUser { get; set; }

        //Extend
        public string ItemTypeName { 
            get {
                return CommonUtils.GetItemTypeName(ItemType);
            }
        }

        public string PrintStatusName
        {
            get
            {
                return CommonUtils.GetPrintStatusName(ItemType);
            }
        }
        public List<LabelPrintItemModel> LabelPrintItemModels { get; set; } = new List<LabelPrintItemModel>();

        public PaginateData<List<LabelPrintItemModel>>? LabelPrintItemPagging = new PaginateData<List< LabelPrintItemModel>>();
    }
}
