using BLL.Factory.Web.LabelPrint;
using BLL.Factory.Web.PurchaseOrder;
using BLL.Factory.Web.SaleOrder;
using BLL.FactoryBLL.Web.Users;
using BPL.Models.Web;
using BPL.Models.Web.PrintLabel;
using BPL.Utils;
using HDLIB;
using HDLIB.Common;
using HDLIB.Helper;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Web_API.Attributes.Web;
using WebAPI.Models;
using WebAPI.Reports;

namespace WebAPI.Controllers.Web
{
    public class PrintingWebController : BaseAPIController
    {
        
        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/printing-by-instruction/all-label-prints")]
        public BaseModel GetListLabelPrints(int page = 1, int limit = Constant.NumPage,
            string itemType=null, string printOrderNo = null, string wareHouseCode = null, string printStatus = null,
            DateTime? startDate = null, DateTime? endDate = null, bool isSearch = true)
        {
            var _return = new BaseModel();
            try
            {
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
               _return.data = new LabelPrintBLL().GetAllLabelPrint(page, limit, itemType, printOrderNo, wareHouseCode,
                  printStatus, startDate, endDate, isSearch);

                return _return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _return.Message = "Lỗi hệ thống";
                _return.RespondCode = "500";
                _return.ErrorCode = "SYSTEM_ERROR";
                return _return;
            }
        }

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/printing-by-instruction/label-print-detail")]
        public BaseModel GetLabelPrintDetails(int page = 1, int limit = Constant.NumPage, int printOrderId = 0)
        {
            var _return = new BaseModel();
            try
            {
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new LabelPrintBLL().GetLabelPrintDetailById(printOrderId, page, limit);

                return _return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _return.Message = "Lỗi hệ thống";
                _return.RespondCode = "500";
                _return.ErrorCode = "SYSTEM_ERROR";
                return _return;
            }
        }

        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/printing-by-instruction/import-excel-file")]
        public async Task<BaseModel> ImportFile()
        {
            var _return = new BaseModel();
            try
            {
                string userName = HelperFunction.GetUserName(User);
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var inputfile = httpRequest.Files[0];
                    var fileStream = inputfile?.InputStream;

                    if (inputfile != null && fileStream != null)
                    {
                        if (!(inputfile.FileName.EndsWith(".xls") || inputfile.FileName.EndsWith(".xlsx")))
                        {
                            _return.Message = "Tệp không đúng định dạng";
                            _return.RespondCode = "VALIDATE";
                            _return.ErrorCode = "VALIDATE";
                            return _return;
                        }

                        var result = await new LabelPrintBLL().ImportFromExcelFile(fileStream, userName);
                        if (result > 0) { 
                            _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                            _return.Message = result.ToString();
                            _return.ErrorCode = "0";
                            return _return;
                        }
                    }
                }
                _return.RespondCode = ConstAPIResponseCode.BAD_REQUEST;
                _return.Message = "Import thất bại công";
                _return.ErrorCode = "1";
                return _return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _return.Message = "Lỗi hệ thống";
                _return.RespondCode = "500";
                _return.ErrorCode = "SYSTEM_ERROR";
                return _return;
            }
        }
        
        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/printing-by-instruction-order/delete-print-instruction/{ID}")]
            public BaseModel DeletePrintInstruction(int ID)
            {
            var _return = new BaseModel();
            try
            {
                string userName = HelperFunction.GetUserName(User);
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new LabelPrintBLL().DeleteLabelPrint(ID, userName);

                return _return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _return.Message = "Lỗi hệ thống";
                _return.RespondCode = "500";
                _return.ErrorCode = "SYSTEM_ERROR";
                return _return;
            }
        }

        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/printing-by-instruction-order/print-label-by-instruction/{printOrderNo}")]
        public BaseModel PrintLabelByInstruction(string printOrderNo)
        {
            var _return = new BaseModel();
            try
            {
                string userName = HelperFunction.GetUserName(User);
                var labelPrintBLL = new LabelPrintBLL();
                var labelPrintModel = labelPrintBLL.GetLabelPrintByOrderNo(printOrderNo);
                if (labelPrintModel == null)
                {
                    _return.RespondCode = ConstAPIResponseCode.NOT_FOUND;
                    _return.ErrorCode = "-1";
                    _return.Message = "Không tìm thấy Nhãn";
                    return _return;
                }

                if (ConstPrintStatus.DaIn.Equals(labelPrintModel.PrintStatus))
                {
                    _return.RespondCode = "PRINTED";
                    _return.ErrorCode = "-2";
                    _return.Message = "Đã in nhãn rồi";
                    return _return;
                }

                var lstLabelPrintItem = labelPrintBLL.GetAllLabelPrintItemWithOutPagging(
                    labelPrintModel.ItemType, labelPrintModel.PrintOrderNo, null, null, null, null, null, false);
                lstLabelPrintItem = lstLabelPrintItem.Where(item => ConstPrintStatus.ChuaIn.Equals(item.PrintStatus)).ToList();
                if (lstLabelPrintItem.Count == 0)
                {
                    _return.RespondCode = "PRINTED";
                    _return.ErrorCode = "-2";
                    _return.Message = "Đã in nhãn rồi";
                    return _return;
                }

                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = BuildLabelPrint(lstLabelPrintItem);

                return _return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _return.Message = "Lỗi hệ thống";
                _return.RespondCode = "500";
                _return.ErrorCode = "SYSTEM_ERROR";
                return _return;
            }
        }

        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/printing-by-instruction-order/print-label")]
        public BaseModel PrintLabelItem([FromBody] List<LabelPrintItemModel> labelPrintItemModels)
        {
            var _return = new BaseModel();
            try
            {
                if (labelPrintItemModels.Count == 0)
                {
                    _return.RespondCode = "NOT_ITEM_PRINT";
                    _return.ErrorCode = "-2";
                    _return.Message = "Không có nhãn";
                    return _return;
                }

                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = BuildLabelPrint(labelPrintItemModels);

                return _return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _return.Message = "Lỗi hệ thống";
                _return.RespondCode = "500";
                _return.ErrorCode = "SYSTEM_ERROR";
                return _return;
            }
        }

