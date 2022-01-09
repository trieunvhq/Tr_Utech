using AMIS.APIManagement;
using BPL.Factory.Web;
using BPL.Models.Web;
using BPL.Models.Web.Report;
using DAL;
using DAL.Factory.Web.SaleOrder;
using DAL.Factory.Web.TransactionHistory;
using HDLIB;
using HDLIB.Common;
using HDLIB.Helper;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL.Factory.Web.SaleOrder
{
    public class SaleOrderBLL: BaseBLL
    {
        public HDLIB.WebPaging.TPaging<SaleOrderModel> FindAllSaleOrder(int page, int limit,
            string exportStatus, string saleOrderNo, string wareHouseCode, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                HDLIB.WebPaging.TPaging<SaleOrderModel> pagging = new HDLIB.WebPaging.TPaging<SaleOrderModel>();

                var result = new SaleOrderItemManagement(db).FindAllSaleOrder(page, limit, exportStatus, saleOrderNo, wareHouseCode, startDate, endDate);
                List<SaleOrderModel> saleOrderModels = new List<SaleOrderModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var saleOrderModel = new SaleOrderModel();
                    saleOrderModel.CopyPropertiesFrom(row);
                    saleOrderModels.Add(saleOrderModel);
                }
                pagging.rows = saleOrderModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }

        public HDLIB.WebPaging.TPaging<SaleOrderItemModel> FindAllSaleOrderItem(int page, int limit,
            string saleOrderNo)
        {
            try
            {
                HDLIB.WebPaging.TPaging<SaleOrderItemModel> pagging = new HDLIB.WebPaging.TPaging<SaleOrderItemModel>();

                var result = new SaleOrderItemManagement(db).FindAllSaleOrderItem(page, limit, saleOrderNo);
                List<SaleOrderItemModel> saleOrderItemModels = new List<SaleOrderItemModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var saleOrderItemModel = new SaleOrderItemModel();
                    saleOrderItemModel.CopyPropertiesFrom(row);
                    saleOrderItemModels.Add(saleOrderItemModel);
                }
                pagging.rows = saleOrderItemModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }

        public SaleOrderModel GetSaleOrderBySaleOrderNo(string saleOrderNo)
        {
            try
            {
                var data = new SaleOrderItemManagement(db).FindByNo(saleOrderNo);
                SaleOrderModel _saleOrderModel = new SaleOrderModel();
                _saleOrderModel.CopyPropertiesFrom(data);

                return _saleOrderModel;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public SaleOrderItemModel GetSaleOrderItemById(int id)
        {
            try
            {
                var _SaleOrderItem = new SaleOrderItemManagement(db).FindByKey(id);
                if (_SaleOrderItem != null)
                {
                    SaleOrderItemModel _SaleOrderItemModel = new SaleOrderItemModel();
                    _SaleOrderItemModel.CopyPropertiesFrom(_SaleOrderItem);
                    return _SaleOrderItemModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public SaleOrderModel GetSaleOrderById(int id)
        {
            try
            {
                var _SaleOrder = new SaleOrderManagement(db).Select(id);
                if (_SaleOrder != null)
                {
                    SaleOrderModel _SaleOrderModel = new SaleOrderModel();
                    _SaleOrderModel.CopyPropertiesFrom(_SaleOrder);
                    return _SaleOrderModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int InsertSaleOrder(DAL.SaleOrder data, string userName)
        {
            try
            {
                //data.ID = 0;

                data.RecordStatus = ConstRecordStatus.New;
                if (data.CreateDate == null)
                {
                    data.CreateDate = DateTime.Now;
                }
                data.UserCreate = userName;

                return new SaleOrderManagement(db).Insert(data);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public int InsertSaleOrderItem(DAL.SaleOrderItem data, string userName)
        {
            try
            {
                //data.ID = 0;

                data.RecordStatus = ConstRecordStatus.New;
                if (data.CreateDate == null)
                {
                    data.CreateDate = DateTime.Now;
                }
                data.UserCreate = userName;

                return new SaleOrderItemManagement(db).Insert(data);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int UpdateSaleOrderItem(int id, DAL.SaleOrderItem data, string userName)
        {
            try
            {
                var saleOrderItemManagement = new SaleOrderItemManagement(db);
                var _origin = saleOrderItemManagement.FindByKey(id);
                if (_origin == null) return -1;
                _origin.CopyPropertiesFrom(data, true);
                _origin.UpdateDate = DateTime.Now;
                _origin.RecordStatus = ConstRecordStatus.Update;
                _origin.UserUpdate = userName;
                return saleOrderItemManagement.Update(_origin);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        #region import from AMIS
        public int ImportFromAMIS(string userName)
        {
            //get max entry
            int so_id = 0;
            int page = 0;
            int pagesize = 500;
            int total = 0;
            int count = 0;
            so_id = new SaleOrderItemManagement(db).GetMaxSaleOrderId();
            using (var transac = DataContext.GetTransaction(db))
            {
                try
                {
                    do
                    {
                        var result = new SaleOrderAPIManagement().GetSaleOrderInfo<List<SaleOrderItemInfoAmisModel>>(so_id, page, pagesize);
                        if (result.ResultCode != 0)
                        {
                            return -1;
                        }

                        foreach (var item in result.Data.PageData)
                        {
                            //validate
                            if (ValidateImport(item)) { 
                                //SaleOrder
                                ImportSaleOrder(item, userName);

                                //Sale order item
                                ImportSaleOrderItem(item, userName);
                            }
                        }
                        count = result.Data.PageData.Count;
                        total += count;
                        page += page;
                    } while (count > 0 && count == pagesize);

                    transac.Commit();
                    return total;
                }
                catch (Exception ex)
                {

                    transac.Rollback();
                    return -1;
                }
            }
            
        }

        private bool ValidateImport(SaleOrderItemInfoAmisModel saleOrderItemInfoAmisModel)
        {
            return !(saleOrderItemInfoAmisModel.SaleOrderID == null || saleOrderItemInfoAmisModel.SaleOrderID == 0
                || string.IsNullOrEmpty(saleOrderItemInfoAmisModel.SaleOrderNo)
                || string.IsNullOrEmpty(saleOrderItemInfoAmisModel.CustomerCode)
                || string.IsNullOrEmpty(saleOrderItemInfoAmisModel.WarehouseCode)
                || string.IsNullOrEmpty(saleOrderItemInfoAmisModel.ItemCode));
        }
        private int ImportSaleOrder(SaleOrderItemInfoAmisModel saleOrderItemInfoAmisModel, string userName)
        {
            var saleOrderManagement = new SaleOrderManagement(db);
            var exist = saleOrderManagement.GetSaleOrderBySaleOrderNo(saleOrderItemInfoAmisModel.SaleOrderNo);
            if (exist != null)
            {
                return 1;
            }
            DAL.SaleOrder saleOrder = new DAL.SaleOrder();
            saleOrder.SaleOrderNo = saleOrderItemInfoAmisModel.SaleOrderNo;
            saleOrder.SaleOrderDate = DateTimeHelper.ConvertStringDateTimeToDate(saleOrderItemInfoAmisModel.SaleOrderDate, "dd-MM-yyyy");
            saleOrder.ExportStatus = "Y";
            saleOrder.GetDataStatus = "Y";
            
            
            return InsertSaleOrder(saleOrder, userName);
        }
        private int ImportSaleOrderItem(SaleOrderItemInfoAmisModel saleOrderItemInfoAmisModel, string userName)
        {
            var saleOrderItemManagement = new SaleOrderItemManagement(db);
            var exist = saleOrderItemManagement.GetSaleOrderItemBy(saleOrderItemInfoAmisModel.SaleOrderID??0, saleOrderItemInfoAmisModel.SaleOrderNo,
                saleOrderItemInfoAmisModel.CustomerCode, saleOrderItemInfoAmisModel.WarehouseCode, saleOrderItemInfoAmisModel.ItemCode)?.FirstOrDefault();
            if (exist != null)
            {
                return 1;
            }
            DAL.SaleOrderItem saleOrderItem = new SaleOrderItem();
            saleOrderItem.SaleOrderID = saleOrderItemInfoAmisModel.SaleOrderID??0;
            saleOrderItem.SaleOrderNo = saleOrderItemInfoAmisModel.SaleOrderNo;
            saleOrderItem.SaleOrderDate = DateTimeHelper.ConvertStringDateTimeToDate(saleOrderItemInfoAmisModel.SaleOrderDate, "dd-MM-yyyy");
            saleOrderItem.DeliveryDate = DateTimeHelper.ConvertStringDateTimeToDate(saleOrderItemInfoAmisModel.DeliveryDate, "dd-MM-yyyy");
            saleOrderItem.CustomerCode = saleOrderItemInfoAmisModel.CustomerCode;
            saleOrderItem.CustomerName = saleOrderItemInfoAmisModel.CustomerName;
            saleOrderItem.LocationCode = saleOrderItemInfoAmisModel.WarehouseCode;
            saleOrderItem.LocationName = saleOrderItemInfoAmisModel.WarehouseName;

            saleOrderItem.ItemType = saleOrderItemInfoAmisModel.ItemType;
            saleOrderItem.ItemCode = saleOrderItemInfoAmisModel.ItemCode;
            saleOrderItem.ItemName = saleOrderItemInfoAmisModel.ItemName;
            saleOrderItem.Quantity = saleOrderItemInfoAmisModel.Quantity??0;
            saleOrderItem.Unit = saleOrderItemInfoAmisModel.Unit;
            return InsertSaleOrderItem(saleOrderItem, userName);
            
        }
        #endregion
        

        #region export excel
        public ReportResponseBase ExportToExcel(string saleOrderNo)
        {
            
            var lstDataSaleOrderItem = GetSaleOrderItemsForReport(saleOrderNo);

            var file_name = "Template_Xuatkhothucte.xlsx";
            string file_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            // Replace redundance in path
            var temp_file_name = file_path.Replace("file:\\", "").Replace("\\bin", "") + @"\storages\template\reports\" + file_name;

            var fileInfo = new FileInfo(temp_file_name);
            OfficeOpenXml.ExcelPackage package = null;
            using (package = new OfficeOpenXml.ExcelPackage(fileInfo))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    package.Workbook.Worksheets.Add("Detail");
                    //return null;
                }
                var worksheet = package.Workbook.Worksheets[1];
                var rowStart = 1;
                var colStart = 0;
                int rowIdx = rowStart;               

                for (int idx = 0; idx < lstDataSaleOrderItem.Count; idx++)
                {
                    var reportData = lstDataSaleOrderItem.ElementAt(idx);
                    rowIdx++;
                    worksheet.InsertRow(rowIdx, colStart+1);
                    worksheet.Cells[rowIdx, colStart + 1].Value = reportData.OrderNo;//Số đơn hàng
                    worksheet.Cells[rowIdx, colStart + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 2].Value = reportData.OrderDate.Value.ToString("dd-MM-yyyy");//Ngày đơn hàng
                    worksheet.Cells[rowIdx, colStart + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 3].Value = reportData.OrderDate.Value.ToString("dd-MM-yyyy");//Ngày xuất hàng
                    worksheet.Cells[rowIdx, colStart + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[rowIdx, colStart + 4].Value = reportData.WarehouseCode_To;//Mã kho
                    worksheet.Cells[rowIdx, colStart + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 5].Value = reportData.WarehouseName_To;//Tên Kho
                    worksheet.Cells[rowIdx, colStart + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 6].Value = reportData.CustomerName;//Mã khách hàng
                    worksheet.Cells[rowIdx, colStart + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                    worksheet.Cells[rowIdx, colStart + 7].Value = reportData.CustomerName;//Tên khahcs hàng
                    worksheet.Cells[rowIdx, colStart + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 8].Value = reportData.ItemCode;//Mã hàng
                    worksheet.Cells[rowIdx, colStart + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 9].Value = reportData.ItemName;//Tên hàng
                    worksheet.Cells[rowIdx, colStart + 9].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 10].Value = reportData.EXT_OtherCode;//Other code
                    worksheet.Cells[rowIdx, colStart + 10].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 11].Value = reportData.EXT_Serial;//Số serial
                    worksheet.Cells[rowIdx, colStart + 11].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 12].Value = reportData.EXT_LotNo;//Lot no
                    worksheet.Cells[rowIdx, colStart + 12].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 13].Value = reportData.EXT_PartNo;//part no
                    worksheet.Cells[rowIdx, colStart + 13].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 14].Value = reportData.EXT_MfDate.Value.ToString("dd-MM-yyyy");//MF date
                    worksheet.Cells[rowIdx, colStart + 14].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 15].Value = reportData.EXT_RecDate.Value.ToString("dd-MM-yyyy");//Rec date
                    worksheet.Cells[rowIdx, colStart + 15].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 16].Value = reportData.EXT_ExpDate.Value.ToString("dd-MM-yyyy");//Exp date
                    worksheet.Cells[rowIdx, colStart + 16].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 17].Value = reportData.Quantity;//Số lượng
                    worksheet.Cells[rowIdx, colStart + 17].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    worksheet.Cells[rowIdx, colStart + 18].Value = reportData.Unit;//Đơn vị tính
                    worksheet.Cells[rowIdx, colStart + 18].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                }
                package.Workbook.Properties.Title = file_name;
                return new ReportResponseBase(package.GetAsByteArray(), file_name, ReportResponseBase.EReportFileType.Excel);
            }

        }
        
        public List<TransactionHistoryModel> GetSaleOrderItemsForReport(string saleOrderNo)
        {
            var lstTransactionHistory = new TransactionHistoryManagement(db).GetAllBy(saleOrderNo,ConstTransactionType.XuatKho);
            List<TransactionHistoryModel> transactionHistoryModels = new List<TransactionHistoryModel>();
            foreach(var transactionHistory in lstTransactionHistory)
            {
                TransactionHistoryModel transactionHistoryModel = new TransactionHistoryModel();
                transactionHistoryModel.CopyPropertiesFrom(transactionHistory);
                transactionHistoryModels.Add(transactionHistoryModel);
            }
            return transactionHistoryModels;
        }
        #endregion
    }
}
