using BLL.Factory.Web.PurchaseOrder;
using BLL.Factory.Web.TransactionHistory;
using BLL.FactoryBLL.Web.Users;
using BPL.Models.Web;
using HDLIB;
using HDLIB.Common;
using Newtonsoft.Json.Linq;
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
    public class TransactionHistoryWebController : BaseAPIController
    {

        [HttpGet]
        [AuthRequire]
        [PJICOAuthorize]
        [Route("api-wa/transaction-history/all-transaction-histories")]
        public BaseModel GetListTransactionHistories(int page = 1, int limit = Constant.NumPage,
            string transactionType = null, string orderNo = null, DateTime? orderDate = null,
            string itemType = null, string itemCode = null, DateTime? orderDateFrom = null, DateTime? orderDateTo = null, bool isSearch = true)
        {
            var _return = new BaseModel();
            try
            {
                _return.RespondCode = ConstAPIResponseCode.SUCCESS;
                _return.data = new TransactionHistoryBLL().FindAll(page, limit, transactionType, orderNo, orderDate,
                    itemType, itemCode, orderDateFrom, orderDateTo, isSearch);
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