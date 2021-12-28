using System;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class BaseAPIController : ApiController
    {
        #region IDispose
        // WebApi 2 will call this automatically after each 
        // request. You need this to ensure your context is disposed
        // and the memory it is using is freed when your app does garbage 
        // collection.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //20211223 TODO db.Dispose();
            }
            GC.Collect();
            base.Dispose(disposing);
        }
        #endregion
    }
}