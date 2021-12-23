using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.API
{
    public class LogExAPI
    {
        public static int AddLogEx(string token, string appType, string osType, string actionName, string exContent, int? userId)
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<int>>
                        (Constaint.ServiceAddress, Constaint.APIurl.AddLogEx,
                        new
                        {
                            Token = token,
                            AppType = appType,
                            OsType = osType,
                            ActionName = actionName,
                            ExContent = exContent,
                            UserId = userId
                        });

                return result.data;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
