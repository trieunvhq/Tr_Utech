using QRMS.Helper;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QRMS.AppDAL
{
    public class LoginDAL
    {
        private static DatabaseContext conn = new DatabaseContext();

        //Lưu thông tin tài khoản đăng nhập lần trước
        public static int AddCurrentAccount(AccountModel obj)
        {
            try
            {
                var item = conn.settingModels.Where(a => a.Key.Equals("Account")).FirstOrDefault();
                if (item != null)
                {
                    item.Value = obj.USERNAME;

                    conn.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    conn.SaveChanges();
                }
                else
                {
                    SettingModel setting = new SettingModel()
                    {
                        Key = "Account",
                        Value = obj.USERNAME
                    };

                    conn.settingModels.Add(setting);
                    conn.SaveChanges();
                }

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        //Lưu số lần đăng nhập sai
        public static int NumberLoginFail(string userName)
        {
            try
            {
                var item = conn.accountLogModels.Where(a => a.UserName.Equals(userName)).FirstOrDefault();
                if (item != null)
                {
                    item.NumberFail += 1;

                    conn.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    conn.SaveChanges();

                    return item.NumberFail;
                }
                else
                {
                    var allRecord = conn.accountLogModels.ToList();
                    conn.accountLogModels.RemoveRange(allRecord);
                    conn.SaveChanges();

                    AccountLogModel accountLog = new AccountLogModel()
                    {
                        UserName = userName,
                        NumberFail = 1
                    };

                    conn.accountLogModels.Add(accountLog);
                    conn.SaveChanges();

                    return accountLog.NumberFail;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        //Reset số lần đăng nhập sai
        public static int ResetNumberLoginFail()
        {
            try
            {
                var allRecord = conn.accountLogModels.ToList();
                conn.accountLogModels.RemoveRange(allRecord);
                conn.SaveChanges();

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
