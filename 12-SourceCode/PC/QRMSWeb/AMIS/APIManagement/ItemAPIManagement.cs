﻿
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
    public class ItemAPIManagement
    {
        static readonly ILog m_log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static FlurlClient flurlClient = ConnectManagement.getClientInstance();
        
        
        public RespondedAPI<T> GetItemInfo<T>(int id, int page, int pagesize) where T : class
        {
            return ConnectManagement.GetObjectFromAPI<RespondedAPI<T>>(UrlName.GetListItemMaster, new { id, page, pagesize });
        }

        
    }
}
