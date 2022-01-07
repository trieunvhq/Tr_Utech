using System;
namespace BPL.Models
{
    public class PrLogsModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string funtion { get; set; }
        public string exception { get; set; }
        public string my_view { get; set; }
        public string username { get; set; }
    }
}
