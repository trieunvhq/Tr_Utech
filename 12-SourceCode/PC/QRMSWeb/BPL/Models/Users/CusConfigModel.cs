using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPL.Models.Users
{
    public class CusConfigModel
    {
        public bool IsMaintain { get; set; }
        public string MessageLogin { get; set; }

        public User objAccount { get; set; }
        public string TokenAccess { get; set; }
    }
}
