using System.Collections.Generic;
using QRMS.Controls; 
using Plugin.Settings;
using Plugin.Settings.Abstractions;
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
        private const string Temp1_QRMS_KHKey = "last_Temp1_QRMS_KHKey";
        public static string Temp1
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(Temp1_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(Temp1_QRMS_KHKey, value);
            }
        }
        private const string Temp2_QRMS_KHKey = "last_Temp2_QRMS_KHKey";
        public static string Temp2
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(Temp2_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(Temp2_QRMS_KHKey, value);
            }
        }
        private const string Temp3_QRMS_KHKey = "last_Temp3_QRMS_KHKey";
        public static string Temp3
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(Temp3_QRMS_KHKey, "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(Temp3_QRMS_KHKey, value);
            }
        }//
       
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
                return (MySettings.w_QRMS *2.0/3.0);
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
                return (MySettings.h_QRMS-300) /2;
            }
        }
        public static double Haft_W_2_3
        {
            get
            {
                return (MySettings.w_QRMS - 32 - 16) *2 / 3;
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
                if(MySettings.w_QRMS<400)
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
    }
}

