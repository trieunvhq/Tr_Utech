namespace QRMSWeb.Constants
{
    public class Constants
    {
        public const string ROLE_ADMIN_USER = "admin";

        public const int MAX_UPLOAD_FILE = 209715200;

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
    public class ConstInputStatus
    {
        public const string DaNhapDu = "Y";
        public const string ChuaNhap = "N";
        public const string DangNhap = "D";
    }
    public class ConstExportStatus
    {
        public const string DaExport = "Y";
        public const string ChuaExport = "N";
        public const string DangExport = "D";
    }
    public class ConstGetDataStatus
    {
        public const string DaLay = "Y";
        public const string ChuaLay = "N";
    }
    public class ConstTransferStatus
    {
        public const string DaChuyen = "Y";
        public const string ChuaChuyen = "N";
    }
    public class ConstTransferType
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
        public const string Delete = "D";
        public const string Update = "U";
        public const string Create = "N";
        public const string Locked = "L";
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