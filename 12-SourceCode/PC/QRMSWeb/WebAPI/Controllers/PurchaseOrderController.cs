using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BPL.Factory.HT.PurchaseOrders;
using HDLIB.Common;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PurchaseOrderController : ApiController
    {
        [HttpPost]
        [Route("api-ht/purchaseorder/getpurchaseorder")]
        public BaseModel Login([FromBody] PurchaseOrderModelInput input)
        {
            var _return = new BaseModel();
            try
            {
                using (var db = new DAL.QRMSEntities())
                {
                    string err_code = "";
                    string err_msg = "";

                    var result = new PurchaseOrderBPL(db).GetPurchaseOrder(input.from_day, input.to_day, out err_code, out err_msg);

                    _return.ErrorCode = err_code;
                    _return.Message = err_msg;
                    _return.data = result;
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = ResponseErrorCode.Error.ToString();
                _return.Message = ex.Message;
            }
            return _return;
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class PurchaseOrderModelInput
    {
        public DateTime from_day { get; set; }
        public DateTime to_day { get; set; }
    }
}