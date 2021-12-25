using HDLIB.Common;
using System;
using System.Linq;

namespace DAL.Factory.Web.Users
{
    public class UserManagement : IDisposable
    {
        QRMSEntities db;

        public UserManagement()
        {
            db = new QRMSEntities();
        }

        public UserManagement(QRMSEntities db)
        {
            this.db = db ?? DataContext.getEntities();
        }

        public User Select(int ID)
        {
            try
            {
                return db.Users.Find(ID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public User FindByPk(int id, bool withLock)
        {
            try
            {
                if (withLock) { 
                    return db.Users.AsNoTracking().FirstOrDefault(a => a.ID == id && a.RecordStatus != RecordStatus.Deleted);
                } else
                {
                    return db.Users.AsNoTracking().FirstOrDefault(a => a.ID == id && a.RecordStatus != RecordStatus.Deleted && a.RecordStatus != RecordStatus.Locked);
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public User FindByUserName(string userName)
        {
            try
            {
                userName = userName?.Trim();
                return db.Users.AsNoTracking().FirstOrDefault(a => a.Code.ToLower() == userName.ToLower() && a.RecordStatus != RecordStatus.Deleted);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        
        public HDLIB.WebPaging.TPaging<User> GetAllUser(int page, int limit,
            string username, string fullName, string userType, bool isSearch)
        {
            try
            {
                username = username?.Trim();
                fullName = fullName?.Trim();
                userType = userType?.Trim();

                HDLIB.WebPaging.TPaging<User> paging = new HDLIB.WebPaging.TPaging<User>();
                int offset = (page - 1) * limit;

                string SQL = $"select * from User a where (a.RecordStatus is not null and a.RecordStatus != '{ RecordStatus.Deleted }')";
                if (isSearch) { 
                    SQL += (string.IsNullOrEmpty(username)) ? "" : $" and LOWER(a.Code) LIKE '%{username.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                } else
                {
                    SQL += (string.IsNullOrEmpty(username)) ? "" : $" and LOWER(a.Code) = '{username.ToLower()}'";
                }
                SQL += (string.IsNullOrEmpty(fullName)) ? "" : $" and LOWER(a.FullName) LIKE '%{fullName.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";
                SQL += (string.IsNullOrEmpty(userType)) ? "" : $" and LOWER(a.Role) LIKE '%{userType.Trim().ToLower().Replace("\\", "\\\\").Replace("'", "''").Replace("%", "\\%").Replace("_", "\\_")}%' ESCAPE '\\'";


                //SQL += isAssetPercentFee ? " and a.ASSET_PERCENT_FEE is not null" : " and a.ASSET_PERCENT_FEE is null";
                // SQL += " order by a.common_code, a.common_name asc, a.ACTIVE_DATE_F desc, a.ACTIVE_DATE_T desc";
                SQL += " order by a.id desc";
                var exec_sql = db.Users.SqlQuery(SQL);
                var data = exec_sql.AsNoTracking().Skip(offset).Take(limit).ToList();
                int total = exec_sql.Count();
                paging.CalculatePaging(data, page, limit, total);

                return paging;

            }
            catch (Exception e)
            {
                Logging.LogError(e);
                throw;
            }
        }


        public int Update(User _acc)
        {
            try
            {
                db.DetachAll<User>();
                db.Entry(_acc).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public int Insert(params User[] _Accs)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.Users.AddRange(_Accs);
                return db.SaveChanges();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public User CheckAccount(string userName, string pass)
        {
            try
            {
               
                string SQL = $"select * from User a where (a.RecordStatus is not null and a.RecordStatus != '{RecordStatus.Deleted}') ";
                SQL += $" and a.code = '{userName}' and a.password_hash = '{pass}'";
                var result = db.Users.SqlQuery(SQL).AsNoTracking().SingleOrDefault();
                if (result != null)
                {
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
        }

        public int LockAccount(string userName, int? userID)
        {
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        string SQL = $"select * from User a where (a.RecordStatus is not null and a.RecordStatus != '{RecordStatus.Deleted}') ";
                        SQL += $" and a.code = '{userName}'";
                        var result = db.Users.SqlQuery(SQL).FirstOrDefault();
                        if (result != null)
                        {
                            result.RecordStatus = RecordStatus.Locked;
                            result.UpdateDate = DateTime.Now;
                            result.UpdateUser = userName;

                            db.DetachAll<User>();
                            db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            transaction.Commit();
                            return 0;
                        }

                        return 1;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return -1;
            }
        }

        public User GetAccountByUser(string userName)
        {
            try
            {
                string SQL = $"select * from User a where (a.RecordStatus is not null and a.RecordStatus != '{RecordStatus.Deleted}') ";
                SQL += $" and a.code = '{userName}'";
                var result = db.Users.SqlQuery(SQL).AsNoTracking().FirstOrDefault();
                if (result != null)
                {
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return null;
            }
        }

        public int UpdatePassword(string userName, string newPass, int? userID)
        {
            try
            {
                string SQL = $"select * from User a where (a.RecordStatus is not null and a.RecordStatus != '{RecordStatus.Deleted}') ";
                SQL += $" and a.code = '{userName}'";
                var result = db.Users.SqlQuery(SQL).AsNoTracking().FirstOrDefault();
                if (result != null)
                {
                    result.Password = newPass;
                    result.RecordStatus = RecordStatus.Update;
                    result.UpdateDate = DateTime.Now;
                    result.UpdateUser = userName;

                    db.DetachAll<User>();
                    db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                    return db.SaveChanges();
                }

                return -1;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw ex;
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AccountManagement() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion

        
    }
}