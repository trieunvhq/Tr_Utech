using BPL.Factory.Web;
using BPL.Models.Web;
using DAL;
using DAL.Factory.Web.Users;
using DAL.FactoryDAL.WebAdmin.Account;
using HDLIB;
using HDLIB.Common;
using System;
using System.Collections.Generic;

namespace BLL.FactoryBLL.Web.Users
{
    public class UserBLL: BaseBLL
    {
        
        public int CheckAccount(string ipAddress, string deviceName, string user, string pass, out UserModel objItem)
        {
            try
            {
                string _pass = Cipher.Encrypt(pass, PasswordEncrypt.PRIVATE_KEY);
                var accountManager = new UserManagement(db);
                var result = accountManager.CheckAccount(user, _pass);
                if (result != null)
                {
                    var item = new UserModel();
                    item.CopyPropertiesFrom(result);
                    item.Role = result.Role;

                    if (item.RecordStatus.Equals(RecordStatus.Locked))
                    {
                        objItem = item;
                        return Constant.AccountLocked;
                    }
                    else
                    {
                        var addAccountLog = new LoginLogManagement(db).AddLoginLog(ipAddress, deviceName, item.ID, item.Code);
                        if (addAccountLog == 0)
                        {
                            objItem = item;
                            return Constant.AccountSuccessfully;
                        }
                    }
                }
                objItem = null;
                return Constant.AccountError;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }


        public int LockAccount(string userName, int? userID)
        {
            try
            {
                var accountManager = new UserManagement(db);
                return accountManager.LockAccount(userName, userID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }


        public int UpdatePassword(string username, string newPass, int? userID)
        {
            try
            {
                string _newPass = Cipher.Encrypt(newPass, PasswordEncrypt.PRIVATE_KEY);
                return new UserManagement(db).UpdatePassword(username, _newPass, userID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        
        public int ResetPassword(string username, string newPass)
        {
            try
            {
                //const string temp = "0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!@#$%*";
                var random = new Random();
                /*
                string newPass = new string(Enumerable.Repeat(temp, 8)
                                    .Select(s => s[random.Next(s.Length)]).ToArray());
                */
                string _newPassEnc = Cipher.Encrypt(newPass, PasswordEncrypt.PRIVATE_KEY);
                var result = new UserManagement(db).UpdatePassword(username, _newPassEnc, 0);
                if (result != 1) return -1;
                //send mail
                /*
                EmailModel emailModel = new EmailModel();
                emailModel.FromEmail = "pjcmailquantri@gmail.com";
                emailModel.FromPassword = "pjc!QuanTri12345678";
                emailModel.To = email;
                emailModel.Subject = "Lấy lại mật khẩu";
                emailModel.Body = $"Mật khẩu mới của bạn là: {newPass}. <br/><i><b>Hãy đổi mật khẩu ngay sau đăng nhập lại.</b></i>";
                emailModel.IsBodyHtml = true;
                MailHelper.SendEmail(emailModel);
                */
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw ex;
            }
            return 1;
        }


        public int AddAccount(List<UserModel> lstUserModel, string userName)
        {
            try
            {
                using (var transac = DAL.DataContext.GetTransaction(db))
                {
                    try
                    {
                        var AccManager = new UserManagement(db);
                        List<DAL.User> lstUsers = new List<User>();
                        foreach (var item in lstUserModel)
                        {
                            var user = new User();
                            user.CopyPropertiesFrom(item);
                            user.Password = Cipher.Encrypt(item.Password, PasswordEncrypt.PRIVATE_KEY);
                            user.RecordStatus = RecordStatus.New;
                            user.CreateDate = DateTime.Now;
                            user.CreateUser = userName;
                            lstUsers.Add(user);
                        }
                        var result = AccManager.Insert(lstUsers.ToArray());
                        if (result <= 0)
                        {
                            transac.Rollback();
                        } else { 
                            transac.Commit();
                        }
                        return result;
                    }
                    catch (Exception e)
                    {
                        transac.Rollback();
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public int EditAcc(UserModel _user, string userName)
        {
            try
            {
                using (var transac = DAL.DataContext.GetTransaction(db))
                {
                    try
                    {
                        var AccManager = new UserManagement(db);
                        var _origin = AccManager.Select(_user.ID);
                        if (_origin == null) return -1;
                        _origin.CopyPropertiesFrom(_user, true);
                        _origin.RecordStatus = RecordStatus.Update;
                        _origin.UpdateDate = DateTime.Now;
                        _origin.UpdateUser = userName;
                        var result = AccManager.Update(_origin);
                        if (result <= 0)
                        {
                            transac.Rollback();
                        }
                        else
                        {
                            transac.Commit();
                        }
                        return result;
                    }
                    catch (Exception e)
                    {
                        transac.Rollback();
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public int DeleteAcc(int ID, int? userID)
        {
            try
            {
                var AccManager = new UserManagement(db);
                var _origin = AccManager.Select(ID);
                if (_origin == null) return -1;
                
                return AccManager.Update(_origin);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int LockAcc(int ID, string userName)
        {
            try
            {
                var AccManager = new UserManagement(db);
                var _origin = AccManager.Select(ID);
                if (_origin == null) return -1;
                _origin.RecordStatus = RecordStatus.Locked;
                _origin.UpdateDate = DateTime.Now;
                _origin.UpdateUser = userName;
                return AccManager.Update(_origin);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public int UnlockAcc(int ID, string userName)
        {
            try
            {
                var AccManager = new UserManagement(db);
                var _origin = AccManager.Select(ID);
                if (_origin == null) return -1;
                _origin.RecordStatus = RecordStatus.Update;
                _origin.UpdateDate = DateTime.Now;
                _origin.UpdateUser = userName;
                return AccManager.Update(_origin);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        // NEW
        public UserModel GetAccountById(int id, bool withLock)
        {
            try
            {
                var user = new UserManagement(db).FindByPk(id, withLock);
                if (user != null) { 
                    UserModel userModel = new UserModel();
                    userModel.CopyPropertiesFrom(user);
                    return userModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public UserModel GetAccountByUserName(string username)
        {
            try
            {
                var user = new UserManagement(db).FindByUserName(username);
                if (user != null)
                {
                    UserModel userModel = new UserModel();
                    userModel.CopyPropertiesFrom(user);
                    return userModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }


        public int AccountLogin(UserModel account, string Password, string IpAddress, string DeviceInfo)
        {
            try
            {
                var accountManager = new UserManagement(db);
                var loginLogManager = new LoginLogManagement(db);

                string _Password = Cipher.Encrypt(Password, PasswordEncrypt.PRIVATE_KEY);

                if (account.Password != _Password) return -1;
                /*
                var loginLog = new LOGIN_LOG() {
                    LOG_TIME = DateTime.Now,
                    IP_ADDRESS = IpAddress,
                    DEVICE_INFO = DeviceInfo,
                    USER_ID = account.ID,
                    USER_NAME = account.USERNAME
                };
                
                var loginLogRes = new LoginLogManagement(db).Insert(loginLog);

                if (loginLogRes <= 0) return -1;
                */
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.LogError(ex);
                throw;
            }
        }
    }
}
