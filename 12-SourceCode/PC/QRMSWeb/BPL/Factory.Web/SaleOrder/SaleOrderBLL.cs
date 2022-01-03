using BPL.Factory.Web;
using BPL.Models.Web;
using DAL;
using DAL.Factory.Web.Users;
using DAL.Factory.Web.SaleOrder;
using HDLIB;
using HDLIB.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using AMIS.APIManagement;
using DAL.Factory.Web.SaleOrderItems;
using BPL.Models.Web.Report;
using System.IO;
using OfficeOpenXml.Style;

namespace BLL.Factory.Web.SaleOrder
{
    public class SaleOrderBLL: BaseBLL
    {
        public HDLIB.WebPaging.TPaging<SaleOrderItemModel> FindAll(int page, int limit,
            string itemCode, string itemName, string saleOrderNo, string locationName, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                HDLIB.WebPaging.TPaging<SaleOrderItemModel> pagging = new HDLIB.WebPaging.TPaging<SaleOrderItemModel>();

                var result = new SaleOrderItemManagement(db).FindAll(page, limit, itemCode, itemName, saleOrderNo, locationName, startDate, endDate);
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

                data.RecordStatus = RecordStatus.New;
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

                data.RecordStatus = RecordStatus.New;
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
                _origin.RecordStatus = RecordStatus.Update;
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
        public ReportResponseBase ExportToExcel(int? saleOrderItemId)
        {
            
            var lstDataSaleOrderItem = GetSaleOrderItemsForReport(saleOrderItemId, null, null, null, null);

            var file_name = "SaleOrderItemDetail.xlsx";
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
                var rowStart = 2;
                var colStart = 1;
                int rowIdx = rowStart;               

                for (int idx = 0; idx < lstDataSaleOrderItem.Count; idx++)
                {
                    var reportData = lstDataSaleOrderItem.ElementAt(idx);
                    rowIdx++;
                    worksheet.InsertRow(rowIdx, colStart+1);
                    worksheet.Cells[rowIdx, colStart + 1].Value = rowIdx - rowStart; //stt
                    worksheet.Cells[rowIdx, colStart + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[rowIdx, colStart + 2].Value = reportData.SaleOrderNo;
                    worksheet.Cells[rowIdx, colStart + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 3].Value = reportData.SaleOrderDate;
                    worksheet.Cells[rowIdx, colStart + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[rowIdx, colStart + 4].Value = reportData.ItemName;
                    worksheet.Cells[rowIdx, colStart + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 5].Value = reportData.ItemCode;
                    worksheet.Cells[rowIdx, colStart + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 6].Value = reportData.CustomerName;
                    worksheet.Cells[rowIdx, colStart + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                    worksheet.Cells[rowIdx, colStart + 7].Value = reportData.Quantity;
                    worksheet.Cells[rowIdx, colStart + 7].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    worksheet.Cells[rowIdx, colStart + 8].Value = reportData.Unit;
                    worksheet.Cells[rowIdx, colStart + 8].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    // worksheet.Cells[rowIdx, 8 + (monthValues.Count - 1) * 3].Style.Font.Bold = true;

                }
                package.Workbook.Properties.Title = file_name;
                return new ReportResponseBase(package.GetAsByteArray(), file_name, ReportResponseBase.EReportFileType.Excel);
            }

        }
        
        public List<SaleOrderItemModel> GetSaleOrderItemsForReport(int? saleOrderId, string saleOrderNo, 
            string customerCode, string locationCode, string itemCode )
        {
            var lstSaleOrderItem = new SaleOrderItemManagement(db).GetSaleOrderItemBy(saleOrderId, saleOrderNo,
                customerCode, locationCode, itemCode);
            List<SaleOrderItemModel> saleOrderItemModels = new List<SaleOrderItemModel>();
            foreach(var saleOrderItem in lstSaleOrderItem)
            {
                SaleOrderItemModel saleOrderItemModel = new SaleOrderItemModel();
                saleOrderItemModel.CopyPropertiesFrom(saleOrderItem);
                saleOrderItemModels.Add(saleOrderItemModel);
            }
            return saleOrderItemModels;
        }
        #endregion
    }
}
