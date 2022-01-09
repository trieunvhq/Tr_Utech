using AMIS.APIManagement;
using BPL.Factory.Web;
using BPL.Models.Web;
using BPL.Models.Web.Report;
using DAL;
using DAL.Factory.Web.TransferInstruction;
using DAL.Factory.Web.TransactionHistory;
using HDLIB;
using HDLIB.Common;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HDLIB.Helper;

namespace BLL.Factory.Web.TransferInstruction
{
    public class TransferInstructionBLL : BaseBLL
    {
        public HDLIB.WebPaging.TPaging<TransferInstructionModel> FindAllTransferInstruction(int page, int limit, string transferType,
            string tranferNo, string wareHouseCode_from, string wareHouseCode_to, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                HDLIB.WebPaging.TPaging<TransferInstructionModel> pagging = new HDLIB.WebPaging.TPaging<TransferInstructionModel>();

                var result = new TransferInstructionItemManagement(db).FindAllTransferInstruction(page, limit, transferType, tranferNo, wareHouseCode_from, wareHouseCode_to, startDate, endDate);
                List<TransferInstructionModel> tranferModels = new List<TransferInstructionModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var tranferModel = new TransferInstructionModel();
                    tranferModel.CopyPropertiesFrom(row);
                    tranferModels.Add(tranferModel);
                }
                pagging.rows = tranferModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }

        public HDLIB.WebPaging.TPaging<TransferInstructionItemModel> FindAllTransferInstructionItem(int page, int limit,string transferType,
            string tranferNo)
        {
            try
            {
                HDLIB.WebPaging.TPaging<TransferInstructionItemModel> pagging = new HDLIB.WebPaging.TPaging<TransferInstructionItemModel>();

                var result = new TransferInstructionItemManagement(db).FindAllTransferInstructionItem(page, limit, transferType, tranferNo);
                List<TransferInstructionItemModel> tranferItemModels = new List<TransferInstructionItemModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var tranferItemModel = new TransferInstructionItemModel();
                    tranferItemModel.CopyPropertiesFrom(row);
                    tranferItemModels.Add(tranferItemModel);
                }
                pagging.rows = tranferItemModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }

