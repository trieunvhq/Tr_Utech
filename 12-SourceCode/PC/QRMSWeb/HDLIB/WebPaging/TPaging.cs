using System;
using System.Collections.Generic;
using System.Linq;

namespace HDLIB.WebPaging
{
    public class TPaging<R> : HDLIB.WebPaging.IPaging
    where R : class
    {
        public TPaging()
        {
        }

        public TPaging(IEnumerable<R> data, int page, int limit, int total)
        {
            CalculatePaging(data, page, limit, total);
        }

        public List<R> rows { get; set; }

        public int page { get; set; }

        public int pages { get; set; }

        public int limit { get; set; }

        public int total { get; set; }
       
        public void CalculatePaging(IEnumerable<R> data, int page, int limit, int total)
        {
            this.rows = data.ToList();
            this.page = page;
            this.pages = (int)Math.Ceiling((double)total / limit);
            this.limit = limit;
            this.total = total;
        }     
    }
}