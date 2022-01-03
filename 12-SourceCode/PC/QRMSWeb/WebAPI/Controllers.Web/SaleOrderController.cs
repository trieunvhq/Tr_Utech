using BLL.Factory.Web.SaleOrder;
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
    public class SaleOrderController : BaseAPIController
    {
        

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/sale-order/find-all")]
        public BaseModel FindListSaleOrders(int page = 1, int limit = Constant.NumPage,
            string itemCode = null, string itemName = null, string saleOrderNo = null, string locationName = null, 
            DateTime? startDate = null,  DateTime? endDate = null)
        {
            var _return = new BaseModel();

            try
            {
                itemCode = itemCode?.Trim();
                itemName = itemName?.Trim();
                saleOrderNo = saleOrderNo?.Trim();
                locationName = locationName?.Trim();

                _return.RespondCode = APIResponseCode.SUCCESS;
                _return.data = new SaleOrderBLL().FindAll(page, limit, itemCode, itemName, saleOrderNo, locationName, startDate, endDate);
                    
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
        [Route("api-wa/sale-order/import-from-amis")]
        public BaseModel ImportFromAMIS()
        {
            var _return = new BaseModel();
            try
            {
                var userName = "WA";
                var result = new SaleOrderBLL().ImportFromAMIS(userName);
                if (result >= 0) { 
                    _return.Message = "Import đơn đặt hàng thành công";
                    _return.ErrorCode = "0";
                } else
                {
                    _return.Message = "Import đơn đặt hàng thất bại";
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
        [Route("api_wa/sale-order/export-excel")]
        public HttpResponseMessage ReportToExcell(int? saleOrderID)
        {
            try
            {
                //Response.BinaryWrite(package.GetAsByteArray());

                var excelResponseBase = new SaleOrderBLL().ExportToExcel(saleOrderID);
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