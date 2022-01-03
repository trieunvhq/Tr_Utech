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
using System.Net.Http;
using System.Web.Http;
using Web_API.Attributes.Web;
using WebAPI.Models;

namespace WebAPI.Controllers.Web
{
    public class PurchaseOrderItemController : BaseAPIController
    {
        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/purchase-order-item/all-purchaseOrderItem")]
        public BaseModel GetListPurchaseOrderItem()
        {
            var _return = new BaseModel();

            try
            {
                string itemname = "";
                string locationname = "";
                string purchaseOrderNo = "";
                string itemcode = "";
                int page = 1;
                int limit = 10;
                var queryString = Request.GetQueryNameValuePairs();

                foreach (var param in queryString)
                {
                    switch (param.Key)
                    {
                        case "itemname":
                            {
                                itemname = param.Value?.Trim();
                                break;
                            }
                        case "itemcode":
                            {
                                itemcode = param.Value?.Trim();
                                break;
                            }
                        case "locationname":
                            {
                                locationname = param.Value.Trim();
                                break;
                            }
                        case "purchaseorderno":
                            {
                                purchaseOrderNo = param.Value.Trim();
                                break;
                            }
                        case "page":
                            {
                                page = int.Parse(param.Value ?? "1");
                                break;
                            }
                        case "limit":
                            {
                                limit = int.Parse(param.Value ?? "10");
                                break;
                            }
                    }
                }

                _return.RespondCode = "THÀNH CÔNG";
                _return.data = null; // new PuchaseOrderItemBLL().GetAllSaleOrderItem(page, limit, itemname, itemcode, locationname, purchaseOrderNo);
                    
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
        [Route("api-wa/purchase-order-item/{ID}")]
        public BaseModel GetAccountById(int ID)
        {
            var _return = new BaseModel();
            try
            {
                var result = new UserBLL().GetAccountById(ID, true);
                if (result != null)
                {
                    _return.ErrorCode = "0";
                    _return.data = result;
                }
                else
                {
                    _return.Message = "Không thể lấy thông tin account";
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
        [Route("api-wa/purchase-order-item/import")]
        public BaseModel ImportSaleOrder()
        {
            var _return = new BaseModel();
            try
            {
                var result = new List<SaleOrderItemModel>();
                _return.Message = "Import đơn đặt hàng thành công";
                _return.ErrorCode = "0";
                return _return;

            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.Message = ex.ToString();
                return _return;
            }
        }

    }
}