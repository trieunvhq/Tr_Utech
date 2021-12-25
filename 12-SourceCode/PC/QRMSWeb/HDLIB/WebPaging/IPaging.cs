using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB.WebPaging
{
    public interface IPaging
    {
        int page { get; set; }

        int pages { get; set; }

        int limit { get; set; }

        int total { get; set; }
    }
}
