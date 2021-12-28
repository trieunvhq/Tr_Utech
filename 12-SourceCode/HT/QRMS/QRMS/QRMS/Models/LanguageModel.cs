using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class LanguageModel
    {
        public int ID { get; set; }
        public string ScreenCode { get; set; }
        public string ScreenName { get; set; }
        public string ItemType { get; set; }
        public string TypeName { get; set; }
        public string LangVi { get; set; }
        public string LangEn { get; set; }
        public string Remark { get; set; }
    }
}
