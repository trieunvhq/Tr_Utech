using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OfficeOpenXml;
using ClosedXML.Excel;
using System.Timers;

namespace HDLIB
{
    public static class Excel
    {
        #region Import
        public static DataTable GetDataRowFromExcel(string fileName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            FileInfo fileInfo = new FileInfo(fileName);
            OfficeOpenXml.ExcelPackage excel = new OfficeOpenXml.ExcelPackage(fileInfo);
            var ws = excel.Workbook.Worksheets.First();
            bool hasHeader = true; // adjust it accordingly( i've mentioned that this is a simple approach)
            foreach (var firstRowCell in ws.Cells[1, 1, 1, 255])  // 2:Số dòng bắt đầu - 1:Số cột bắt đầu - 2:số dòng kết thúc - 255:Số cột kết thúc
            {
                if (string.IsNullOrEmpty(firstRowCell.Text))
                {
                    break; //// no data
                }

                dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }
            var startRow = hasHeader ? 2 : 1;
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, dt.Columns.Count];//ws.Dimension.End.Column]; //EDIT 2016.04.27 - this not equal first column.
                var row = dt.NewRow();
                bool isNullRow = true;
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                    if (!string.IsNullOrEmpty(cell.Text))
                    {
                        isNullRow = false;
                    }
                }

                if (isNullRow)
                {
                    break;
                }

