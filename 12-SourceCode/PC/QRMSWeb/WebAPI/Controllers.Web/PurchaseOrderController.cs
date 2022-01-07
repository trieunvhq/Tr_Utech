using BLL.Factory.Web.PurchaseOrder;
using BLL.Factory.Web.TransactionHistory;
using HDLIB.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Web_API.Attributes.Web;
using WebAPI.Models;

namespace WebAPI.Controllers.Web
{
    public class PurchaseOrderWebController : BaseAPIController
    {
        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/purchase-order/all-purchase-order")]
        public BaseModel GetListPurchaseOrder(int page = 1, int limit = Constant.NumPage,
            string wareHouseCode = null, string purchaseOrderNo = null, string inputStatus = null,
            DateTime? startDate = null, DateTime? endDate = null, bool isSearch=true)
        {
            var _return = new BaseModel();
            try
            {
                wareHouseCode = wareHouseCode?.Trim();
                inputStatus = inputStatus?.Trim();
                purchaseOrderNo = purchaseOrderNo?.Trim();
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new PurchaseOrderBLL().FindAllPurchaseOrder(page, limit, wareHouseCode, purchaseOrderNo, inputStatus, startDate, endDate, isSearch);

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
        [Route("api-wa/purchase-order/all-purchase-order-item")]
        public BaseModel GetListPurchaseOrderItem(int page = 1, int limit = Constant.NumPage,
            string purchaseOrderNo = null)
        {
            var _return = new BaseModel();
            try
            {
                purchaseOrderNo = purchaseOrderNo?.Trim();
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new PurchaseOrderBLL().FindAllPurchaseOrderItem(page, limit, purchaseOrderNo);

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
        [Route("api-wa/purchase-order/purchase-order-actual-scan")]
        public BaseModel GetListPurchaseOrderItemActualScan(int page = 1, int limit = Constant.NumPage,
             string purchaseOrderNo = null)
        {
            var _return = new BaseModel();
            try
            {
                purchaseOrderNo = purchaseOrderNo?.Trim();
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new TransactionHistoryBLL().FindAll(page, limit, ConstTransactionType.NhapKho, purchaseOrderNo, null
                    , null, null, null, null,false);

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
        [Route("api-wa/purchase-order/{ID}")]
        public BaseModel GetPurchaseOrderById(int ID)
        {
            var _return = new BaseModel();
            try
            {
                var result = new PurchaseOrderBLL().GetPurchaseOrderById(ID);
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
        [Route("api-wa/get-purchase-order-by-no")]
        public BaseModel GetPurchaseOrderByPurchaseOrderNo(string PurchaseOrderNo = null)
        {
            var _return = new BaseModel();
            try
            {
                var result = new PurchaseOrderBLL().GetPurchaseOrderByPurchaseOrderNo(PurchaseOrderNo);
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
        [Route("api-wa/purchase-order/import-from-amis")]
        public BaseModel ImportFromAMIS()
        {
            var _return = new BaseModel();
            try
            {
                var userName = "WA";
                var result = new PurchaseOrderBLL().ImportFromAMIS(userName);
                if (result >= 0)
                {
                    _return.Message = "Import đơn đặt hàng thành công";
                    _return.ErrorCode = "0";
                }
                else
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
        [Route("api_wa/purchase-order/export-excel")]
        public HttpResponseMessage ReportToExcell(string purchaseOrderNo = null)
        {
            try
            {
                //Response.BinaryWrite(package.GetAsByteArray());

                var excelResponseBase = new PurchaseOrderBLL().ExportToExcel(purchaseOrderNo);
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

        #region Print
        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/purchase-order/all-purchase-order-print")]
        public BaseModel GetListPurchaseOrderPrint(int page = 1, int limit = Constant.NumPage,
            string itemType=null, string wareHouseCode=null, string purchaseOrderNo=null, string printStatus=null,
            DateTime? purchaseOrderDate=null, bool isSearch = true)
        {
            var _return = new BaseModel();
            try
            {
                itemType = itemType?.Trim();
                wareHouseCode = wareHouseCode?.Trim();
                purchaseOrderNo = purchaseOrderNo?.Trim();
                printStatus = printStatus?.Trim();

                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new PurchaseOrderBLL().FindAllPurchaseOrderPrint(page, limit, itemType, wareHouseCode, purchaseOrderNo,
                    printStatus, purchaseOrderDate, isSearch);

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
        #endregion
    }
}