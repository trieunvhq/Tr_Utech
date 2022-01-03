using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.APIModels
{
    public class RespondedAPI<T> where T: class
    {
        public int ResultCode { get; set; }
        public RespondedAPIData<T> Data { get; set; }
        public string ResultMsg { get; set; }
    }

    public class RespondedAPIData<T> where T : class
    {
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public T PageData { get; set; }
    }
}
