using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class AccountLogModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public int NumberFail { get; set; }
    }
}
