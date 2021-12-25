using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB.Common
{
    public class Constant
    {
        public const bool IsAuthentication = true;
        public const int AccountSuccessfully = 0;
        public const int AccountError = 1;
        public const int AccountLocked = 2;
        public const string DeletedRecordStatus = "D";
    }
    public class PasswordEncrypt
    {
        public const string PRIVATE_KEY = "123456123456123456123456";
    }
    public class RecordStatus
    {
        public const string New = "N";
        public const string Update = "U";
        public const string Deleted = "D";
        public const string Locked = "L";
    }
    public class APIResponseCode
    {
        public const string SUCCESS = "200";
        public const string CREATE_SUCCESS = "201";
        public const string BAD_REQUEST = "400";
        public const string NOT_FOUND = "404";
        public const string SERVER_ERROR = "500";
        public const string VALIDATION = "422";
        public const string UNAUTHORIZED = "401";
    }
    public class APIErrorCode
    {
        public const string VALIDATION = "VALIDATION";
        public const string UNAUTHORIZED = "UNAUTHORIZED";
        public const string SYSTEM_ERROR = "SYSTEM_ERROR";
        public const string IMAGE_IS_REQUIRED = "IMAGE_IS_REQUIRED";
        public const string UPLOAD_FAILED = "UPLOAD_FAILED";
        public const string UPLOAD_SUCCESS = "UPLOAD_SUCCESS";
    }
    public class APIMessage
    {
        public const string UNAUTHORIZED = "Không có quyền truy cập";
        public const string SYSTEM_ERROR = "Lỗi hệ thống";
        public const string IMAGE_IS_REQUIRED = "Ảnh không được để trống";
        public const string UPLOAD_FAILED = "Tải tệp không thành công";
        public const string UPLOAD_SUCCESS = "Tải tệp thành công";
    }

    public class ResponseErrorCode
    {
        public const string OK = "0";
        public const string Error = "99";
    }
}
