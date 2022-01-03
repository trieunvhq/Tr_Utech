using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BPL.Factory.HT.TransactionHistoris;
using BPL.Models;
using HDLIB.Common;
using Newtonsoft.Json.Linq;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TransactionHistoryController : ApiController
    {
        public Dictionary<string, List<TransactionHistoryBPLModel>> _QRList = new Dictionary<string, List<TransactionHistoryBPLModel>>();

        [HttpPost]
        [Route("api-ht/transactionHistori/inserthistory")]
        public BaseModel InsertHistoryQR([FromBody] List<TransactionHistoryBPLModel> input)
        {


            var _return = new BaseModel();
            try
            {

                string err_code = "";
                string err_msg = "";
                bool tt = false;
                string token = "";
                int page = 0;

                if (input.Count > 0)
                {
                    token = input[0].token;
                    page = input[0].page;

                    if (_QRList.ContainsKey(token))
                    {
                        for (int i = 0; i < input.Count; ++i)
                        {
                            if (!_QRList[token].Contains(input[i]))
                                _QRList[token].Add(input[i]);
                        }
                        if (page == 0)
                        {
                            tt = true;
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        _QRList.Add(token, new List<TransactionHistoryBPLModel>());
                        for (int i = 0; i < input.Count; ++i)
                        {
                            _QRList[token].Add(input[i]);
                        }
                        if (page == 0)
                        {
                            tt = true;
                        }
                        else
                        {
                        }
                    }

                    if (tt)
                    {
                        var result = new TransactionHistoryBPL().InsertTransactionHistory(_QRList[token], out err_code, out err_msg);
                        _QRList.Remove(token);
                        _return.data = result;
                    }
                    else
                    {
                        _return.data = null;
                    }
                }
                else
                {
                    _return.data = null;
                }
                
                _return.ErrorCode = err_code;
                _return.Message = err_msg;

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