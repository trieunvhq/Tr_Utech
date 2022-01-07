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

        #region paging
        public const int Max_Row = 10;
        public const int NumPage = 10;
        #endregion
    }
    public class ConstPasswordEncrypt
    {
        public const string PRIVATE_KEY = "123456123456123456123456";
    }

    public class ConstInputStatus
    {
        public const string Enough = "Y";
        public const string NotEnough = "D";
        public const string NotYetEntered = "N"; 
    }

    public class ConstTransferStatus
    {
        public const string Delivered = "Y"; 
        public const string NotDelivered = "N"; 
    }

    public class ConstTransferType
    {
        public const string WarehouseTransfer = "C"; 
        public const string Import = "I";
        public const string Export = "O";
        public const string Inventory = "K";
    }

    public class ConstTransferType_Web
    {
        public const string ChuyenKho = "C";
        public const string NhapKho = "I";
        public const string XuatKho = "O";
        public const string KiemKe = "K";
        public const string Import = "IM";
        public const string Export = "EX";
    }
    
    public class ConstRecordStatus
    {
        public const string New = "N";
        public const string Update = "U";
        public const string Deleted = "D";
        public const string Locked = "L";
    }

    public class ConstAPIResponseCode
    {
        public const string SUCCESS = "200";
        public const string CREATE_SUCCESS = "201";
        public const string BAD_REQUEST = "400";
        public const string NOT_FOUND = "404";
        public const string SERVER_ERROR = "500";
        public const string VALIDATION = "422";
        public const string UNAUTHORIZED = "401";
    }
    public class ConstAPIErrorCode
    {
        public const string VALIDATION = "VALIDATION";
        public const string UNAUTHORIZED = "UNAUTHORIZED";
        public const string SYSTEM_ERROR = "SYSTEM_ERROR";
        public const string IMAGE_IS_REQUIRED = "IMAGE_IS_REQUIRED";
        public const string UPLOAD_FAILED = "UPLOAD_FAILED";
        public const string UPLOAD_SUCCESS = "UPLOAD_SUCCESS";
    }
    public class ConstAPIMessage
    {
        public const string UNAUTHORIZED = "Không có quyền truy cập";
        public const string SYSTEM_ERROR = "Lỗi hệ thống";
        public const string IMAGE_IS_REQUIRED = "Ảnh không được để trống";
        public const string UPLOAD_FAILED = "Tải tệp không thành công";
        public const string UPLOAD_SUCCESS = "Tải tệp thành công";
    }

    public class ConstResponseErrorCode
    {
        public const string OK = "0";
        public const string Error = "99";
    }

    public class ConstItemType
    {
        public const string DungCu = "DC";
        public const string NguyenLieu = "NL";
        public const string ThanhPham = "TP";
    }

    public class ConstPrintStatus
    {
        public const string DaIn = "Y";
        public const string ChuaIn = "N";
        public const string DangIn = "D";
    }
    public class ConstExportStatus
    {
        public const string DaExport = "Y";
        public const string ChuaExport = "N";
    }
    public class ConstGetDataStatus
    {
        public const string DaLay = "Y";
        public const string ChuaLay = "N";
    }
    public class ConstTransactionType
    {
        public const string NhapKho = "I";
        public const string XuatKho = "O";
        public const string ChuyenKho = "C";
        public const string KiemKe = "K";
        public const string Import = "IM";
        public const string Export = "EX";

    }
}
