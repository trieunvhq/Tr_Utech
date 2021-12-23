using System;
using System.Data;
using BPL.Models;
using DAL;
using HDLIB.Common;

namespace BPL.Acounts
{
    public static class AcountLogin
    {
        public static AcountModel checkLogin(string taiKhoan_, string matKhau_)
        {
            AcountModel tr = new AcountModel();

            try
            {
                using(clsUser cls = new clsUser())
                {
                    DataTable dt = cls.Tr_User_KiemTraDangNhap(taiKhoan_, matKhau_);
                    if (dt.Rows.Count > 0)
                    {
                        tr.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                        tr.Ctype = dt.Rows[0]["Ctype"].ToString();
                        tr.Code = dt.Rows[0]["Code"].ToString();
                        tr.FullName = dt.Rows[0]["FullName"].ToString();
                        tr.Password = dt.Rows[0]["Password"].ToString();
                        tr.Phone = dt.Rows[0]["Phone"].ToString();
                        tr.Email = dt.Rows[0]["Email"].ToString();
                        tr.Role = dt.Rows[0]["Role"].ToString();
                        tr.WarehouseCode = dt.Rows[0]["WarehouseCode"].ToString();
                        tr.RecordStatus = dt.Rows[0]["RecordStatus"].ToString();
                        if (dt.Rows[0]["CreateDate"] != null)
                            tr.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"].ToString());
                        tr.CreateUser = dt.Rows[0]["CreateUser"].ToString();

                        if (dt.Rows[0]["UpdateDate"] != null)
                            tr.UpdateDate = Convert.ToDateTime(dt.Rows[0]["UpdateDate"].ToString());

                        tr.UpdateUser = dt.Rows[0]["UpdateUser"].ToString();
                        tr.isTrue = true;
                    }
                    else
                        tr.isTrue = false;
                }
            }
            catch(Exception ex)
            {
                Logging.LogMessage(ex.ToString());
            }

            return tr;
        }
    }
}
