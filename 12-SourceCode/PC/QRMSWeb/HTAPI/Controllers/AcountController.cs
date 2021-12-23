using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HTAPI.Controllers
{
    //[Route("api/[controller]")]
    public class AcountController : Controller
    {

        //Check đăng nhập
        [HttpPost]
        [Route("api-cus/account/login/tt")]
        public BaseModel CheckAccount([FromBody] acmodel jObject)
        {
            var _return = new BaseModel();
            try
            {
                string user = jObject.user;
                string pass = jObject.pass;

                var result = "";//AcountLogin.checkLogin(user, pass);

                if (result != null)
                {
                    //if (result.isTrue)
                    //{
                        _return.RespondCode = "1";
                        _return.Message = "Đăng nhập thành công";
                        _return.ErrorCode = "";
                    //}
                }
                else
                {
                    _return.RespondCode = "0";
                    _return.ErrorCode = "";
                    _return.Message = "";
                }

                _return.data = result;
            }
            catch (Exception ex)
            {
                //Logging.LogError(ex);
                _return.Message = ex.ToString();
            }
            return _return;
        }
    

   

    // GET: api/values
    [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

public class acmodel
{
    public string user { get; set; }
    public string pass { get; set; }
}
}
