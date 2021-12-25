using System;
using System.Data;
using BPL.Models;
using BPL.Models.Users;
using DAL;
using HDLIB.Common;

namespace BPL.Master
{
    public class AcountLogin
    {
        //public AcountModel checkLogin(string taiKhoan_, string matKhau_)
        //{
        //    AcountModel tr = new AcountModel();

        //    try
        //    {
        //        using(clsUser cls = new clsUser())
        //        {
        //            DataTable dt = cls.Tr_User_KiemTraDangNhap(taiKhoan_, matKhau_);
        //            if (dt.Rows.Count > 0)
        //            {
        //                tr.isTrue = true;
        //                tr.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
        //                tr.Ctype = dt.Rows[0]["Ctype"].ToString();
        //                tr.Code = dt.Rows[0]["Code"].ToString();
        //                tr.FullName = dt.Rows[0]["FullName"].ToString();
        //                tr.Password = dt.Rows[0]["Password"].ToString();
        //                tr.Phone = dt.Rows[0]["Phone"].ToString();
        //                tr.Email = dt.Rows[0]["Email"].ToString();
        //                tr.Role = dt.Rows[0]["Role"].ToString();
        //                tr.WarehouseCode = dt.Rows[0]["WarehouseCode"].ToString();
        //                tr.RecordStatus = dt.Rows[0]["RecordStatus"].ToString();

        //                if (!string.IsNullOrEmpty(dt.Rows[0]["CreateDate"].ToString()))
        //                    tr.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"].ToString());
        //                tr.CreateUser = dt.Rows[0]["CreateUser"].ToString();

        //                if (!string.IsNullOrEmpty(dt.Rows[0]["UpdateDate"].ToString()))
        //                    tr.UpdateDate = Convert.ToDateTime(dt.Rows[0]["UpdateDate"].ToString());

        //                tr.UpdateUser = dt.Rows[0]["UpdateUser"].ToString();
        //            }
        //            else
        //                tr.isTrue = false;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        Logging.LogMessage(ex.ToString());
        //    }

        //    return tr;
        //}


        ////Insert
        //public int CreateAccount(AcountModel dt)
        //{
        //    try
        //    {
        //        using (clsUser cls = new clsUser())
        //        {
        //            if ((cls.Tr_User_CheckAcountTonTai(dt.Code)).Rows.Count == 0)
        //            {
        //                cls.sCtype = dt.Ctype;
        //                cls.sCode = dt.Code;
        //                cls.sFullName = dt.FullName;
        //                cls.sPassword = dt.Password;
        //                cls.sPhone = dt.Phone;
        //                cls.sEmail = dt.Email;
        //                cls.sRole = dt.Role;
        //                cls.sWarehouseCode = dt.WarehouseCode;
        //                cls.sRecordStatus = dt.RecordStatus;
        //                if (dt.CreateDate != null)
        //                    cls.daCreateDate = Convert.ToDateTime(dt.CreateDate);

        //                if (dt.UpdateDate != null)
        //                    cls.daUpdateDate = Convert.ToDateTime(dt.UpdateDate);
        //                cls.sCreateUser = dt.CreateUser;
        //                cls.sUpdateUser = dt.UpdateUser;

        //                if (cls.Insert())
        //                    return 1; //thành công
        //                else
        //                    return 0; //Không tạo được tài khoản
        //            }
        //            else
        //                return 2; //tài khoản đã tồn tại
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.LogMessage(ex.ToString());
        //        return -1;
        //    }

        //}

        ////
        ////Update
        //public int UpdateAccount(AcountModel dt)
        //{
        //    try
        //    {
        //        using (clsUser cls = new clsUser())
        //        {
        //            cls.iID = dt.ID;
        //            cls.sCtype = dt.Ctype;
        //            cls.sCode = dt.Code;
        //            cls.sFullName = dt.FullName;
        //            cls.sPassword = dt.Password;
        //            cls.sPhone = dt.Phone;
        //            cls.sEmail = dt.Email;
        //            cls.sRole = dt.Role;
        //            cls.sWarehouseCode = dt.WarehouseCode;
        //            cls.sRecordStatus = dt.RecordStatus;
        //            if (dt.CreateDate != null)
        //                cls.daCreateDate = Convert.ToDateTime(dt.CreateDate);

        //            if (dt.UpdateDate != null)
        //                cls.daUpdateDate = Convert.ToDateTime(dt.UpdateDate);
        //            cls.sCreateUser = dt.CreateUser;
        //            cls.sUpdateUser = dt.UpdateUser;

        //            if (cls.Update())
        //                return 1; //thành công
        //            else
        //                return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.LogMessage(ex.ToString());
        //        return -1;
        //    }

        //}

        ////Update
        //public int DeleteAccount(int id)
        //{
        //    try
        //    {
        //        using (clsUser cls = new clsUser())
        //        {
        //            cls.iID = id;

        //            if (cls.Delete())
        //                return 1; //thành công
        //            else
        //                return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.LogMessage(ex.ToString());
        //        return -1;
        //    }

        //}
    }
}
