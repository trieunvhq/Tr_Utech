using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDLIB.Common;

namespace DAL.FactoryDAL.WebAdmin.Account
{
    public class LoginLogManagement
    {
        QRMSEntities db;
        public LoginLogManagement() { db = new QRMSEntities(); }
        public LoginLogManagement(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }

        public int AddLoginLog(string ipAddress, string deviceName, int userID, string userName)
        {
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        /*
                        var item = new LOGIN_LOG()
                        {
                            LOG_TIME = DateTime.Now,
                            IP_ADDRESS = ipAddress,
                            DEVICE_INFO = deviceName,
                            USER_ID = userID,
                            USER_NAME = userName
                        };

                        db.LOGIN_LOG.Add(item);
                        db.SaveChanges();

                        transaction.Commit();
                        */
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        Logging.LogMessage(ex.ToString());
                        transaction.Rollback();
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return -1;
            }
        }

        // NEW
        /*
        public int Insert(params LOGIN_LOG[] _VALUEs)
        {
            try
            {
                db.LOGIN_LOG.AddRange(_VALUEs);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }*/
    }
}
