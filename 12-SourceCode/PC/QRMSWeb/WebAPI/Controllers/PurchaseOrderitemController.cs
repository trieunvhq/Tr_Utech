using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using Newtonsoft.Json.Linq;
using BPL.Factory.HT.PurchaseOrderitems;
using HDLIB.Common;

namespace WebAPI.Controllers
{
    public class PurchaseOrderitemController : ApiController
    {
        [HttpPost]
        [Route("api-ht/purchaseorderitem/getitem")]
        public BaseModel Login([FromBody] JObject input)
        {
            var _return = new BaseModel();
            try
            {
                using (var db = new DAL.QRMSEntities())
                {
                    string err_code = "";
                    string err_msg = "";
                    int PurchaseOrderItemID = input["ID"].ToObject<int>();

                    var result = new PurchaseOrderitemBPL(db).GetPurchaseOrderitems(PurchaseOrderItemID, out err_code, out err_msg);

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
}