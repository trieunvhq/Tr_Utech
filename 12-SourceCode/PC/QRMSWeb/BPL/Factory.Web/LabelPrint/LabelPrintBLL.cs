using AMIS.APIManagement;
using BPL.Factory.Web;
using BPL.Models.Web;
using BPL.Models.Web.PrintLabel;
using BPL.Models.Web.Report;
using DAL;
using DAL.Factory.Web.LabelPrint;
using DAL.Factory.Web.SaleOrder;
using HDLIB;
using HDLIB.Common;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Factory.Web.LabelPrint
{
    public class LabelPrintBLL : BaseBLL
    {
        #region LabelPrint
        public HDLIB.WebPaging.TPaging<LabelPrintModel> GetAllLabelPrint(int page, int limit,
            string itemType, string printOrderNo, string wareHouseCode, string printStatus, DateTime? startDate, DateTime? endDate, bool isSeaerch)
        {
            try
            {
                HDLIB.WebPaging.TPaging<LabelPrintModel> pagging = new HDLIB.WebPaging.TPaging<LabelPrintModel>();

                var result = new LabelPrintManagement(db).FindAll(page, limit, itemType, printOrderNo, wareHouseCode,
                    printStatus, startDate, endDate, isSeaerch);
                List<LabelPrintModel> labelPrintModels = new List<LabelPrintModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var labelPrintModel = new LabelPrintModel();
                    labelPrintModel.CopyPropertiesFrom(row);
                    labelPrintModels.Add(labelPrintModel);
                }
                pagging.rows = labelPrintModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }
        public LabelPrintModel GetLabelPrintById(int id)
        {
            try
            {
                var _LabelPrint = new LabelPrintManagement(db).Find(id);
                if (_LabelPrint != null)
                {
                    LabelPrintModel _LabelPrintModel = new LabelPrintModel();
                    _LabelPrintModel.CopyPropertiesFrom(_LabelPrint);
                    return _LabelPrintModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public LabelPrintModel GetLabelPrintDetailById(int id)
        {
            try
            {
                var _LabelPrint = new LabelPrintManagement(db).Find(id);
                if (_LabelPrint != null)
                {
                    LabelPrintModel _LabelPrintModel = new LabelPrintModel();
                    _LabelPrintModel.CopyPropertiesFrom(_LabelPrint);
                    _LabelPrintModel.LabelPrintItemModels = GetAllLabelPrintItemWithOutPagging(
                        _LabelPrintModel.ItemType, _LabelPrintModel.PrintOrderNo, null, null, null, null, null, false);
                    return _LabelPrintModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public LabelPrintModel GetLabelPrintDetailById(int id, int page, int limit)
        {
            try
            {
                var _LabelPrint = new LabelPrintManagement(db).Find(id);
                if (_LabelPrint != null)
                {
                    LabelPrintModel _LabelPrintModel = new LabelPrintModel();
                    _LabelPrintModel.CopyPropertiesFrom(_LabelPrint);
                    _LabelPrintModel.LabelPrintItemPagging = GetAllLabelPrintItems(page, limit,
                        _LabelPrintModel.ItemType, _LabelPrintModel.PrintOrderNo, null, null, null);
                    return _LabelPrintModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public LabelPrintModel GetLabelPrintByOrderNo(string orderNo)
        {
            try
            {
                var _LabelPrint = new LabelPrintManagement(db).GetLabelPrintByOrderNo(orderNo);
                if (_LabelPrint != null)
                {
                    LabelPrintModel _LabelPrintModel = new LabelPrintModel();
                    _LabelPrintModel.CopyPropertiesFrom(_LabelPrint);
                    return _LabelPrintModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public LabelPrintModel GetLabelPrintDetailByOrderNo(string orderNo)
        {
            try
            {
                var _LabelPrint = new LabelPrintManagement(db).GetLabelPrintByOrderNo(orderNo);
                if (_LabelPrint != null)
                {
                    LabelPrintModel _LabelPrintModel = new LabelPrintModel();
                    _LabelPrintModel.CopyPropertiesFrom(_LabelPrint);
                    _LabelPrintModel.LabelPrintItemModels = GetAllLabelPrintItemWithOutPagging(
                        _LabelPrintModel.ItemType, _LabelPrintModel.PrintOrderNo, null, null, null, null, null, false);
                    return _LabelPrintModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public LabelPrintModel GetLabelPrintDetailByOrderNo(string orderNo, int page, int limit)
        {
            try
            {
                var _LabelPrint = new LabelPrintManagement(db).GetLabelPrintByOrderNo(orderNo);
                if (_LabelPrint != null)
                {
                    LabelPrintModel _LabelPrintModel = new LabelPrintModel();
                    _LabelPrintModel.CopyPropertiesFrom(_LabelPrint);
                    _LabelPrintModel.LabelPrintItemPagging = GetAllLabelPrintItems(page, limit,
                        _LabelPrintModel.ItemType, _LabelPrintModel.PrintOrderNo, null, null, null);
                    return _LabelPrintModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }


        public int InsertLabelPrint(DAL.LabelPrint data, string userName)
        {
            try
            {
                //data.ID = 0;

                data.RecordStatus = ConstRecordStatus.New;
                if (data.CreateDate == null)
                {
                    data.CreateDate = DateTime.Now;
                }
                data.CreateUser = userName;

                return new LabelPrintManagement(db).Insert(data);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int UpdateLabelPrint(int id, DAL.LabelPrint data, string userName)
        {
            try
            {
                var labelPrintManagement = new LabelPrintManagement(db);
                var _origin = labelPrintManagement.Find(id);
                if (_origin == null) return -1;
                _origin.CopyPropertiesFrom(data, true);
                _origin.ID = id;
                _origin.UpdateDate = DateTime.Now;
                _origin.RecordStatus = ConstRecordStatus.Update;
                _origin.UpdateUser = userName;
                return labelPrintManagement.Update(_origin);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int DeleteLabelPrint(int id, string userName)
        {
            try
            {
                var labelPrintManagement = new LabelPrintManagement(db);
                var _origin = labelPrintManagement.Find(id);
                if (_origin == null) return -1;
                using (var transac = DataContext.GetTransaction(db))
                {
                    try
                    {
                        _origin.UpdateDate = DateTime.Now;
                        _origin.RecordStatus = ConstRecordStatus.Deleted;
                        _origin.UpdateUser = userName;
                        if (labelPrintManagement.Update(_origin) > 0)
                        {
                            var lstLabelPrintItems = GetAllLabelPrintItemWithOutPagging(_origin.ItemType, _origin.PrintOrderNo, null, null, null, null, null, false);
                            var labelPrintItemManagement = new LabelPrintItemManagement(db);
                            foreach (var item in lstLabelPrintItems)
                            {
                                DeleteLabelPrintItem(item.ID, userName);
                            }
                        }
                        transac.Commit();
                    }
                    catch (Exception ex)
                    {
                        Logging.LogError(ex);
                        transac.Rollback();
                        throw;
                    }
                }
                    return 1;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        #endregion
        #region LabelPrintItem
        public HDLIB.WebPaging.TPaging<LabelPrintItemModel> GetAllLabelPrintItems(int page, int limit,
            string itemType, string printOrderNo, string printStatus, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                HDLIB.WebPaging.TPaging<LabelPrintItemModel> pagging = new HDLIB.WebPaging.TPaging<LabelPrintItemModel>();

                var result = new LabelPrintItemManagement(db).FindAll(page, limit, itemType, printOrderNo,
                    printStatus, startDate, endDate, true);
                List<LabelPrintItemModel> labelPrintItemModels = new List<LabelPrintItemModel>();
                pagging.limit = result.limit;
                pagging.page = result.page;
                pagging.pages = result.pages;
                pagging.total = result.total;
                foreach (var row in result.rows)
                {
                    var labelPrintItemModel = new LabelPrintItemModel();
                    labelPrintItemModel.CopyPropertiesFrom(row);
                    labelPrintItemModels.Add(labelPrintItemModel);
                }
                pagging.rows = labelPrintItemModels;
                return pagging;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }
        public List<LabelPrintItemModel> GetAllLabelPrintItemWithOutPagging(
            string itemType, string printOrderNo, string printStatus, string itemCode, string itemName,
            DateTime? startDate, DateTime? endDate, bool isSearch)
        {
            try
            {
                var rows = new LabelPrintItemManagement(db).FindAllWithOutPagging(itemType, printOrderNo,
                    printStatus, itemCode, itemName, startDate, endDate, isSearch);
                List<LabelPrintItemModel> labelPrintItemModels = new List<LabelPrintItemModel>();

                foreach (var row in rows)
                {
                    var labelPrintItemModel = new LabelPrintItemModel();
                    labelPrintItemModel.CopyPropertiesFrom(row);
                    labelPrintItemModels.Add(labelPrintItemModel);
                }
                return labelPrintItemModels;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }

        }

        public int InsertLabelPrintItem(DAL.LabelPrintItem data, string userName)
        {
            try
            {
                //data.ID = 0;

                data.RecordStatus = ConstRecordStatus.New;
                if (data.CreateDate == null)
                {
                    data.CreateDate = DateTime.Now;
                }
                data.CreateUser = userName;

                return new LabelPrintItemManagement(db).Insert(data);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int UpdateLabelPrintItem(int id, DAL.LabelPrintItem data, string userName)
        {
            try
            {
                var labelPrintItemManagement = new LabelPrintItemManagement(db);
                var _origin = labelPrintItemManagement.Find(id);
                if (_origin == null) return -1;
                _origin.CopyPropertiesFrom(data, true);
                _origin.ID = id;
                _origin.UpdateDate = DateTime.Now;
                _origin.RecordStatus = ConstRecordStatus.Update;
                _origin.UpdateUser = userName;
                return labelPrintItemManagement.Update(_origin);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int DeleteLabelPrintItem(int id, string userName)
        {
            try
            {
                var labelPrintItemManagement = new LabelPrintItemManagement(db);
                var _origin = labelPrintItemManagement.Find(id);
                if (_origin == null) return -1;
                _origin.UpdateDate = DateTime.Now;
                _origin.RecordStatus = ConstRecordStatus.Deleted;
                _origin.UpdateUser = userName;
                return labelPrintItemManagement.Update(_origin);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        #endregion
        #region import from excel file
        public async Task<int> ImportFromExcelFile(Stream excelFileStream, string userName)
        {
            using (var stream = new MemoryStream())
            {

                await excelFileStream.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    var rowCount = worksheet.Dimension.Rows;
                    int count = 0;
                    int labelPrintId = 0;
                    using (var transac = DataContext.GetTransaction(db))
                    {
                        try
                        {
                            for (int row = 2; row <= rowCount; row++)
                            {
                                LabelPrintItemFromExcelFileModel labelPrintItemFromExcelFileModel = new LabelPrintItemFromExcelFileModel();
                                labelPrintItemFromExcelFileModel.PrintOrderNo = worksheet.Cells[row, 1].Value?.ToString();
                                labelPrintItemFromExcelFileModel.PrintOrderDate = worksheet.Cells[row, 2].Value?.ToString();
                                labelPrintItemFromExcelFileModel.ItemTypeName = worksheet.Cells[row, 3].Value?.ToString();
                                labelPrintItemFromExcelFileModel.WareHouseCode = worksheet.Cells[row, 4].Value?.ToString();
                                labelPrintItemFromExcelFileModel.WareHouseName = worksheet.Cells[row, 5].Value?.ToString();
                                labelPrintItemFromExcelFileModel.ItemCode = worksheet.Cells[row, 6].Value?.ToString();
                                labelPrintItemFromExcelFileModel.ItemName = worksheet.Cells[row, 7].Value?.ToString();
                                labelPrintItemFromExcelFileModel.OtherCode = worksheet.Cells[row, 8].Value?.ToString();
                                labelPrintItemFromExcelFileModel.Serial = worksheet.Cells[row, 9].Value?.ToString();
                                labelPrintItemFromExcelFileModel.PartNo = worksheet.Cells[row, 10].Value?.ToString();
                                labelPrintItemFromExcelFileModel.LotNo = worksheet.Cells[row, 11].Value?.ToString();
                                labelPrintItemFromExcelFileModel.MfDate = worksheet.Cells[row, 12].Value?.ToString();
                                labelPrintItemFromExcelFileModel.RecDate = worksheet.Cells[row, 13].Value?.ToString();
                                labelPrintItemFromExcelFileModel.ExpDate = worksheet.Cells[row, 14].Value?.ToString();
                                labelPrintItemFromExcelFileModel.Quantity = worksheet.Cells[row, 15].Value?.ToString();
                                labelPrintItemFromExcelFileModel.Unit = worksheet.Cells[row, 16].Value?.ToString();

                                //validate
                                if (ValidateImport(labelPrintItemFromExcelFileModel))
                                {
                                    labelPrintId = ImportLabelPrint(labelPrintItemFromExcelFileModel, userName);
                                    if (labelPrintId <= 0) throw new Exception($"Không tạo được Label Print. Row: {row}");
                                    if (ImportLabelPrintItem(labelPrintId, labelPrintItemFromExcelFileModel, userName) <= 0)
                                    {
                                        throw new Exception($"Không tạo được Label Print item. Row: {row}");
                                    }
                                    count++;
                                }
                            }
                            transac.Commit();
                            return count;
                        }
                        catch (Exception ex)
                        {
                            transac.Rollback();
                            throw ex;
                        }
                    }
                }
                return 0;
            }
        }
        private bool ValidateImport(LabelPrintItemFromExcelFileModel labelPrintItemFromExcelFileModel)
        {
            return !(string.IsNullOrEmpty(labelPrintItemFromExcelFileModel.PrintOrderNo));
        }
        private int ImportLabelPrint(LabelPrintItemFromExcelFileModel labelPrintItemFromExcelFileModel, string userName)
        {
            var labelPrintManagement = new LabelPrintManagement(db);
            DAL.LabelPrint labelPrint = new DAL.LabelPrint();

            labelPrint.PrintOrderNo = labelPrintItemFromExcelFileModel.PrintOrderNo;
            labelPrint.PrintOrderDate = DateTimeHelper.ConvertStringDateTimeToDate(labelPrintItemFromExcelFileModel.PrintOrderDate);
            labelPrint.WarehouseCode = labelPrintItemFromExcelFileModel.WareHouseCode;
            labelPrint.WarehouseName = labelPrintItemFromExcelFileModel.WareHouseName;
            labelPrint.ItemType = labelPrintItemFromExcelFileModel.ItemType;

            var exist = GetLabelPrintByOrderNo(labelPrintItemFromExcelFileModel.PrintOrderNo);
            if (exist != null)
            {
                
                labelPrint.PrintStatus = exist.PrintStatus;
                UpdateLabelPrint(exist.ID, labelPrint, userName);
                return exist.ID;
            } else { 
                labelPrint.PrintStatus = "N";

                return InsertLabelPrint(labelPrint, userName);
            }
        }
        private int ImportLabelPrintItem(int labelPrintId, LabelPrintItemFromExcelFileModel labelPrintItemFromExcelFileModel, string userName)
        {
            
            LabelPrintItem labelPrintItem = new LabelPrintItem();
            labelPrintItem.LabelPrintID = labelPrintId;
            labelPrintItem.PrintOrderNo = labelPrintItemFromExcelFileModel.PrintOrderNo;
            labelPrintItem.PrintOrderDate = DateTimeHelper.ConvertStringDateTimeToDate(labelPrintItemFromExcelFileModel.PrintOrderNo);
            labelPrintItem.ItemCode = labelPrintItemFromExcelFileModel.ItemCode;
            labelPrintItem.ItemName = labelPrintItemFromExcelFileModel.ItemName;
            labelPrintItem.ItemType = labelPrintItemFromExcelFileModel.ItemType;
            labelPrintItem.OtherCode = labelPrintItemFromExcelFileModel.OtherCode;
            labelPrintItem.Serial = labelPrintItemFromExcelFileModel.Serial;
            labelPrintItem.PartNo = labelPrintItemFromExcelFileModel.PartNo;
            labelPrintItem.LotNo = labelPrintItemFromExcelFileModel.LotNo;
            labelPrintItem.MfDate = DateTimeHelper.ConvertStringDateTimeToDate(labelPrintItemFromExcelFileModel.MfDate);
            labelPrintItem.RecDate = DateTimeHelper.ConvertStringDateTimeToDate(labelPrintItemFromExcelFileModel.RecDate);
            labelPrintItem.ExpDate = DateTimeHelper.ConvertStringDateTimeToDate(labelPrintItemFromExcelFileModel.ExpDate);
            labelPrintItem.Quantity = NumberHelper.ConvertStringNumberVNToDecimal(labelPrintItemFromExcelFileModel.Quantity);
            labelPrintItem.Unit = labelPrintItemFromExcelFileModel.Unit;

            var labelPrintItemManagement = new LabelPrintItemManagement(db);
            var exist = labelPrintItemManagement.FindAllWithOutPagging(labelPrintItemFromExcelFileModel.ItemType,
                labelPrintItemFromExcelFileModel.PrintOrderNo, null,
                labelPrintItemFromExcelFileModel.ItemCode, null, null, null, false).FirstOrDefault();
            if (exist != null)
            {
                labelPrintItem.PrintStatus = exist.PrintStatus;
                UpdateLabelPrintItem(exist.ID, labelPrintItem, userName);
                return 1;
            }
            else
            {
                labelPrintItem.PrintStatus = "N";
                return InsertLabelPrintItem(labelPrintItem, userName);
            }

        }

        /// <summary>
        /// In nhãn được dàn trang trên A4
        /// </summary>
        /// <param name="dataInput"></param>
        /// <returns></returns>
        public string LabelPrint(List<LabelPrintItemModel> dataInput)
        {
            //DataInputModel dataPrint = dataInput.ToList().FirstOrDefault() ?? new DataInputModel();
            Logging.LogMessage("-----Begin Print Label-----");
            try
            {
                List<List<LabelPrintReportModel>> printDataSource = new List<List<LabelPrintReportModel>>()
                {
                    new List<LabelPrintReportModel>(),
                    new List<LabelPrintReportModel>()
                };
                /*
                                string physicalImgPath = HttpContext.Server.MapPath("~/" + Helper.FileHelper.IMG_QR_PATH);
                                if (!Directory.Exists(physicalImgPath)) { Directory.CreateDirectory(physicalImgPath); }
                                //Helper.FileHelper.DeleteAllFileInFolder(physicalImgPath);

                                for (int i = 0; i < dataInput.Length; i++)
                                {
                                    int _index = i % 2;
                                    dataInput[i].BagNo = dataInput.Length <= 1 ? dataInput[i].BagNo ?? 1 : i + 1;
                                    printDataSource[_index].Add(dataInput[i].GetLabelModel(ServiceClient, physicalImgPath));
                                }

                                Response.Buffer = false;
                                Response.ClearContent();
                                Response.ClearHeaders();

                                string physicalReportPath = HttpContext.Server.MapPath($"~/{Helper.FileHelper.IMG_REPORT_PATH}");
                                string reportFile = "MaterialLabel.rpt";
                                string base64 = new Helper
                                    .CrystalReportHelper<List<Reports.MaterialLabelModel>>()
                                    .GenerateBase64Reports(physicalReportPath, reportFile, subPrintDataSource: printDataSource);

                                foreach (var item in printDataSource)
                                {
                                    Helper.FileHelper.DeleteFiles(item.Select(a => a.QRCode).ToArray());
                                }

                                HDLIB.Logging.LogError(new Exception(base64));
                                return Content(base64);
                */
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
            finally
            {
                Logging.LogError(new Exception("-----Begin Print Material-----"));
            }
        }
        #endregion

    }
}
