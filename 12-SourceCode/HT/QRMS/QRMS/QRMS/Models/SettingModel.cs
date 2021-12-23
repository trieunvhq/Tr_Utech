using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class SettingModel
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
    }
}
