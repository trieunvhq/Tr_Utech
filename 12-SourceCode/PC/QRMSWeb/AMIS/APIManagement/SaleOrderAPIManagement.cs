
using AMIS.APIModels;
using Flurl.Http;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.APIManagement
{
    public class SaleOrderAPIManagement
    {
        static readonly ILog m_log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static FlurlClient flurlClient = ConnectManagement.getClientInstance();
        
        public void GetSaleOrder()
        {
            try
            {
                int fromDocEntry = 0;
                //fromDocEntry = new SaleOrderBPL().GetMaxDocEntry();
                //GetSaleOrderNumbers(fromDocEntry);
                //GetSaleOrderInfo(fromDocEntry);
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
        }
        
        public RespondedAPI<T> GetSaleOrderInfo<T>(int so_id, int page, int pagesize) where T : class
        {
           				
            return ConnectManagement.GetObjectFromAPI<RespondedAPI<T>>(UrlName.GetListSaleOrder, new { so_id, page, pagesize });
        }

        
    }
}