        public TransferInstructionModel GetTransferInstructionByTranferNo(string tranferNo, string transferType)
        {
            try
            {
                var data = new TransferInstructionItemManagement(db).FindByNo(tranferNo, transferType);
                TransferInstructionModel _tranferModel = new TransferInstructionModel();
                _tranferModel.CopyPropertiesFrom(data);

                return _tranferModel;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public SaleOrderItemModel GetTransferInstructionItemById(int id)
        {
            try
            {
                var _tranferItem = new TransferInstructionItemManagement(db).FindByKey(id);
                if (_tranferItem != null)
                {
                    SaleOrderItemModel _tranferItemModel = new SaleOrderItemModel();
                    _tranferItemModel.CopyPropertiesFrom(_tranferItem);
                    return _tranferItemModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public SaleOrderModel GetTransferInstructionById(int id)
        {
            try
            {
                var _tranfer = new TransferInstructionManagement(db).Select(id);
                if (_tranfer != null)
                {
                    SaleOrderModel _tranferModel = new SaleOrderModel();
                    _tranferModel.CopyPropertiesFrom(_tranfer);
                    return _tranferModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int InsertTranfer(DAL.TransferInstruction data, string userName)
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

                return new TransferInstructionManagement(db).Insert(data);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public int InsertTranferItem(DAL.TransferInstructionItem data, string userName)
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

                return new TransferInstructionItemManagement(db).Insert(data);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int UpdateTranferItem(int id, DAL.SaleOrderItem data, string userName)
        {
            try
            {
                var TranferItemManagement = new TransferInstructionItemManagement(db);
                var _origin = TranferItemManagement.FindByKey(id);
                if (_origin == null) return -1;
                _origin.CopyPropertiesFrom(data, true);
                _origin.UpdateDate = DateTime.Now;
                _origin.RecordStatus = ConstRecordStatus.Update;
                _origin.UserUpdate = userName;
                return TranferItemManagement.Update(_origin);
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
            so_id = new TransferInstructionItemManagement(db).GetMaxTransferInstructionId();
            using (var transac = DataContext.GetTransaction(db))
            {
                try
                {
                    do
                    {
                        var result = new TransferInstructionAPIManagement().GetTransferInstructionInfo<List<TransferInstructionItemInfoAmisModel>>(so_id, page, pagesize);
                        if (result.ResultCode != 0)
                        {
                            return -1;
                        }

                        foreach (var item in result.Data.PageData)
                        {
                            //validate
                            if (ValidateImport(item)) {
                                //Tranfer
                                var transferInstructionId = ImportTransferInstruction(item, userName);
                                if (transferInstructionId <= 0)
                                {
                                    throw new Exception("Save Transfer Instruction lỗi");
                                }
                                //Sale order item
                                item.TransferOrderID = transferInstructionId;
                                if (ImportTranferItem(item, userName) < 0) throw new Exception("Save Transfer Instruction Item lỗi");
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
                    throw;
                }
            }
            
        }

        private bool ValidateImport(TransferInstructionItemInfoAmisModel _transferInstructiontemInfoAmisModel)
        {
            return !(_transferInstructiontemInfoAmisModel.TransferOrderID == null || _transferInstructiontemInfoAmisModel.TransferOrderID == 0
                || string.IsNullOrEmpty(_transferInstructiontemInfoAmisModel.TransferOrderNo)
                || string.IsNullOrEmpty(_transferInstructiontemInfoAmisModel.TransferOrderType)
                || string.IsNullOrEmpty(_transferInstructiontemInfoAmisModel.FWarehouseCode)
                || string.IsNullOrEmpty(_transferInstructiontemInfoAmisModel.TWarehouseCode)
                || string.IsNullOrEmpty(_transferInstructiontemInfoAmisModel.ItemCode));
        }
        private int ImportTransferInstruction(TransferInstructionItemInfoAmisModel _transferInstructiontemInfoAmisModel, string userName)
        {
            var transferManagement = new TransferInstructionManagement(db);
            var exist = transferManagement.GetTransferInstructionByTranferNo(_transferInstructiontemInfoAmisModel.TransferOrderNo,_transferInstructiontemInfoAmisModel.TransferOrderType);
            if (exist != null)
            {
                return exist.ID;
            }
            DAL.TransferInstruction _transferInstruction = new DAL.TransferInstruction();
            _transferInstruction.TransferOrderAmisID = _transferInstructiontemInfoAmisModel.TransferOrderID;
            _transferInstruction.TransferOrderNo = _transferInstructiontemInfoAmisModel.TransferOrderNo;
            _transferInstruction.TransferType = _transferInstructiontemInfoAmisModel.TransferOrderType;
            _transferInstruction.WarehouseCode_From = _transferInstructiontemInfoAmisModel.FWarehouseCode;
            _transferInstruction.WarehouseName_From = _transferInstructiontemInfoAmisModel.FWarehouseName;
            _transferInstruction.WarehouseCode_To = _transferInstructiontemInfoAmisModel.TWarehouseCode;
            _transferInstruction.WarehouseName_To = _transferInstructiontemInfoAmisModel.TWarehouseName;
            _transferInstruction.InstructionDate = DateTimeHelper.ConvertStringDateTimeToDate(_transferInstructiontemInfoAmisModel.TransferOrderDate, "dd-MM-yyyy");
            _transferInstruction.TransferStatus = "N";
            _transferInstruction.GetDataStatus = "Y";
            
            
            return InsertTranfer(_transferInstruction, userName);
        }
        private int ImportTranferItem(TransferInstructionItemInfoAmisModel tranferItemInfoAmisModel, string userName)
        {
            var transferItemManagement = new TransferInstructionItemManagement(db);
            var exist = transferItemManagement.GetTransferInstructionItemBy(tranferItemInfoAmisModel.TransferOrderNo, tranferItemInfoAmisModel.TransferOrderType)?.FirstOrDefault();
            if (exist != null)
            {
                return exist.ID;
            }
            DAL.TransferInstructionItem transferInstructionItem = new TransferInstructionItem();
            transferInstructionItem.TransferOrderID = tranferItemInfoAmisModel.TransferOrderID ?? 0;
            transferInstructionItem.TransferOrderNo = tranferItemInfoAmisModel.TransferOrderNo;
            transferInstructionItem.TransferType = tranferItemInfoAmisModel.TransferOrderType;
            transferInstructionItem.InstructionDate = DateTimeHelper.ConvertStringDateTimeToDate(tranferItemInfoAmisModel.TransferOrderDate, "dd-MM-yyyy");
            transferInstructionItem.WarehouseCode_From = tranferItemInfoAmisModel.FWarehouseCode;
            transferInstructionItem.WarehouseName_From = tranferItemInfoAmisModel.FWarehouseName;
            transferInstructionItem.WarehouseName_To = tranferItemInfoAmisModel.TWarehouseName;
            transferInstructionItem.WarehouseCode_To = tranferItemInfoAmisModel.TWarehouseCode;
            transferInstructionItem.TransferStatus = "N";
            transferInstructionItem.GetDataStatus = "Y";

            transferInstructionItem.ItemType = tranferItemInfoAmisModel.ItemType;
            transferInstructionItem.ItemCode = tranferItemInfoAmisModel.ItemCode;
            transferInstructionItem.ItemName = tranferItemInfoAmisModel.ItemName;
            transferInstructionItem.Quantity = tranferItemInfoAmisModel.Quantity??0;
            transferInstructionItem.Unit = tranferItemInfoAmisModel.Unit;
            return InsertTranferItem(transferInstructionItem, userName);
            
        }
        #endregion
        

        #region export excel
        public ReportResponseBase ExportToExcel(string transferNo,string transferTyper)
        {
            
            var lstDataSaleOrderItem = GetTransactionItemsForReport(transferNo, transferTyper);

            var file_name = "Template_Chuyenkhothucte.xlsx";
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
                    worksheet.Cells[rowIdx, colStart + 1].Value = reportData.OrderNo;//Số yêu cầu chuyển
                    worksheet.Cells[rowIdx, colStart + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 2].Value = reportData.OrderDate.Value.ToString("dd-MM-yyyy");//Ngày yêu cầu
                    worksheet.Cells[rowIdx, colStart + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 3].Value = reportData.OrderDate.Value.ToString("dd-MM-yyyy");//Ngày chuyển kho
                    worksheet.Cells[rowIdx, colStart + 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 4].Value = reportData.WarehouseCode_From;//Mã kho đi
                    worksheet.Cells[rowIdx, colStart + 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 5].Value = reportData.WarehouseName_From;//Tên kho đi
                    worksheet.Cells[rowIdx, colStart + 5].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 6].Value = reportData.WarehouseCode_To;//Mã kho đến
                    worksheet.Cells[rowIdx, colStart + 6].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[rowIdx, colStart + 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    worksheet.Cells[rowIdx, colStart + 7].Value = reportData.WarehouseName_To;//Tên Kho đến
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
        
        public List<TransactionHistoryModel> GetTransactionItemsForReport(string transferNo,string transferType)
        {
            var lstTransactionHistory = new TransactionHistoryManagement(db).GetAllBy(transferNo, transferType);
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
