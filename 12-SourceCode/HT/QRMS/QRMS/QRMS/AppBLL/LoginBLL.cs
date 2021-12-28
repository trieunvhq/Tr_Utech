using QRMS.AppLIB.Common;
using QRMS.Helper;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace QRMS.AppBLL
{
    public class LoginBLL
    {
        //Lưu thông tin tài khoản đăng nhập lần trước
        public static int AddCurrentAccount(AccountModel obj)
        {
            return AppDAL.LoginDAL.AddCurrentAccount(obj);
        }

        //Lưu số lần đăng nhập sai
        public static int NumberLoginFail(string userName)
        {
            return AppDAL.LoginDAL.NumberLoginFail(userName);
        }

        //Reset số lần đăng nhập sai
        public static int ResetNumberLoginFail()
        {
            return AppDAL.LoginDAL.ResetNumberLoginFail();
        }
    }
}
