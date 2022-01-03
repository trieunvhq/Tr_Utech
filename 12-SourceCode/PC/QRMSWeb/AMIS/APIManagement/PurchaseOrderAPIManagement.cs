using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIS.APIModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Flurl;
using Flurl.Http;
using log4net;

namespace AMIS.APIManagement
{
    public class PurchaseOrderAPIManagement
    {
        static readonly ILog m_log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static FlurlClient flurlClient = ConnectManagement.getClientInstance();

        public PurchaseOrderAPIManagement()
        {

        }
        public RespondedAPI<T> GetPurchaseOrderInfo<T>(int po_id, int page, int pagesize) where T : class
        {

            return ConnectManagement.GetObjectFromAPI<RespondedAPI<T>>(UrlName.GetListPurchaseOrder, new { po_id, page, pagesize });
        }
    }
}
