using BLL.Factory.Web.TransferInstruction;
using BLL.Factory.Web.TransactionHistory;
using BLL.FactoryBLL.Web.Users;
using BPL.Models.Web;
using HDLIB;
using HDLIB.Common;
using Newtonsoft.Json.Linq;
using PISAS_API.Models.Web.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Web_API.Attributes.Web;
using WebAPI.Models;

namespace WebAPI.Controllers.Web
{
    public class TransferInstructionWebController : BaseAPIController
    {

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/transfer-ins/find-all-transfer-ins")]
        public BaseModel FindListTransferInstructions(int page = 1, int limit = Constant.NumPage,string transferType = null,
            string transferNo = null, string wareHouseCode_from = null, string wareHouseCode_to = null,
            DateTime? startDate = null,  DateTime? endDate = null)
        {
            var _return = new BaseModel();

            try
            {
                transferNo = transferNo?.Trim();
                wareHouseCode_from = wareHouseCode_from?.Trim();
                wareHouseCode_to = wareHouseCode_to?.Trim();

                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new TransferInstructionBLL().FindAllTransferInstruction(page, limit, transferType, transferNo, wareHouseCode_from,wareHouseCode_to, startDate, endDate);
                    
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
        [Route("api-wa/transfer-ins/find-all-transfer-ins-item")]
        public BaseModel FindListTransferInstructions(int page = 1, int limit = Constant.NumPage,string transferType = null,
            string saleOrderNo = null)
        {
            var _return = new BaseModel();

            try
            {
                saleOrderNo = saleOrderNo?.Trim();

                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new TransferInstructionBLL().FindAllTransferInstructionItem(page, limit, transferType, saleOrderNo);

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
        [Route("api-wa/get-transfer-ins-by-no")]
        public BaseModel GetTransferInstructionByTranferNo(string transferOrderNo = null,string transferType = null)
        {
            var _return = new BaseModel();
            try
            {
                var result = new TransferInstructionBLL().GetTransferInstructionByTranferNo(transferOrderNo, transferType);
                if (result != null)
                {
                    _return.ErrorCode = "0";
                    _return.data = result;
                }
                else
                {
                    _return.Message = "Không tìm thấy thông tin";
                    _return.ErrorCode = "-1";
                }

                return _return;

            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ex.ToString();
                return _return;
            }
        }

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/transfer-ins/transfer-ins-actual-scan")]
        public BaseModel GetListTransferInstructionItemActualScan(int page = 1, int limit = Constant.NumPage,string transferType = null,
             string tranferOrderNo = null)
        {
            var _return = new BaseModel();
            try
            {
                tranferOrderNo = tranferOrderNo?.Trim();
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new TransactionHistoryBLL().FindAll(page, limit, transferType, tranferOrderNo, null
                    , null, null, null, null, false);

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
        [Route("api-wa/transfer-ins/import-from-amis")]
        public BaseModel ImportFromAMIS()
        {
            var _return = new BaseModel();
            try
            {
                var userName = "WA";
                var result = new TransferInstructionBLL().ImportFromAMIS(userName);
                if (result >= 0) { 
                    _return.Message = "Import chỉ thị thành công";
                    _return.ErrorCode = "0";
                } else
                {
                    _return.Message = "Import chỉ thị thất bại";
                    _return.ErrorCode = "-1";
                }
                return _return;

            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ex.ToString();
                _return.ErrorCode = "-99";
                return _return;
            }
        }
        [HttpGet]
        [Route("api_wa/transfer-ins/export-excel")]
        public HttpResponseMessage ReportToExcell(string tranferOrderNo, string transferType)
        {
            try
            {
                //Response.BinaryWrite(package.GetAsByteArray());

                var excelResponseBase = new TransferInstructionBLL().ExportToExcel(tranferOrderNo,transferType);
                if (excelResponseBase != null)
                {
                    var result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(excelResponseBase.Data);

                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = excelResponseBase.FileName
                    };
                    return result;
                }

            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                //TODO for testing  Logging.LogErrorTesting(ex);
            }

            var result_err = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("-- Co loi --")
            };
            result_err.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return result_err;
        }
    }
}