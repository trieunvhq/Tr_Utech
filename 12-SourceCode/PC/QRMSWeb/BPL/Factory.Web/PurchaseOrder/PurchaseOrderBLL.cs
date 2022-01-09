using AMIS.APIManagement;
using BPL.Factory.Web;
using BPL.Models.Web;
using BPL.Models.Web.Report;
using DAL;
using DAL.Factory.Web.PurchaseOrder;
using HDLIB;
using HDLIB.Common;
using OfficeOpenXml.Style;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using DAL.Factory.Web.TransactionHistory;
using HDLIB.Helper;

namespace BLL.Factory.Web.PurchaseOrder
{
    public class PurchaseOrderBLL : BaseBLL
    {

        public HDLIB.WebPaging.TPaging<PurchaseOrderModel> FindAllPurchaseOrder(int page, int limit,
            string wareHouseCode, string purchaseOrderNo, string inputStatus,
            DateTime? startDate, DateTime? endDate, bool isSearch)
        {
            try
            {
                HDLIB.WebPaging.TPaging<PurchaseOrderModel> pagging = new HDLIB.WebPaging.TPaging<PurchaseOrderModel>();

                var result = new PurchaseOrderItemManagement(db).FindAllPurchaseOrder(page, limit, wareHouseCode,
                    purchaseOrderNo, inputStatus, startDate, endDate, isSearch);
                List<PurchaseOrderModel> purchaseOrderModels = new List<PurchaseOrderModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var purchaseOrderModel = new PurchaseOrderModel();
                    purchaseOrderModel.CopyPropertiesFrom(row);
                    purchaseOrderModels.Add(purchaseOrderModel);
                }
                pagging.rows = purchaseOrderModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }
        public HDLIB.WebPaging.TPaging<PurchaseOrderItemModel> FindAllPurchaseOrderItem(int page, int limit,
            string purchaseOrderNo)
        {
            try
            {
                HDLIB.WebPaging.TPaging<PurchaseOrderItemModel> pagging = new HDLIB.WebPaging.TPaging<PurchaseOrderItemModel>();

                var result = new PurchaseOrderItemManagement(db).FindAllPurchaseOrderItem(page, limit,
                    purchaseOrderNo);
                List<PurchaseOrderItemModel> purchaseOrderItemModels = new List<PurchaseOrderItemModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var purchaseOrderItemModel = new PurchaseOrderItemModel();
                    purchaseOrderItemModel.CopyPropertiesFrom(row);
                    purchaseOrderItemModels.Add(purchaseOrderItemModel);
                }
                pagging.rows = purchaseOrderItemModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }

        
        public PurchaseOrderItemModel GetPurchaseOrderItemById(int id)
        {
            try
            {
                var _purchaseOrderItem = new PurchaseOrderItemManagement(db).FindByKey(id);
                if (_purchaseOrderItem != null)
                {
                    PurchaseOrderItemModel _purchaseOrderItemModel = new PurchaseOrderItemModel();
                    _purchaseOrderItemModel.CopyPropertiesFrom(_purchaseOrderItem);
                    return _purchaseOrderItemModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public PurchaseOrderModel  GetPurchaseOrderByPurchaseOrderNo(string purchaseOrderNo)
        {
            try
            {
                var data = new PurchaseOrderItemManagement(db).FindByNo(purchaseOrderNo);
                PurchaseOrderModel _purchaseOrderModel = new PurchaseOrderModel();
                _purchaseOrderModel.CopyPropertiesFrom(data);
                
                return _purchaseOrderModel;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public PurchaseOrderModel GetPurchaseOrderById(int id)
        {
            try
            {
                var _purchaseOrder = new PurchaseOrderManagement(db).Select(id);
                if (_purchaseOrder != null)
                {
                    PurchaseOrderModel _purchaseOrderModel = new PurchaseOrderModel();
                    _purchaseOrderModel.CopyPropertiesFrom(_purchaseOrder);
                    return _purchaseOrderModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int InsertPurchaseOrder(DAL.PurchaseOrder data, string userName)
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

                return new PurchaseOrderManagement(db).Insert(data);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public int InsertPurchaseOrderItem(DAL.PurchaseOrderItem data, string userName)
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

                return new PurchaseOrderItemManagement(db).Insert(data);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int UpdatepurchaseOrderItem(int id, DAL.PurchaseOrderItem data, string userName)
        {
            try
            {
                var purchaseOrderItemManagement = new PurchaseOrderItemManagement(db);
                var _origin = purchaseOrderItemManagement.FindByKey(id);
                if (_origin == null) return -1;
                _origin.CopyPropertiesFrom(data, true);
                _origin.UpdateDate = DateTime.Now;
                _origin.RecordStatus = ConstRecordStatus.Update;
                _origin.UserUpdate = userName;
                return purchaseOrderItemManagement.Update(_origin);
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
            so_id = new PurchaseOrderItemManagement(db).GetMaxpurchaseOrderId();
            using (var transac = DataContext.GetTransaction(db))
            {
                try
                {
                    do
                    {
                        var result = new PurchaseOrderAPIManagement().GetPurchaseOrderInfo<List<PurchaseOrderItemInfoAmisModel>>(so_id, page, pagesize);
                        if (result.ResultCode != 0)
                        {
                            return -1;
                        }

                        foreach (var item in result.Data.PageData)
                        {
                            //validate
                            if (ValidateImport(item))
                            {
                                //purchaseOrder
                                ImportPurchaseOrder(item, userName);

                                //Sale order item
                                ImportPurchaseOrderItem(item, userName);
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

        private bool ValidateImport(PurchaseOrderItemInfoAmisModel purchaseOrderItemInfoAmisModel)
        {
            return !(purchaseOrderItemInfoAmisModel.PurchaseOrderID == null || purchaseOrderItemInfoAmisModel.PurchaseOrderID == 0
                || string.IsNullOrEmpty(purchaseOrderItemInfoAmisModel.PurchaseOrderNo)
                || string.IsNullOrEmpty(purchaseOrderItemInfoAmisModel.SupplierCode)
                || string.IsNullOrEmpty(purchaseOrderItemInfoAmisModel.WarehouseCode)
                || string.IsNullOrEmpty(purchaseOrderItemInfoAmisModel.ItemCode));
        }
        private int ImportPurchaseOrder(PurchaseOrderItemInfoAmisModel purchaseOrderItemInfoAmisModel, string userName)
        {
            var purchaseOrderManagement = new PurchaseOrderManagement(db);
            var exist = purchaseOrderManagement.GetpurchaseOrderBypurchaseOrderNo(purchaseOrderItemInfoAmisModel.PurchaseOrderNo);
            if (exist != null)
            {
                return 1;
            }
            DAL.PurchaseOrder purchaseOrder = new DAL.PurchaseOrder();
            purchaseOrder.PurchaseOrderNo = purchaseOrderItemInfoAmisModel.PurchaseOrderNo;
            purchaseOrder.PurchaseOrderDate = DateTimeHelper.ConvertStringDateTimeToDate(purchaseOrderItemInfoAmisModel.PurchaseOrderDate, "dd-MM-yyyy");
            purchaseOrder.ExportStatus = "Y";
            purchaseOrder.InputStatus = "N";
            purchaseOrder.PrintStatus = "N";
            purchaseOrder.GetDataStatus = "Y";

            return InsertPurchaseOrder(purchaseOrder, userName);
        }
        private int ImportPurchaseOrderItem(PurchaseOrderItemInfoAmisModel purchaseOrderItemInfoAmisModel, string userName)
        {
            var purchaseOrderItemManagement = new PurchaseOrderItemManagement(db);
            var exist = purchaseOrderItemManagement.GetPurchaseOrderItemBy(purchaseOrderItemInfoAmisModel.PurchaseOrderNo).FirstOrDefault();
            if (exist != null)
            {
                return 1;
            }
            DAL.PurchaseOrderItem purchaseOrderItem = new PurchaseOrderItem();
            purchaseOrderItem.PurchaseOrderID = purchaseOrderItemInfoAmisModel.PurchaseOrderID ?? 0;
            purchaseOrderItem.PurchaseOrderNo = purchaseOrderItemInfoAmisModel.PurchaseOrderNo;
            purchaseOrderItem.PurchaseOrderDate = DateTimeHelper.ConvertStringDateTimeToDate(purchaseOrderItemInfoAmisModel.PurchaseOrderDate, "dd-MM-yyyy");
            purchaseOrderItem.ItemCode = purchaseOrderItemInfoAmisModel.ItemCode;
            purchaseOrderItem.ItemName = purchaseOrderItemInfoAmisModel.ItemName;
            purchaseOrderItem.ItemType = purchaseOrderItemInfoAmisModel.ItemType;
            purchaseOrderItem.Quantity = purchaseOrderItemInfoAmisModel.Quantity ?? 0;
            purchaseOrderItem.Unit = purchaseOrderItemInfoAmisModel.Unit;
            purchaseOrderItem.LocationCode = purchaseOrderItemInfoAmisModel.WarehouseCode;
            purchaseOrderItem.LocationName = purchaseOrderItemInfoAmisModel.WarehouseName;
            purchaseOrderItem.SupplierCode = purchaseOrderItemInfoAmisModel.SupplierCode;
            purchaseOrderItem.SupplierName = purchaseOrderItemInfoAmisModel.SupplierName;
            purchaseOrderItem.InputStatus = "N";
            purchaseOrderItem.PrintStatus = "N";

            return InsertPurchaseOrderItem(purchaseOrderItem, userName);

        }
        #endregion


        #region export excel
        public ReportResponseBase ExportToExcel(string purchaseOrderNo)
        {

            var lstDataPurchaseOrderItem = GetPurchaseOrderItemsForReport(purchaseOrderNo);

            var file_name = "Template_Nhapkhothucte.xlsx";
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

                for (int idx = 0; idx < lstDataPurchaseOrderItem.Count; idx++)
                {
                    var reportData = lstDataPurchaseOrderItem.ElementAt(idx);
                    rowIdx++;
                    worksheet.InsertRow(rowIdx, colStart + 1);

                    worksheet.Cells[rowIdx, colStart + 1].Value = reportData.OrderNo;//Số đơn mua hàng
                    worksheet.Cells[rowIdx, colStart + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 2].Value = reportData.OrderDate.Value.ToString("dd-MM-yyyy"); // Ngày đơn mua hàng
                    worksheet.Cells[rowIdx, colStart + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[rowIdx, colStart + 3].Value = reportData.OrderDate.Value.ToString("dd-MM-yyyy");//Ngày nhập kho
                    worksheet.Cells[rowIdx, colStart + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 4].Value = reportData.WarehouseCode_To;//Mã kho
                    worksheet.Cells[rowIdx, colStart + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 5].Value = reportData.WarehouseName_To;//Tên kho
                    worksheet.Cells[rowIdx, colStart + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 6].Value = reportData.SupplierCode;//Mã nhà cung cấp
                    worksheet.Cells[rowIdx, colStart + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 7].Value = reportData.SupplierName;//Tên nhà cung cấp
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

                    worksheet.Cells[rowIdx, colStart + 12].Value = reportData.EXT_PartNo;//LotNo
                    worksheet.Cells[rowIdx, colStart + 12].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 13].Value = reportData.EXT_LotNo;//LotNo
                    worksheet.Cells[rowIdx, colStart + 13].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 14].Value = reportData.EXT_MfDate.Value.ToString("dd-MM-yyyy");//MfDate
                    worksheet.Cells[rowIdx, colStart + 14].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[rowIdx, colStart + 15].Value = reportData.EXT_RecDate.Value.ToString("dd-MM-yyyy");//RecDate
                    worksheet.Cells[rowIdx, colStart + 15].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[rowIdx, colStart + 16].Value = reportData.EXT_ExpDate.Value.ToString("dd-MM-yyyy");//ExpDate
                    worksheet.Cells[rowIdx, colStart + 16].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[rowIdx, colStart + 17].Value = reportData.Quantity;//ExpDate
                    worksheet.Cells[rowIdx, colStart + 17].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    worksheet.Cells[rowIdx, colStart + 18].Value = reportData.Unit;//ExpDate
                    worksheet.Cells[rowIdx, colStart + 18].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                package.Workbook.Properties.Title = file_name;
                return new ReportResponseBase(package.GetAsByteArray(), file_name, ReportResponseBase.EReportFileType.Excel);
            }
        }
        public List<TransactionHistoryModel> GetPurchaseOrderItemsForReport(string purchaseOrderNo)
        {
            var lstpurchaseOrderItem = new TransactionHistoryManagement(db).GetAllBy(purchaseOrderNo, ConstTransactionType.NhapKho);
            List<TransactionHistoryModel> transactionHistoryModels = new List<TransactionHistoryModel>();
            foreach (var purchaseOrderItem in lstpurchaseOrderItem)
            {
                TransactionHistoryModel transactionHistoryModel = new TransactionHistoryModel();
                transactionHistoryModel.CopyPropertiesFrom(purchaseOrderItem);
                transactionHistoryModels.Add(transactionHistoryModel);
            }
            return transactionHistoryModels;
        }
        #endregion
        #region print
        public HDLIB.WebPaging.TPaging<PurchaseOrderItemModel> FindAllPurchaseOrderPrint(int page, int limit,
            string itemType, string wareHouseCode, string purchaseOrderNo, string printStatus,
            DateTime? purchaseOrderDate, bool isSearch)
        {
            try
            {
                HDLIB.WebPaging.TPaging<PurchaseOrderItemModel> pagging = new HDLIB.WebPaging.TPaging<PurchaseOrderItemModel>();

                var result = new PurchaseOrderItemManagement(db).FindAllPurchaseOrderPrint(page, limit, itemType, wareHouseCode,
                    purchaseOrderNo, printStatus, purchaseOrderDate, isSearch);
                List<PurchaseOrderItemModel> purchaseOrderItemModels = new List<PurchaseOrderItemModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var purchaseOrderItemModel = new PurchaseOrderItemModel();
                    purchaseOrderItemModel.CopyPropertiesFrom(row);
                    purchaseOrderItemModels.Add(purchaseOrderItemModel);
                }
                pagging.rows = purchaseOrderItemModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }
        #endregion
    }


}
