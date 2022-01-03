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

namespace BLL.Factory.Web.PurchaseOrder
{
    public class PurchaseOrderBLL : BaseBLL
    {

        public HDLIB.WebPaging.TPaging<PurchaseOrderItemModel> FindAll(int page, int limit,
            string itemCode, string itemName, string purchaseOrderNo, string locationCode,
            DateTime? startDate, DateTime? endDate, bool isSearch)
        {
            try
            {
                HDLIB.WebPaging.TPaging<PurchaseOrderItemModel> pagging = new HDLIB.WebPaging.TPaging<PurchaseOrderItemModel>();

                var result = new PurchaseOrderItemManagement(db).FindAll(page, limit, itemCode, itemName,
                    purchaseOrderNo, locationCode, startDate, endDate, isSearch);
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

                data.RecordStatus = RecordStatus.New;
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

                data.RecordStatus = RecordStatus.New;
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
                _origin.RecordStatus = RecordStatus.Update;
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
            var exist = purchaseOrderItemManagement.GetPurchaseOrderItemBy(purchaseOrderItemInfoAmisModel.PurchaseOrderID ?? 0, purchaseOrderItemInfoAmisModel.PurchaseOrderNo,
                purchaseOrderItemInfoAmisModel.SupplierCode, purchaseOrderItemInfoAmisModel.WarehouseCode, purchaseOrderItemInfoAmisModel.ItemCode).FirstOrDefault();
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
        public ReportResponseBase ExportToExcel(int? purchaseOrderItemId)
        {

            var lstDataPurchaseOrderItem = GetPurchaseOrderItemsForReport(purchaseOrderItemId, null, null, null, null);

            var file_name = "PurchaseOrderItemDetail.xlsx";
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

                for (int idx = 0; idx < lstDataPurchaseOrderItem.Count; idx++)
                {
                    var reportData = lstDataPurchaseOrderItem.ElementAt(idx);
                    rowIdx++;
                    worksheet.InsertRow(rowIdx, colStart + 1);
                    worksheet.Cells[rowIdx, colStart + 1].Value = rowIdx - rowStart; //stt
                    worksheet.Cells[rowIdx, colStart + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[rowIdx, colStart + 2].Value = reportData.PurchaseOrderNo;
                    worksheet.Cells[rowIdx, colStart + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 3].Value = reportData.PurchaseOrderDate;
                    worksheet.Cells[rowIdx, colStart + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[rowIdx, colStart + 4].Value = reportData.ItemName;
                    worksheet.Cells[rowIdx, colStart + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 5].Value = reportData.ItemCode;
                    worksheet.Cells[rowIdx, colStart + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 6].Value = reportData.SupplierName;
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

        public List<PurchaseOrderItemModel> GetPurchaseOrderItemsForReport(int? purchaseOrderId, string purchaseOrderNo,
            string customerCode, string locationCode, string itemCode)
        {
            var lstpurchaseOrderItem = new PurchaseOrderItemManagement(db).GetPurchaseOrderItemBy(purchaseOrderId, purchaseOrderNo,
                customerCode, locationCode, itemCode);
            List<PurchaseOrderItemModel> purchaseOrderItemModels = new List<PurchaseOrderItemModel>();
            foreach (var purchaseOrderItem in lstpurchaseOrderItem)
            {
                PurchaseOrderItemModel purchaseOrderItemModel = new PurchaseOrderItemModel();
                purchaseOrderItemModel.CopyPropertiesFrom(purchaseOrderItem);
                purchaseOrderItemModels.Add(purchaseOrderItemModel);
            }
            return purchaseOrderItemModels;
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
