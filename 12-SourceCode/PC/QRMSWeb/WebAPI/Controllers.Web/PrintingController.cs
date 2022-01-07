using BLL.Factory.Web.LabelPrint;
using BLL.Factory.Web.PurchaseOrder;
using BLL.Factory.Web.SaleOrder;
using BLL.FactoryBLL.Web.Users;
using BPL.Models.Web;
using HDLIB;
using HDLIB.Common;
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
    }
}