                dt.Rows.Add(row);
            }
            ws.Dispose();
            excel.Dispose();

            return dt;
        }
        public static System.Data.DataTable GetDataRowFromExcel(string fileName, string sheetName = "", int sheetIndex = 0, bool hasHeader = true, int headerRow = 1, int fromCol = 1, int toCol = 255)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            FileInfo fileInfo = new FileInfo(fileName);
            OfficeOpenXml.ExcelPackage excel = new OfficeOpenXml.ExcelPackage(fileInfo);
            var ws = excel.Workbook.Worksheets.FirstOrDefault(a => a.Name == sheetName || a.Index == sheetIndex) ?? excel.Workbook.Worksheets.FirstOrDefault();
            if (ws == null)
            {
                string xlsxFile = ConvertXLStoXLSX(fileName);
                if (string.IsNullOrEmpty(xlsxFile))
                {
                    return null;
                }
                else
                {
                    fileInfo = new System.IO.FileInfo(xlsxFile);
                    excel.Dispose();
                    excel = new OfficeOpenXml.ExcelPackage(fileInfo);
                    ws = excel.Workbook.Worksheets.FirstOrDefault(a => a.Name == sheetName || a.Index == sheetIndex) ?? excel.Workbook.Worksheets.FirstOrDefault();
                }
            }

            foreach (var firstRowCell in ws.Cells[headerRow, fromCol, headerRow, toCol])
            {
                if (string.IsNullOrEmpty(firstRowCell.Text))
                {
                    break;
                }
                dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }

            var startRow = hasHeader ? headerRow + 1 : headerRow;
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, fromCol, rowNum, fromCol + dt.Columns.Count];//ws.Dimension.End.Column]; //EDIT 2016.04.27 - this not equal first column.
                var row = dt.NewRow();
                bool isNullRow = true;
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                    if (!string.IsNullOrEmpty(cell.Text))
                    {
                        isNullRow = false;
                    }
                }

                if (isNullRow)
                {
                    break;
                }

                dt.Rows.Add(row);
            }
            ws.Dispose();
            excel.Dispose();

            return dt;
        }

        public static System.Data.DataTable GetDataRowFromExcel(Stream fileStream, string sheetName = "", int sheetIndex = 0, bool hasHeader = true, int headerRow = 1, int fromCol = 1, int toCol = 255)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            OfficeOpenXml.ExcelPackage excel = new OfficeOpenXml.ExcelPackage(fileStream);
            var ws = excel.Workbook.Worksheets.FirstOrDefault(a => a.Name == sheetName || a.Index == sheetIndex) ?? excel.Workbook.Worksheets.FirstOrDefault();

            foreach (var firstRowCell in ws.Cells[headerRow, fromCol, headerRow, toCol])
            {
                if (string.IsNullOrEmpty(firstRowCell.Text))
                {
                    break;
                }
                dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }

            var startRow = hasHeader ? headerRow + 1 : headerRow;
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, fromCol, rowNum, dt.Columns.Count];//ws.Dimension.End.Column]; //EDIT 2016.04.27 - this not equal first column.
                var row = dt.NewRow();
                bool isNullRow = true;
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                    if (!string.IsNullOrEmpty(cell.Text))
                    {
                        isNullRow = false;
                    }
                }

                if (isNullRow)
                {
                    break;
                }

                dt.Rows.Add(row);
            }
            ws.Dispose();
            excel.Dispose();

            return dt;
        }
        private static string ConvertXLStoXLSX(string filePath)
        {
            try
            {
                #region Interop            

                Microsoft.Office.Interop.Excel.Application xlApp = default(Microsoft.Office.Interop.Excel.Application);
                Microsoft.Office.Interop.Excel.Workbooks xlWorkbooks = default(Microsoft.Office.Interop.Excel.Workbooks);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = default(Microsoft.Office.Interop.Excel.Workbook);
                Microsoft.Office.Interop.Excel.Worksheet _workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);

                var xlsxFile = filePath + ".xlsx";
                try
                {
                    #region convert xls to xlsx
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlWorkbooks = xlApp.Workbooks;
                    xlWorkbook = xlWorkbooks.Open(filePath);
                    if (System.IO.File.Exists(xlsxFile))
                    {
                        System.IO.File.Delete(xlsxFile);
                    }
                    xlWorkbook.SaveAs(Filename: xlsxFile, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
                    #endregion
                }
                catch (Exception ex)
                {
                    HDLIB.Logging.LogError(ex);
                    xlsxFile = string.Empty;
                }
                finally
                {
                    #region cleanup Interop
                    //cleanup
                    if (_workSheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(_workSheet);
                    if (xlWorkbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
                    if (xlWorkbooks != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbooks);
                    xlApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    #endregion
                }
                #endregion
                return xlsxFile;
            }
            catch (Exception ex)
            {
                HDLIB.Logging.LogError(ex);
                return string.Empty;
            }
        }
        #endregion

        #region Export
        public static bool ExportDataToExcelFile(string[] header, IEnumerable<string[]> exportData, string sheetName, string filePath)
        {
            try
            {
                // Creating an instance of ExcelPackage 
                OfficeOpenXml.ExcelPackage excelPackage = new OfficeOpenXml.ExcelPackage();

                // name of the sheet 
                var workSheet = excelPackage.Workbook.Worksheets.Add(sheetName);

                // setting the properties of the work sheet  
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 15;

                // Setting the properties of the first row 
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Header of the Excel sheet 
                for (int i = 1; i <= header.Length; i++)
                {
                    workSheet.Cells[1, i].Value = header[i - 1];
                }
                // Data of the Excel sheet
                int rowCount = 2;
                foreach (var line in exportData)
                {
                    for (int i = 1; i <= line.Length; i++)
                    {
                        workSheet.Cells[rowCount, i].Value = line[i - 1];
                    }
                    rowCount++;
                }

                //workSheet.Column(1).AutoFit();

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                // Create excel file on physical disk  
                System.IO.FileStream objFileStrm = System.IO.File.Create(filePath);
                objFileStrm.Close();

                // Write content to excel file  
                System.IO.File.WriteAllBytes(filePath, excelPackage.GetAsByteArray());
                excelPackage.Dispose();
                workSheet.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                HDLIB.Logging.LogError(ex);
                return false;
            }
        }

        public static Stream ExportDataToExcelStream(string[] header, IEnumerable<string[]> exportData, string sheetName, int headerRow = 1, int startColunm = 1)
        {
            try
            {
                var stream = new MemoryStream();
                // Creating an instance of ExcelPackage 
                using (ExcelPackage excelPackage = new ExcelPackage(stream))
                {
                    // name of the sheet 
                    using (var workSheet = excelPackage.Workbook.Worksheets.Add(sheetName))
                    {

                        // setting the properties of the work sheet  
                        workSheet.TabColor = System.Drawing.Color.Black;
                        workSheet.DefaultRowHeight = 15;

                        // Header of the Excel sheet 
                        for (int i = 1; i <= header.Length; i++)
                        {
                            workSheet.Cells[headerRow, startColunm + i].Value = header[i - 1];
                        }
                        // Data of the Excel sheet
                        int rowCount = headerRow + 1;
                        foreach (var line in exportData)
                        {
                            exportData.GetEnumerator();
                            for (int i = 1; i <= line.Length; i++)
                            {
                                workSheet.Cells[rowCount, startColunm + i].Value = line[i - 1];
                            }
                            rowCount++;
                        }

                        //workSheet.Column(1).AutoFit();
                        excelPackage.Save();
                    }
                    return stream;
                }
            }
            catch (Exception ex)
            {
                HDLIB.Logging.LogError(ex);
                return null;
            }
        }

        public static Stream ExportDataToExcelStream(DataTable dataTable, string sheetName="Sheet1", int headerRow = 1, int startColunm = 1)
        {
            try
            {
                var stream = new MemoryStream();

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dataTable, sheetName);
                    IXLWorksheet workSheet = wb.Worksheet(sheetName);
                    IXLTable table = workSheet.Table(0);
                    workSheet.RowHeight = 20;
                    workSheet.Columns().AdjustToContents();
                    workSheet.Table(table.Name).ShowAutoFilter = false;
                    workSheet.ConditionalFormats.RemoveAll();
                    workSheet.Table(table.Name).ShowColumnStripes = false;
                    workSheet.Table(table.Name).ShowRowStripes = false;

                    wb.SaveAs(stream);
                }
                return stream;
            }
            catch (Exception ex)
            {
                HDLIB.Logging.LogError(ex);
                return null;
            }
        }

        public static void openTemplateExcelAndExport(string[] header, List<string[]> exportData, int headerRow = 1, int startColunm = 1, string sheetName = "", string fileTemplate = "")
        {
            #region Interop  

            Microsoft.Office.Interop.Excel.Application xlApp = default(Microsoft.Office.Interop.Excel.Application);
            Microsoft.Office.Interop.Excel.Workbooks xlWorkbooks = default(Microsoft.Office.Interop.Excel.Workbooks);
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = default(Microsoft.Office.Interop.Excel.Workbook);
            Microsoft.Office.Interop.Excel.Worksheet _workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);
            Microsoft.Office.Interop.Excel.Range cellRange = default(Microsoft.Office.Interop.Excel.Range);
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkbooks = xlApp.Workbooks;
                bool isTemplateExist = (string.IsNullOrEmpty(fileTemplate) && File.Exists(fileTemplate));
                xlWorkbook = (isTemplateExist) ? xlWorkbooks.Add(fileTemplate) : xlWorkbooks.Add();
                _workSheet = xlWorkbook.ActiveSheet;
                _workSheet.Name = (string.IsNullOrEmpty(sheetName)) ? "Sheet1" : sheetName;
                cellRange = _workSheet.Rows[1];
                cellRange.RowHeight = 60;

                Microsoft.Office.Interop.Excel.Style headerStyle = xlWorkbook.Styles.Add("header");
                headerStyle.Font.Size = 12;
                headerStyle.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                headerStyle.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                headerStyle.Font.Bold = true;
                headerStyle.WrapText = true;
                headerStyle.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);

                for (int i = 0; i < header.Length; i++)
                {
                    _workSheet.Cells[headerRow, startColunm + i] = header[i];
                    cellRange = _workSheet.Cells[headerRow, startColunm + i];
                    cellRange.Style = headerStyle;
                    cellRange.BorderAround2(LineStyle: Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Weight: Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                }

                for (int i = 0; i < exportData.Count; i++)
                {
                    for (int j = 0; j < exportData[i].Length; j++)
                    {
                        _workSheet.Cells[headerRow + 1 + i, startColunm + j] = exportData[i][j];
                    }
                }

                _workSheet.Columns.ColumnWidth = 20;
                _workSheet.Columns.AutoFit();
                xlApp.Visible = true;
            }
            catch (Exception ex)
            {
                HDLIB.Logging.LogError(ex);
            }
            finally
            {
                #region cleanup Interop
                //cleanup
                #endregion
            }
            #endregion

        }
        #endregion

        #region kill Excel Interop
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);

        private static void KillExcel(Microsoft.Office.Interop.Excel.Application excelApp)
        {
            int id = 0;
            IntPtr intptr = new IntPtr(excelApp.Hwnd);
            System.Diagnostics.Process process = null;
            try
            {
                GetWindowThreadProcessId(intptr, out id);
                process = System.Diagnostics.Process.GetProcessById(id);
                if (process != null)
                {
                    //Clean up to make sure the background Excel process is terminated.
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    process.Kill();
                    process.Dispose();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch
            {
                process.Dispose();
            }
        }

        private static void KillOpenedExcelApplication(
            Microsoft.Office.Interop.Excel.Application excelApp,
            Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks,
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook,
            Microsoft.Office.Interop.Excel.Worksheet _workSheet,
            Microsoft.Office.Interop.Excel.Range cellRange=null
            )
        {
            int id = 0;
            IntPtr intptr = new IntPtr(excelApp.Hwnd);
            System.Diagnostics.Process process = null;
            try
            {
                GetWindowThreadProcessId(intptr, out id);
                process = System.Diagnostics.Process.GetProcessById(id);

                if (process != null)
                {
                    var timer = new Timer
                    {
                        Interval = 1000
                    };
                    timer.Elapsed += delegate
                    {
                        if (excelApp != null && !excelApp.Visible)
                        {
                            //Clean up to make sure the background Excel process is terminated.
                            if (cellRange != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(cellRange);
                            if (_workSheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(_workSheet);
                            if (excelWorkbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
                            if (excelWorkbooks != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbooks);
                            excelApp.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                            process.Kill();
                            process.Dispose();
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            timer.Stop();
                            timer.Dispose();
                        }
                    };
                    timer.Start();
                }
            }
            catch
            {
                process.Dispose();
            }
        }
        #endregion

        public class ExcelExportParams
        {
            public string[] headers;
            public List<string[]> exportData;
            public Stream importStream;
            public int headerRow;
            public int startCol;
            public int endCol;
            public int firstPageRows;
        }
    }
}
