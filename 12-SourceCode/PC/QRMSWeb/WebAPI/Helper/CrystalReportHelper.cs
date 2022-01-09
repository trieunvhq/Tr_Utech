using CrystalDecisions.CrystalReports.Engine;
using HDLIB.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Helper
{
    public class CrystalReportHelper<T>
        where T : class
    {
        public string GenerateBase64Reports(string physicalReportPath, string reportFile, List<T> printDataSource = null, List<T> subPrintDataSource = null)
        {
            try
            {
                using (ReportDocument rd = new ReportDocument())
                {
                    rd.Load(Path.Combine(physicalReportPath, reportFile));

                    if (printDataSource != null)
                    {
                        rd.SetDataSource(printDataSource);
                    }

                    if (subPrintDataSource != null)
                    {
                        for (int i = 0; i < rd.Subreports.Count; i++)
                        {
                            rd.Subreports[i].DataSourceConnections.Clear();
                            rd.Subreports[i].SetDataSource(subPrintDataSource[i]);
                        }
                    }

                    string base64 = string.Empty;
                    using (Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        using (var ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            var ByteArray = ms.ToArray();
                            base64 = Convert.ToBase64String(ByteArray);
                        }
                    }

                    return base64;
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw ex;
            }
        }
    }
}
