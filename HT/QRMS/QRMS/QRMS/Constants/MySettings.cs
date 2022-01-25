using System.Collections.Generic;
using QRMS.Controls;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using QRMS.Models;
using System;
using QRMS.API;
using QRMS.AppLIB.Common;
using System.Data;
using System.Text;
using QRMS.Models.Shares;

namespace QRMS.Constants
{
    public static class MySettings
    {

        public static string ItemContract_Status { get; set; }
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }
        #region Setting Constants

        private const string Status_QRMS_KHKey = "last_Status_QRMS_KHKey";

        private const string WidthSettings_QRMS_KHKey = "last_Width_QRMSKey";
        private const string HeightSettings_QRMS_KHKey = "last_Height_QRMS_KHKey";

        private const string HasNotch_Settings_QRMS_KHKey = "last_HasNotch_QRMSKey";
        private const string Height_KeySoft_Settings_QRMS_KHKey = "last_Height_KeySoft_QRMS_KHKey";
        private const string Height_Notch_Settings_QRMS_KHKey = "last_Height_Notch_QRMSKey";
        private const string Height_Keyboard_Settings_QRMS_KHKey = "last_Height_Keyboard_QRMS_KHKey";
        #endregion
        /// <summary>
        ///
        /// 
        /// </summary>
        ///
        private const string UserID_QRMS_KHKey = "last_UserID_QRMS_KHKey";
        public static int UserID
        {
            get
            {
                return AppSettings.GetValueOrDefault<int>(UserID_QRMS_KHKey, 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(UserID_QRMS_KHKey, value);
            }
        }

        //pas:
        private const string Tr_Pass_QRMS_KHKey = "last_Tr_Pass__QRMS_KHKey";
        public static string Tr_Pass
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(Tr_Pass_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(Tr_Pass_QRMS_KHKey, value);
            }
        }


