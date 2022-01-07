using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDLIB.Paging;
using HDLIB.Common;

namespace DAL.Factory.HT.Users
{
    public class AccountManagement : IDisposable
    {
        QRMSEntities db;
        public AccountManagement() { db = new QRMSEntities(); }
        public AccountManagement(QRMSEntities db) { this.db = db ?? DataContext.getEntities(); }

        /// <summary>
        /// Thêm mới tài khoản
        /// </summary>
        /// <param name="_acccount"></param>
        /// <returns>Id account</returns>
        public int Insert(User _acccount)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                var account = db.Users.Add(_acccount);
                db.SaveChanges();
                return account.ID;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return 0;
            }
        }

        /// <summary>
        /// Cập nhật customerId tài khoản
        /// </summary>
        /// <param name="_acccount"></param>
        /// <returns></returns>
        //public int UpdateCustomerId(ACCOUNT _acccount)
        //{
        //    try
        //    {
        //        db = db ?? GlobalVariable.db;
        //        var account = db.Users.Find(_acccount.ID);
        //        account.CUSTOMER_ID = _acccount.CUSTOMER_ID;

        //        return db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.LogError(ex);
        //        return 0;
        //    }
        //}

        /// <summary>
        /// KIểm tra email tồn tài trong db không
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsExistEmail(string email)
        {
            try
            {
                var count = db.Users.Where(x => x.RecordStatus != null && x.RecordStatus != ConstRecordStatus.Deleted)
                    .Where(x => x.Email.ToLower() == email.Trim().ToLower())
                    .Count();
                if (count > 0) return true;

                return false;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// KIểm tra số điện thoại tồn tài trong db không
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool IsExistMobile(string mobile)
        {
            try
            {
                var count = db.Users.Where(x => x.RecordStatus != null && x.RecordStatus != ConstRecordStatus.Deleted)
                    .Where(x => x.Phone == mobile.Trim())
                    .Count();
                if (count > 0) return true;

                return false;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// KIểm tra số điện thoại tồn tài trong db không
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsExistUsername(string user_)
        {
            try
            {
                var count = db.Users.Where(x => x.RecordStatus != null && x.RecordStatus != ConstRecordStatus.Deleted)
                    .Where(x => x.Code == user_.Trim())
                    .Count();
                if (count > 0) return true;

                return false;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Kiểm tra đăng nhập
        /// </summary>
        /// <param name="user">Tên đăng nhập: email hoặc sđt</param>
        /// <param name="pass">Mật khẩu</param>
        /// <returns></returns>
        public User CheckAccount(string user, string pass)
        {
            try
            {
                var result = db.Users.Where(x => x.RecordStatus != null && x.RecordStatus != ConstRecordStatus.Deleted)
                    .Where(x => x.Email.ToLower() == user.ToLower() || x.Phone == user.ToLower() || x.Code == user.ToLower())
                    .Where(x => x.Password == pass)
                    .FirstOrDefault();
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
        public User CheckUserName(string user)
        {
            try
            {
                var result = db.Users.Where(x => x.RecordStatus != null && x.RecordStatus != ConstRecordStatus.Deleted)
                    .Where(x => x.Email.ToLower() == user.ToLower() || x.Phone == user)
                    .FirstOrDefault();
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
        /// <summary>
        /// Lấy tài khoản theo email hoặc số điện thoại
        /// </summary>
        /// <param name="emailOrMobile"></param>
        /// <returns></returns>
        public User GetByEmailOrMobile(string emailOrMobile)
        {
            try
            {
                var account = db.Users.Where(x => x.RecordStatus != null && x.RecordStatus != ConstRecordStatus.Deleted)
                    .Where(x => x.Email.ToLower() == emailOrMobile.Trim().ToLower()
                            || x.Phone == emailOrMobile
                    )
                    .FirstOrDefault();

                return account;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Thay đổi mật khẩu cho tài khoản
        /// </summary>
        /// <param name="accountId">Tài khoản id</param>
        /// <param name="newPass">Password</param>
        /// <returns></returns>
        public int UpdatePassword(int accountId, string newPass)
        {
            try
            {
                string SQL = $"select * from User a where (a.RecordStatus is not null and a.RecordStatus != '{ ConstRecordStatus.Deleted }') ";
                SQL += $"and ID = '{ accountId }'";
                var result = db.Users.SqlQuery(SQL).FirstOrDefault();
                if (result != null)
                {
                    result.Password = newPass;
                    result.UpdateDate = DateTime.Now;
                    result.UpdateUser = result.Code;

                    db.DetachAll<User>();
                    db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return -1;
            }
        }

        /// <summary>
        /// Lấy thông tin account theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Select(int id)
        {
            try
            {
                return db.Users.Find(id);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Cập nhật account
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Edit(User obj)
        {
            try
            {
                db = db ?? GlobalVariable.db;
                db.DetachAll<User>();
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return obj.ID;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Kiểm tra mật khẩu hiện tại
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool CheckCurrentPassowrd(int accountId, string passwordHash)
        {
            try
            {
                var account = db.Users.Where(x => x.RecordStatus != null && x.RecordStatus != ConstRecordStatus.Deleted)
                    .Where(x => x.ID == accountId && x.Password == passwordHash)
                    .FirstOrDefault();
                if (account == null)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
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
