using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPL.Models
{
    public class NhapKhoDungCuBPLModel
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
        public string InputStatus { get; set; }
        public string RecordStatus { get; set; }
        public decimal SoLuongDaNhap { get; set; }
        public int SoLuongBox { get; set; }
    }
}
