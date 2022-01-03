using QRMSWeb.Constants;
using System;

namespace QRMSWeb.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Ctype { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        //extend

        public string RoleName
        {
            get
            {
                return Constants.Constants.ROLE_ADMIN_USER.Equals(Role) ? "Admin" : "Nhân viên";
            }
        }

        public bool IsLocked
        {
            get
            {
                return ConstRecordStatus.Locked.Equals(RecordStatus);
            }
        }
        public bool IsInUsing { get; set; } = false;
    }
}