using BPL.Models.Users;
using DAL.Factory.HT.Users;
using HDLIB.Common;
using HDLIB;
using System;
using DAL;

namespace BPL.Master
{
    public class AccountBPL
    {
        QRMSEntities db;
        public AccountBPL() { db = new QRMSEntities(); }
        public AccountBPL(QRMSEntities db) { this.db = db ?? new QRMSEntities(); }

        /// <summary>
        /// Kiểm tra login
        /// </summary>
        /// <param name="input">thông tin login</param>
        /// <param name="err_code"></param>
        /// <param name="err_msg"></param>
        /// <returns></returns>
        public User CheckAccount(string username_, string pass_, out string err_code, out string err_msg)
        {
            try
            {
                var accountManager = new AccountManagement(db);
                var result = accountManager.CheckAccount(username_, Cipher.Encrypt(pass_, PasswordEncrypt.PRIVATE_KEY));
                if (result != null)
                {
                    var item = new User();
                    item.CopyPropertiesFrom(result);
                    if (item.RecordStatus.Equals(RecordStatus.Locked))
                    {
                        err_code = "5";
                        err_msg = "Đăng nhập thất bại, tài khoản đã bị khóa";
                    }
                    else
                    {
                        err_code = "0";
                        err_msg = "Đăng nhập thành công";
                    }

                    return result;
                }
                err_code = "4";
                err_msg = "Dăng nhập thất bại, tên tài khoản hoặc mật khẩu không đúng";
                return null;
            }
            catch (Exception ex)
            {
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                Logging.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        /// <param name="emailOrMobile"></param>
        /// <param name="err_code"></param>
        /// <param name="err_msg"></param>
        public void LockAccount(string emailOrMobile, out string err_code, out string err_msg)
        {

            try
            {
                var accountManagement = new AccountManagement(db);
                var account = accountManagement.GetByEmailOrMobile(emailOrMobile);
                if (account == null)
                {
                    err_code = "1";
                    err_msg = "Không tìm thấy tài khoản";
                    return;
                }

                // Khóa tài khoản
                account.RecordStatus = RecordStatus.Locked;
                accountManagement.Edit(account);

                err_code = "0";
                err_msg = "Khóa tài khoản thành công";
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                throw;
            }
        }


        public void AddAccount(RegisterInputModel accountInput, out string err_code, out string err_msg)
        {
            try
            {
                var accountManagement = new AccountManagement(db);
                bool isExistEmail = accountManagement.IsExistEmail(accountInput.Email);
                bool isExistMobile = accountManagement.IsExistMobile(accountInput.Phone);
                bool isExistUsername = accountManagement.IsExistUsername(accountInput.Code);

                if (isExistUsername)
                {
                    err_code = "1";
                    err_msg = "Không tạo được tài khoản, Tên tài khoản đã tồn tại!";
                    return;
                }
                else if (isExistEmail)
                {
                    err_code = "2";
                    err_msg = "Không tạo được tài khoản, Email đã tồn tại!";
                    return;
                }
                else if (isExistMobile)
                {
                    err_code = "3";
                    err_msg = "Không tạo được tài khoản, Số điện thoại đã tồn tại!";
                    return;
                }

                User account = new User();
                account.CopyPropertiesFrom(accountInput);
                account.RecordStatus = "N";
                account.CreateDate = DateTime.Now;
                account.Password = Cipher.Encrypt(accountInput.Password, PasswordEncrypt.PRIVATE_KEY);

                int id = new AccountManagement(db).Insert(account);
                if (id > 0)
                {
                    err_code = "0";
                    err_msg = "Tạo tài khoản thành công!";
                    return;
                }
                else
                {
                    err_code = "4";
                    err_msg = "Tạo tài khoản không thành công. Vui lòng thử lại sau!";
                    return;
                }    
            }
            catch (Exception ex)
            {
                err_code = "5";
                err_msg = ex.Message;
                Logging.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Thay đổi mật khẩu
        /// </summary>
        /// <param name="input"></param>
        /// <param name="err_code"></param>
        /// <param name="err_msg"></param>
        public void ChangePassword(ChangePasswordModel input, out string err_code, out string err_msg)
        {
            try
            {
                var accountManagement = new AccountManagement(db);

                // Kiểm tra mật khẩu cũ
                string currentPasswordHash = Cipher.Encrypt(input.CurrentPassword, PasswordEncrypt.PRIVATE_KEY);
                bool checkCurrentPassowrd = accountManagement.CheckCurrentPassowrd(input.AccountId, currentPasswordHash);
                if (checkCurrentPassowrd == false)
                {
                    err_code = "1";
                    err_msg = "Mật khẩu hiện tại không đúng";
                    return;
                }

                // Cập nhật mật khẩu mới
                string newPasswordHash = Cipher.Encrypt(input.NewPassword, PasswordEncrypt.PRIVATE_KEY);
                int updatePassword = accountManagement.UpdatePassword(input.AccountId, newPasswordHash);
                if (updatePassword == 0)
                {
                    err_code = "2";
                    err_msg = "Không tìm thấy tài khoản có ID = " + input.AccountId;
                    return;
                }
                else if (updatePassword == -1)
                {
                    err_code = "3";
                    err_msg = "Cập nhật mật khẩu mới thất bại";
                    return;
                }

                err_code = "0";
                err_msg = "Cập nhật mật khẩu thành công";
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                throw;
            }
        }

      

        /// <summary>
        /// Lấy thông tin tab tài khoản
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="err_code"></param>
        /// <param name="err_msg"></param>
        /// <returns></returns>
        public User GetAccountTabInfo(int accountId, out string err_code, out string err_msg)
        {
            try
            {
                var accountManagement = new AccountManagement();
                // CustomerName
                var account = accountManagement.Select(accountId);
                if (account == null)
                {
                    err_code = "1";
                    err_msg = "Không tìm thấy tài khoản có ID = " + accountId;
                    return null;
                }
                else
                {
                    err_code = "0";
                    err_msg = "Lấy thông tin thành công";
                    return account;
                }    
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// Tạo mật khẩu mới
        /// </summary>
        /// <param name="input"></param>
        /// <param name="err_code"></param>
        /// <param name="err_msg"></param>
        public void NewPassword(ChangePasswordModel input, out string err_code, out string err_msg)
        {
            try
            {
                var accountManagement = new AccountManagement(db);

                // Cập nhật mật khẩu mới
                string newPasswordHash = Cipher.Encrypt(input.NewPassword, PasswordEncrypt.PRIVATE_KEY);
                int updatePassword = accountManagement.UpdatePassword(input.AccountId, newPasswordHash);
                if (updatePassword == 0)
                {
                    err_code = "1";
                    err_msg = "Không tìm thấy tài khoản có ID = " + input.AccountId;
                    return;
                }
                else if (updatePassword == -1)
                {
                    err_code = "2";
                    err_msg = "Tạo mật khẩu mới thất bại";
                    return;
                }

                err_code = "0";
                err_msg = "Tạo mật khẩu thành công";
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                err_code = ResponseErrorCode.Error.ToString();
                err_msg = ex.Message;
                throw;
            }
        }
    }
}
