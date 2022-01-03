using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPL.Models.Web.Report
{
    public class ReportResponseBase
    {
        public enum EReportFileType
        {
            Pdf = 1,
            Excel = 2,
            Browser = 3
        }

        public ReportResponseBase()
        {

        }
        public ReportResponseBase(byte[] data, string file_name, EReportFileType file_type)
        {
            this.Data = data;
            this.FileName = file_name;
        }
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public EReportFileType FileType { get; set; }
    }
}
