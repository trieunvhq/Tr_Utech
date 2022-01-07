namespace BPL.Models.Web.PrintLabel
{
    using HDLIB.Common;
    using System;
    using System.Collections.Generic;

    public class LabelPrintItemFromExcelFileModel
    {
        public string PrintOrderNo { get; set; }
        public string PrintOrderDate { get; set; }
        
        public string ItemTypeName { get; set; }
        public string WareHouseCode { get; set; }
        public string WareHouseName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }        
        public string OtherCode { get; set; }
        public string Serial { get; set; }
        public string PartNo { get; set; }
        public string LotNo { get; set; }
        public string MfDate { get; set; }
        public string RecDate { get; set; }
        public string ExpDate { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }

        //Extent
        public string ItemType {
            get
            {
                ItemTypeName = (ItemTypeName?.Trim())??string.Empty;

                if (ItemTypeName.ToLower().Contains("dụng") || ItemTypeName.ToLower().Contains("thiết")) {
                    return ConstItemType.DungCu;
                } else if (ItemTypeName.ToLower().Contains("nguyên") || ItemTypeName.ToLower().Contains("liệu")) {
                    return ConstItemType.NguyenLieu;
                }
                else if (ItemTypeName.ToLower().Contains("thành") || ItemTypeName.ToLower().Contains("phẩm"))
                {
                    return ConstItemType.ThanhPham;
                }
                return string.Empty;
            }
        }
    }
}