        /// <summary>
        /// In nhãn được dàn trang trên A4
        /// </summary>
        /// <param name="dataInput"></param>
        /// <returns></returns>
        private string BuildLabelPrint(List<LabelPrintItemModel> dataInput)
        {
            //DataInputModel dataPrint = dataInput.ToList().FirstOrDefault() ?? new DataInputModel();
            Logging.LogMessage("-----Begin Print Label-----");
            try
            {

                List<List<LabelPrintReportModel>> printDataSource = new List<List<LabelPrintReportModel>>();
                /*{
                    new List<LabelPrintReportModel>(),
                    new List<LabelPrintReportModel>()
                };
                */
                string physicalImgPath = HttpContext.Current.Server.MapPath("~/" + Helper.FileHelper.IMG_QR_PATH);
                if (!Directory.Exists(physicalImgPath)) { Directory.CreateDirectory(physicalImgPath); }
                //Helper.FileHelper.DeleteAllFileInFolder(physicalImgPath);

                for (int i = 0; i < dataInput.Count; i++)
                {
                    int _index = i % 4; // 4 labels/row
                    /* dataInput[i].BagNo = dataInput.Count <= 1 ? dataInput[i].BagNo ?? 1 : i + 1;
                    
                     printDataSource[_index].Add(dataInput[i].GetLabelModel(ServiceClient, physicalImgPath));
                    */
                    //add new row
                    if (_index == 0)
                    {
                        printDataSource.Add(new List<LabelPrintReportModel>());
                    }
                    printDataSource[_index].Add(BuildLabelPrintReportModel(dataInput[i], physicalImgPath));
                }

                /*                                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                */

                string physicalReportPath = HttpContext.Current.Server.MapPath($"~/{Helper.FileHelper.IMG_REPORT_PATH}");

                string reportFile = "LabelPrint.rpt";
                string base64 = new Helper
                    .CrystalReportHelper<List<Reports.LabelPrintReportModel>>()
                    .GenerateBase64Reports(physicalReportPath, reportFile, printDataSource: printDataSource);
                /*
                string base64 = new Helper
                    .CrystalReportHelper<List<Reports.LabelPrintReportModel>>()
                    .GenerateBase64Reports(physicalReportPath, reportFile, subPrintDataSource: printDataSource);
                /*
                foreach (var item in printDataSource)
                {
                    Helper.FileHelper.DeleteFiles(item.Select(a => a.QRCode).ToArray());
                }
                */
                Logging.LogMessage(base64);

                return base64;
                    //return Content(base64);
                
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
        private LabelPrintReportModel BuildLabelPrintReportModel(LabelPrintItemModel labelPrintItemModel, string pathImage)
        {
            LabelPrintReportModel labelPrintReportModel = new LabelPrintReportModel();

            labelPrintReportModel.PrintOrderNo = labelPrintItemModel.PrintOrderNo;
            labelPrintReportModel.PrintOrderDate = DateTimeHelper.GetDateVNFormated(labelPrintItemModel.PrintOrderDate);
            labelPrintReportModel.ItemType = labelPrintItemModel.ItemType;
            labelPrintReportModel.ItemTypeName = CommonUtils.GetItemTypeName(labelPrintItemModel.ItemType);
            labelPrintReportModel.ItemCode = labelPrintItemModel.ItemCode;
            labelPrintReportModel.ItemName = labelPrintItemModel.ItemName;
            labelPrintReportModel.OtherCode = labelPrintItemModel.OtherCode;
            labelPrintReportModel.Serial = labelPrintItemModel.Serial;
            labelPrintReportModel.PartNo = labelPrintItemModel.PartNo;
            labelPrintReportModel.LotNo = labelPrintItemModel.LotNo;
            labelPrintReportModel.MfDate = DateTimeHelper.GetDateVNFormated(labelPrintItemModel.MfDate);
            labelPrintReportModel.RecDate = DateTimeHelper.GetDateVNFormated(labelPrintItemModel.RecDate);
            labelPrintReportModel.ExpDate = DateTimeHelper.GetDateVNFormated(labelPrintItemModel.ExpDate);
            labelPrintReportModel.Quantity = labelPrintItemModel.Quantity?.ToString("F");
            labelPrintReportModel.Unit = labelPrintItemModel.Unit;
            //.QRCodeBase64 = ;
            //labelPrintReportModel.QRCode = ; //fule file path

/*
            LabelPrintReportModel _labelData = new LabelPrintReportModel()
            {
                BagNo = BagNo?.ToString(),
                Quantity = Quantity?.ToString("F")
            };
            ObjectHelper.PropertyCopier<LabelPrintItemModel, LabelPrintReportModel>.Copy(inputModel, ref _labelData);
            var qrMaterialModel = this.GetMaterialQRModel();

            string fileName = inputModel.IType == HDLIB.Constants.Item_CType.Material ?
                Helper.FileHelper.SaveQRImg(Helper.FileHelper.IMG_QR_PATH, serviceClient.MaterialQRCodeImage(qrMaterialModel))
                : Helper.FileHelper.SaveQRImg(Helper.FileHelper.IMG_QR_PATH, serviceClient.PackingQRCodeImage(qrMaterialModel));
            _labelData.QRCode = Path.Combine(folderPath, fileName);
*/
            return labelPrintReportModel;
        }

        [HttpPost]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api_wa/printing-by-instruction-order/update-printed-label")]
        public BaseModel UpdatePrintLabelItem([FromBody] List<LabelPrintItemModel> labelPrintItemModels)
        {
            var _return = new BaseModel();
            try
            {
                var ids = labelPrintItemModels?.Select(item => item.ID).ToList();
                var result = new LabelPrintBLL().UpdatePrintStatus(ids);
                /*if (printItemID <= 0)
                {
                    _return.RespondCode = "NOT_ITEM_PRINT";
                    _return.ErrorCode = "-2";
                    _return.Message = "Không có nhãn";
                    return _return;
                }
                */
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = null;

                return _return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _return.Message = "Lỗi hệ thống";
                _return.RespondCode = "500";
                _return.ErrorCode = "SYSTEM_ERROR";
                return _return;
            }
        }
    }
}