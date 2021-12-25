using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory.Web.Account
{
    public class AccountLogManagement
    {
        //public static int AddAccountLog(string ipAddress, string deviceName, int userID, string userName)
        //{
        //    try
        //    {
        //        using (var transaction = GlobalVariable.db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var item = new ACCOUNTLOG()
        //                {
        //                    LOG_TIME = DateTime.Now,
        //                    IP_ADDRESS = ipAddress,
        //                    DEVICE_INFO = deviceName,
        //                    USER_ID = userID,
        //                    USER_NAME = userName
        //                };

        //                GlobalVariable.db.ACCOUNTLOGs.Add(item);
        //                GlobalVariable.db.SaveChanges();

        //                transaction.Commit();
        //                return 0;
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                return -1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return -1;
        //    }
        //}
    }
}
