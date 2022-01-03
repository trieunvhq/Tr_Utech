using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIS.APIModels;

namespace AMIS.APIModels
{
    public class PurchaseOrderAPIModel
    {
        public int docNum { get; set; }
        public string vendorCode { get; set; }
        public string vendorName { get; set; }
        public DateTime? postingDate { get; set; }
        public DateTime? deliveryDate { get; set; }
        public DateTime? documentDate { get; set; }
        public string location { get; set; }
    }

}
