using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS
{
    public class TransferStatus
    {
        public const int None = 0;
        public const int ItemMaster = 1;
        public const int Location = 2;
        public const int Warehouse = 3;
        public const int Customer = 4;
        public const int Supplier = 5;
    }
}
