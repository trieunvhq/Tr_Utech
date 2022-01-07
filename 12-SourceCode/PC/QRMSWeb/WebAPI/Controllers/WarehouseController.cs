using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BPL.Amis.HT;
using BPL.Models.WarehouseHT;
using HDLIB.Common;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class WarehouseController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        [Route("api-ht/warehouse/getlistwarehouses")]
        public BaseModel GetListWarehouse()
        {
            var _return = new BaseModel();
            try
            {
                string err_code = "";
                string err_msg = "";

                var result = new WarehoseGetBPL().GetListWarehoses(out err_code, out err_msg);

                _return.ErrorCode = err_code;
                _return.Message = err_msg;
                _return.data = result;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                _return.ErrorCode = ConstResponseErrorCode.Error.ToString();
                _return.Message = ex.Message;
            }
            return _return;
        }

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