        //
        private const string IsPass_QRMS_KHKey = "last_IsPass_QRMS_KHKey";
        public static bool IsPass
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(IsPass_QRMS_KHKey, true);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(IsPass_QRMS_KHKey, value);
            }
        }
        //
        private const string IsVanTay_QRMS_KHKey = "last_IsVanTay_QRMS_KHKey";
        public static bool IsVanTay
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(IsVanTay_QRMS_KHKey, false);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(IsVanTay_QRMS_KHKey, value);
            }
        }
        //
        private const string IsKhuonMat_QRMS_KHKey = "last_IsKhuonMat_QRMS_KHKey";
        public static bool IsKhuonMat
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(IsKhuonMat_QRMS_KHKey, false);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(IsKhuonMat_QRMS_KHKey, value);
            }
        }
        //
        //
        private const string IsP_V_K_QRMS_KHKey = "last_IsP_V_K_QRMS_KHKey";
        public static int IsP_V_K
        {
            get
            {
                return AppSettings.GetValueOrDefault<int>(IsP_V_K_QRMS_KHKey, 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(IsP_V_K_QRMS_KHKey, value);
            }
        }
        //
        private const string FULL_NAME_QRMS_KHKey = "last_FULL_NAME_QRMS_KHKey";
        public static string FULL_NAME
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(FULL_NAME_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(FULL_NAME_QRMS_KHKey, value);
            }
        }
        //
        private const string UserName_QRMS_KHKey = "last_UserName_QRMS_KHKey";
        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(UserName_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(UserName_QRMS_KHKey, value);
            }
        }
        private const string Password_QRMS_KHKey = "last_ Password_QRMS_KHKey";
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(Password_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(Password_QRMS_KHKey, value);
            }
        }
        //
        private const string TenMay_QRMS_KHKey = "last_TenMay_QRMS_KHKey";
        public static string TenMay
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(TenMay_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(TenMay_QRMS_KHKey, value);
            }
        }
        private const string Service_QRMS_KHKey = "last_Service_QRMS_KHKey";
        public static string Service
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(Service_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(Service_QRMS_KHKey, value);
            }
        }
        //
        private const string CodeKho_QRMS_KHKey = "last_CodeKho_QRMS_KHKey";
        public static string CodeKho
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(CodeKho_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(CodeKho_QRMS_KHKey, value);
            }
        }//
        private const string MaKho_QRMS_KHKey = "last_MaKho_QRMS_KHKey";
        public static string MaKho
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(MaKho_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(MaKho_QRMS_KHKey, value);
            }
        }//

        private const string LenhKiemKe_QRMS_KHKey = "last_LenhKiemKe_QRMS_KHKey";
        public static string LenhKiemKe
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(LenhKiemKe_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(LenhKiemKe_QRMS_KHKey, value);
            }
        }
        //
        private const string LenhDiChuyen_QRMS_KHKey = "last_LenhDiChuyen_QRMS_KHKey";
        public static string LenhDiChuyen
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(LenhDiChuyen_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(LenhDiChuyen_QRMS_KHKey, value);
            }
        }
        //
        public static double Haft_H
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) / 2;
            }
        }

        public static double W1
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) / 3;
            }
        }
        public static double W2
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) * 2 / 3;
            }
        }
        public static double W_6
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) / 6;
            }
        }
        public static double W_07
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) / 9;
            }
        }
        public static double Haft_W
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) / 2;
            }
        }
        public static double W_15
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) * 2 / 9;
            }
        }
        //
        public static string Status
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(Status_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(Status_QRMS_KHKey, value);
            }
        }
        public static double w_QRMS16
        {
            get
            {
                return (w_QRMS - 32 - 16);
            }
        }
        public static double w_QRMS16_half
        {
            get
            {
                return (w_QRMS - 32 - 16) / 2;
            }
        }
        public static double w_QRMS
        {
            get
            {
                return AppSettings.GetValueOrDefault<double>(WidthSettings_QRMS_KHKey, 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue<double>(WidthSettings_QRMS_KHKey, value);
            }
        }
        public static double h_QRMS
        {
            get
            {
                return AppSettings.GetValueOrDefault<double>(HeightSettings_QRMS_KHKey, 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue<double>(HeightSettings_QRMS_KHKey, value);
            }
        }

        public static bool HasNotch
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(HasNotch_Settings_QRMS_KHKey, false);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(HasNotch_Settings_QRMS_KHKey, value);
            }
        }
        public static double Height_KeySoft
        {
            get
            {
                return AppSettings.GetValueOrDefault<double>(Height_KeySoft_Settings_QRMS_KHKey, 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue<double>(Height_KeySoft_Settings_QRMS_KHKey, value);
            }
        }
        public static double Height_Notch
        {
            get
            {
                return AppSettings.GetValueOrDefault<double>(Height_Notch_Settings_QRMS_KHKey, 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue<double>(Height_Notch_Settings_QRMS_KHKey, value);
            }
        }
        public static int Height_KeyBoard
        {
            get
            {
                return AppSettings.GetValueOrDefault<int>(Height_Keyboard_Settings_QRMS_KHKey, 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(Height_Keyboard_Settings_QRMS_KHKey, value);
            }
        }
        public static List<string> _lst = new List<string>{"@", "!", "~" , "`", "#", "$", "%"
        , "^" , "&" , "*" , "+" , "=" , "{" , "[" , "}" , "]" , ":" , ";" , "“"
        , "‘" , "<" , ">" , "?"};
        public static string ReplaceSpecialChar(string str)
        {
            for (int i = 0; i < _lst.Count; ++i)
            {
                str = str.Replace(_lst[i], "");
            }
            return str;
        }
        public static List<string> _lst_page = new List<string>();


        public static double W_140_344
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) * 140 / 344;
            }
        }
        public static double W_328_780
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) * 328 / 780;
            }
        }
        public static double W_160_363
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) * 160 / 363;
            }
        }

        public static double W_103_363
        {
            get
            {
                return (MySettings.w_QRMS - 40) * (double)103 / (double)363 + 20;
            }
        }
        public static double W_2_3
        {
            get
            {
                return (MySettings.w_QRMS * 2.0 / 3.0);
            }
        }
        public static double W_23_30
        {
            get
            {
                return (MySettings.w_QRMS * 23.0 / 30.0);
            }
        }
        public static double W_13_20
        {
            get
            {
                return (MySettings.w_QRMS * 13.0 / 20.0);
            }
        }
        public static double W_454_400
        {
            get
            {
                return (MySettings.w_QRMS - 180) * (double)400 / (double)454;
            }
        }
        public static double h_QRMS_1_3
        {
            get
            {
                return ((MySettings.h_QRMS) / (double)3) - 40.5;
            }
        }
        public static double h_QRMS_2_3
        {
            get
            {
                return ((MySettings.h_QRMS) * 2 / (double)3) - 40.5;
            }
        }

        public static double h_2_300
        {
            get
            {
                return (MySettings.h_QRMS - 300) / 2;
            }
        }
        public static double Haft_W_2_3
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) * 2 / 3;
            }
        }
        public static double Haft_W_32
        {
            get
            {
                return (MySettings.w_QRMS - 32);
            }
        }
        public static double W_32
        {
            get
            {
                return (MySettings.w_QRMS - 32);
            }
        }
        public static double Haft_W_1_3
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) / 3;
            }
        }
        public static double Haft_W_18_3
        {
            get
            {
                return (MySettings.w_QRMS - 32) * 1.8 / 3;
            }
        }
        public static double Haft_W_12_3
        {
            get
            {
                return (MySettings.w_QRMS - 32) * 1.2 / 3;
            }
        }
        public static double W_HTTT_1
        {
            get
            {
                if (MySettings.w_QRMS < 400)
                    return 35;
                else
                    return 28;
            }
        }

        public static double W_HTTT_2
        {
            get
            {
                if (MySettings.w_QRMS < 400)
                    return 50;
                else
                    return 40;
            }
        }

        public static int Index_Page { get; set; }
        public static string To_Page { get; set; }


        public static string JSON { get; set; }
        public static string Token { get; set; }
        public static string Title { get; set; }
        public static bool Vi_En { get; set; }

        public static string screenType { get; set; }

        public static string COMMON_TYPE_CODE { get; set; }
        public static string PURPOSE_CODE { get; set; }
        public static string SEAT { get; set; }
        public static string VEH_TYPE { get; set; }
        public static string WEIGHT_TON { get; set; }
        public static bool IsLuuVaThoat { get; set; }
        public static TTNHDView _TTNHDView { get; set; }
        public static int SoChoNgoi { get; set; }

        public static List<string> _lsthtml = new List<string>{"&Agrave;", "&Aacute;", "&Acirc;" , "&Atilde;", "&Egrave;"
            , "&Eacute;", "&Ecirc;"
        , "&Igrave;" , "&Iacute;" , "&Ograve;" , "&Oacute;" , "&Ocirc;" , "&Otilde;" , "&Ugrave;" , "&Uacute;"
            , "&Yacute;" , "&agrave;" , "&aacute;" , "&acirc;"
        , "&atilde;" , "&egrave;" , "&eacute;" , "&ecirc;" ,
            "&igrave;", "&iacute;", "&ograve;", "&oacute;", "&ocirc;", "&otilde;", "&ugrave;", "&uacute;", "&yacute;", "&ETH;"
        , "&hellip;"};

        public static List<string> _lstGlyph = new List<string>{"À", "Á", "Â" , "Ã", "È"
            , "É", "Ê"
        , "Ì" , "Í" , "Ò" , "Ó" , "Ô" , "Õ" , "Ù" , "Ú"
            , "Ý" , "à" , "á" , "â"
        , "ã" , "è" , "é" , "ê" ,
            "ì", "í", "ò", "ó", "ô", "õ", "ù", "ú", "ý", "Ð", "…"};
        public static string ConvertHtmlToGlyph(string str)
        {
            for (int i = 0; i < _lsthtml.Count; ++i)
            {
                str = str.Replace(_lsthtml[i], _lstGlyph[i]);
            }
            return str;
        }


        public static string DecodeFromUtf8(string utf8String)
        {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)utf8String[i];
            }

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        //❖ ngoài
        //✿ trong
        public static string MyToString(TransactionHistoryModel ht)
        {
            string result_ = "";
            string orderno_ = "";
            try
            {
                if (ht.TransactionType == "K")
                {
                    orderno_ = ht.OrderNo;
                }    

                result_ = ht.ID + "✿"                   //0
                + orderno_ + "✿"                        //1
                + ht.OrderDate.ToString() + "✿"         //2
                + ht.CreateDate.ToString() + "✿"        //3    
                + ht.UserCreate + "✿"                   //4
                + ht.EXT_QRCode;                        //5
            }
            catch
            {

            }

            return result_;
        }

        public static List<TransactionHistoryModel> ToHistoryModel(TransactionHistoryShortModel ht)
        {
            try
            {
                List<TransactionHistoryModel> lst = new List<TransactionHistoryModel>();

                if (ht.DATA.Contains("❖"))
                {
                    string[] historys_ = (ht.DATA).Split('❖');

                    for (int i = 0; i < historys_.Length; i++)
                    {
                        if (historys_[i].Contains("✿"))
                        {
                            string orderno_ = "";

                            string[] items = (historys_[i]).Split('✿');

                            if (ht.TransactionType == "K")
                                orderno_ = items[1];
                            else
                                orderno_ = ht.OrderNo;

                            QRModel qr = QRRead(items[5]);

                            TransactionHistoryModel history = new TransactionHistoryModel
                            {
                                ID = long.Parse(items[0]),
                                TransactionType = ht.TransactionType,
                                OrderNo = orderno_,
                                OrderDate = toMyDateTime(items[2]),
                                ItemCode = qr.Code,
                                ItemName = qr.Name,
                                ItemType = qr.Type,
                                Quantity = qr.Quantity is null ? 0 : Convert.ToDecimal(qr.Quantity),
                                Unit = qr.Unit,
                                EXT_OtherCode = qr.OtherCode,
                                EXT_Serial = qr.Serial,
                                EXT_PartNo = qr.PartNo,
                                EXT_LotNo = qr.LotNo,
                                EXT_MfDate = qr.MfDate,
                                EXT_RecDate = qr.RecDate,
                                EXT_ExpDate = qr.ExpDate,
                                EXT_QRCode = items[5],
                                CustomerCode = qr.CustomerCode,
                                ExportStatus = ht.ExportStatus,
                                RecordStatus = ht.RecordStatus,
                                WarehouseCode_From = ht.WarehouseCode_From,
                                WarehouseName_From = ht.WarehouseName_From,
                                WarehouseCode_To = ht.WarehouseCode_To,
                                WarehouseName_To = ht.WarehouseName_To,
                                CreateDate = toMyDateTime(items[3]),
                                UserCreate = items[4],
                                page = 0,
                                token = MySettings.Token
                            };

                            lst.Add(history);
                        }
                    }
                }
                else if (ht.DATA.Contains("✿"))
                {
                    string orderno_ = "";

                    string[] items = ht.DATA.Split('✿');

                    if (ht.TransactionType == "K")
                        orderno_ = items[1];
                    else
                        orderno_ = ht.OrderNo;

                    QRModel qr = QRRead(items[5]);

                    TransactionHistoryModel history = new TransactionHistoryModel
                    {
                        ID = long.Parse(items[0]),
                        TransactionType = ht.TransactionType,
                        OrderNo = orderno_,
                        OrderDate = toMyDateTime(items[2]),
                        ItemCode = qr.Code,
                        ItemName = qr.Name,
                        ItemType = qr.Type,
                        Quantity = qr.Quantity is null ? 0 : Convert.ToDecimal(qr.Quantity),
                        Unit = qr.Unit,
                        EXT_OtherCode = qr.OtherCode,
                        EXT_Serial = qr.Serial,
                        EXT_PartNo = qr.PartNo,
                        EXT_LotNo = qr.LotNo,
                        EXT_MfDate = qr.MfDate,
                        EXT_RecDate = qr.RecDate,
                        EXT_ExpDate = qr.ExpDate,
                        EXT_QRCode = items[5],
                        CustomerCode = qr.CustomerCode,
                        ExportStatus = ht.ExportStatus,
                        RecordStatus = ht.RecordStatus,
                        WarehouseCode_From = ht.WarehouseCode_From,
                        WarehouseName_From = ht.WarehouseName_From,
                        WarehouseCode_To = ht.WarehouseCode_To,
                        WarehouseName_To = ht.WarehouseName_To,
                        CreateDate = toMyDateTime(items[3]),
                        UserCreate = items[4],
                        page = 0,
                        token = MySettings.Token
                    };

                    lst.Add(history);
                }
                else
                    return null;

                if (lst.Count > 0)
                    return lst;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
            
        private static DateTime? toMyDateTime(string str)
        {
            try
            {
                return Convert.ToDateTime(str);
            }
            catch
            {
                return null;
            }
        }

        public static QRModel QRRead(string str)
        {
            try
            {
                QRModel qr = new QRModel();
                string[] temp_ = DecodeFromUtf8(str).Split(';');
                DateTime? mfdate_;
                DateTime? Recdate_;
                DateTime? Expdate_;
                 
                if (temp_[7].Contains("-") || temp_[7].Contains("/") || temp_[7].Contains("\\") || temp_[7].Contains("."))
                {
                    try
                    {
                        temp_[7] = temp_[7].Replace("-", "/").Replace("\\", "/").Replace(".", "/");
                        string[] ngaythang_ = temp_[7].Split('/');

                        if (ngaythang_.Length >= 3)
                        {
                            try
                            {
                                mfdate_ = new DateTime(Convert.ToInt32(ngaythang_[2].Trim()),
                                    Convert.ToInt32(ngaythang_[1].Trim()),
                                    Convert.ToInt32(ngaythang_[0].Trim()));
                            }
                            catch
                            {
                                mfdate_ = null;
                            }
                        }
                        else
                        {
                            mfdate_ = null;
                        }
                    }
                    catch
                    {
                        mfdate_ = null;
                    }
                }
                else
                {
                    if (temp_[7].Length == 8)
                    {
                        try
                        {
                            mfdate_ = new DateTime(Convert.ToInt32(temp_[7].Substring(4, 4)),
                                Convert.ToInt32(temp_[7].Substring(2, 2)),
                                Convert.ToInt32(temp_[7].Substring(0, 2)));
                        }
                        catch
                        {
                            mfdate_ = null;
                        }
                    }
                    else
                    {
                        mfdate_ = null;
                    }
                }

                if (temp_[8].Contains("-") || temp_[8].Contains("/") || temp_[8].Contains("\\") || temp_[8].Contains("."))
                {
                    try
                    {
                        temp_[8] = temp_[8].Replace("-", "/").Replace("\\", "/").Replace(".", "/");
                        string[] ngaythang_ = temp_[8].Split('/');

                        if (ngaythang_.Length >= 3)
                        {
                            try
                            {
                                Recdate_ = new DateTime(Convert.ToInt32(ngaythang_[2].Trim()),
                                    Convert.ToInt32(ngaythang_[1].Trim()),
                                    Convert.ToInt32(ngaythang_[0].Trim()));
                            }
                            catch
                            {
                                Recdate_ = null;
                            }
                        }
                        else
                        {
                            Recdate_ = null;
                        }
                    }
                    catch
                    {
                        Recdate_ = null;
                    }
                }
                else
                {
                    if (temp_[8].Length == 8)
                    {
                        try
                        {
                            Recdate_ = new DateTime(Convert.ToInt32(temp_[8].Substring(4, 4)),
                                Convert.ToInt32(temp_[8].Substring(2, 2)),
                                Convert.ToInt32(temp_[8].Substring(0, 2)));
                        }
                        catch
                        {
                            Recdate_ = null;
                        }
                    }
                    else
                    {
                        Recdate_ = null;
                    }
                }


                if (temp_[9].Contains("-") || temp_[9].Contains("/") || temp_[9].Contains("\\") || temp_[9].Contains("."))
                {
                    try
                    {
                        temp_[9] = temp_[9].Replace("-", "/").Replace("\\", "/").Replace(".", "/");
                        string[] ngaythang_ = temp_[9].Split('/');

                        if (ngaythang_.Length >= 3)
                        {
                            try
                            {
                                Expdate_ = new DateTime(Convert.ToInt32(ngaythang_[2].Trim()),
                                    Convert.ToInt32(ngaythang_[1].Trim()),
                                    Convert.ToInt32(ngaythang_[0].Trim()));
                            }
                            catch
                            {
                                Expdate_ = null;
                            }
                        }
                        else
                        {
                            Expdate_ = null;
                        }
                    }
                    catch
                    {
                        Expdate_ = null;
                    }
                }
                else
                {
                    if (temp_[9].Length == 8)
                    {
                        try
                        {
                            Expdate_ = new DateTime(Convert.ToInt32(temp_[9].Substring(4, 4)),
                                Convert.ToInt32(temp_[9].Substring(2, 2)),
                                Convert.ToInt32(temp_[9].Substring(0, 2)));
                        }
                        catch
                        {
                            Expdate_ = null;
                        }
                    }
                    else
                    {
                        Expdate_ = null;
                    }
                }

                qr.Type = temp_[0];
                qr.Code = temp_[1];
                qr.Name = temp_[2];
                qr.CustomerCode = temp_[3];
                qr.OtherCode = temp_[3];
                qr.Serial = temp_[4];
                qr.PartNo = temp_[5];
                qr.LotNo = temp_[6];
                qr.MfDate = mfdate_;
                qr.RecDate = Recdate_;
                qr.ExpDate = Expdate_;
                qr.Quantity = ConvertToDecimal(temp_[10]);
                qr.Unit = temp_[11];

                if (qr.Type.Length > 2)
                {
                    return null;
                }
                else if (qr.Name.Length > 200)
                {
                    return null;
                }

                return qr;
            }
            catch
            {
                return null;
            }
        }


        public static Decimal ConvertToDecimal(object str)
        {
            if (str == null)
                return 0;

            string str_2_ = str.ToString();

            if (str_2_ == "")
                return 0;

            Decimal temp_ = 0;
            try
            {
                temp_ = Convert.ToDecimal(new DataTable().Compute(str.ToString(), null));
                //Convert.ToDecimal(str);
            }
            catch
            {
                try
                {
                    try { temp_ = Convert.ToDecimal(str); }
                    catch
                    {
                        str_2_ = str_2_.Replace(",", "*");
                        str_2_ = str_2_.Replace(".", ",");
                        str_2_ = str_2_.Replace("*", ".");
                        temp_ = Convert.ToDecimal(str_2_);
                    }
                }
                catch
                {
                    return 0;
                }
            }
            return temp_;
        }

        public static void InsertLogs(int id, DateTime date, string funtion, string exception, string my_view, string username)
        {
            try
            {
                var result = APIHelper.PostObjectToAPIAsync<BaseModel<object>>
                                            (Constaint.ServiceAddress, Constaint.APIurl.inserts,
                                            new
                                            {
                                                id = id,
                                                date = date,
                                                funtion = funtion,
                                                exception = exception,
                                                my_view = my_view,
                                                username = username
                                            });
            }
            catch (Exception ex)
            {
            }
        }
    }
}

