using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.APIModels
{
    /// <summary>
    /// co the du dung cho api Get Delivery
    /// </summary>
    public class SaleOrderAPIModel
    {
        public int docNum { get; set; }
        public string cardCode { get; set; }
        public string cardName { get; set; }
        public DateTime? postingDate { get; set; }
        public DateTime? deliveryDate { get; set; }
        public DateTime? documentDate { get; set; }
        public string location { get; set; }
    }
}
