using Microsoft.JSInterop;
using QRMSWeb.Models;
using QRMSWeb.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QRMSWeb.Utils
{
    public class CommonUtils
    {
        public static bool isDevlopment = false;
        private static readonly Regex _regexNumber = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        
        //count times show user profile
        public static int ShowCountUserProfile = 0;

        private static bool IsNumberAllowed(string text)
        {
            return !_regexNumber.IsMatch(text);
        }

        public static string ConvertStringNumberBiggerMaxValue(string strNumber, int maxLengthInterger=18, int maxLengthDecimal=0)
        {
            if (String.IsNullOrEmpty(strNumber?.Trim()) || maxLengthInterger < 1)
            {
                return string.Empty;
            }
            strNumber = strNumber.Trim().Replace(" ", "");
            if (maxLengthDecimal < 0) maxLengthDecimal = 0;
            string[] splitNumber = strNumber.Split('.');
            string intergerPart = string.IsNullOrEmpty(splitNumber[0]?.Trim()) ? "0" : splitNumber[0]?.Trim();

            if (splitNumber[0].Length > maxLengthInterger)
            {
                intergerPart = splitNumber[0].Substring(0, maxLengthInterger);
            }
            
            if (splitNumber.Length > 1 && maxLengthDecimal > 0)
            {
                string decimalPart = splitNumber[1];
                if (maxLengthDecimal < splitNumber[1].Length)
                {
                    decimalPart = splitNumber[1].Substring(0, maxLengthDecimal);
                }
                return intergerPart + "." + decimalPart;
            } else
            {
                return intergerPart;
            }
        }
        public static string GetNumberFormatValue(string? data, bool hasNegative = false)
        {
            if (string.IsNullOrEmpty(data?.Trim())) return "";
            data = data.Trim();
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                var _number = decimal.Parse(data.Trim(), formatNumberToVN);
                if (!hasNegative && _number < 0)
                {
                    _number = Math.Abs(_number);
                }
                //return _number == 0 ? "0" : _number.ToString("#.##0");
                return _number == 0 ? "0" : _number.ToString("#,##0", formatNumberToVN).Replace(",", ".");
                // return _number.ToString("#.##0");
            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }
        public static string GetNumberFormatValue(decimal? data, bool hasNegative = false)
        {
            if (data == null) return "";
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                /*string value = data.ToString();
                
                var _number = decimal.Parse(value.Trim(), formatNumberToVN);

                //return _number == 0 ? "0" : _number.ToString("#.##0,###");
                */
                if (!hasNegative && data < 0)
                {
                    data = Math.Abs(data ?? 0);
                }
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                return data == 0 ? "0" : data?.ToString("#,##0", formatNumberToVN).Replace(",", ".");

            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }
        public static string GetNumberFormatValue(object? data, bool hasNegative = false)
        {
            if (data == null) return "";
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }

                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                //
                try
                {
                    var _number = (decimal)data;
                    if (!hasNegative && _number < 0)
                    {
                        _number = Math.Abs(_number);
                    }
                    //return _number == 0 ? "0" : _number.ToString("#.##0,###");
                    return _number == 0 ? "0" : _number.ToString("#,##0", formatNumberToVN).Replace(",", ".");
                }
                catch (Exception e)
                {
                    string value = data.ToString();
                    var _number = decimal.Parse(value, formatNumberToVN);
                    if (!hasNegative && _number < 0)
                    {
                        _number = Math.Abs(_number);
                    }
                    //return _number == 0 ? "0" : _number.ToString("#.##0,###");
                    return _number == 0 ? "0" : _number.ToString("#,##0", formatNumberToVN).Replace(",", ".");
                }

            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }

        public static string GetNumberFullFormatValue(string data, bool hasNegative = false)
        {
            if (string.IsNullOrEmpty(data?.Trim())) return "";
            data = data.Trim().Replace(".", "").Replace(",", ".");
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                var _number = decimal.Parse(data.Trim(), formatNumberToVN);
                if (!hasNegative && _number < 0)
                {
                    _number = Math.Abs(_number);
                }
                // return _number == 0 ? "0" : _number.ToString("#,##0.###");
                return _number == 0 ? "0" : _number.ToString("#,##0.###", formatNumberToVN).Replace(".", "_").Replace(",", ".").Replace("_", ",");

            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }
        public static string GetNumberFullFormatValue(decimal? data, bool hasNegative = false)
        {
            if (data == null) return "";
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                /*
                string value = data. .Value();
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                var _number = decimal.Parse(value.Trim(),formatNumberToVN);
                //return _number == 0 ? "0" : _number.ToString("#,##0.###");
                */
                if (!hasNegative && data < 0)
                {
                    data = Math.Abs(data ?? 0);
                }
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                return data == 0 ? "0" : data?.ToString("#,##0.###", formatNumberToVN).Replace(".", "_").Replace(",", ".").Replace("_", ",");
            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }
        public static string GetNumberFullFormatValue(object? data, bool hasNegative = false)
        {
            if (data == null) return "";
            try
            {
                if (0.Equals(data))
                {
                    return "0";
                }
                decimal _number = (decimal)data;
                if (!hasNegative && _number < 0)
                {
                    _number = Math.Abs(_number);
                }
                //var _number = decimal.Parse(value.Trim());
                CultureInfo formatNumberToVN = new CultureInfo("en-US");
                return _number == 0 ? "0" : _number.ToString("#,##0.###", formatNumberToVN).Replace(".", "_").Replace(",", ".").Replace("_", ",");

            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }

        public static decimal? ConvertStringNumberVNToDecimal(string? strVNNumber, int integerLen=15, int decimalLen=3)
        {
            if (string.IsNullOrEmpty(strVNNumber?.Trim())) return null;
            try
            {
                strVNNumber = strVNNumber.Trim().Replace(".", "").Replace(",", ".");
                if (0.Equals(strVNNumber))
                {
                    return 0;
                }
                strVNNumber = ConvertStringNumberBiggerMaxValue(strVNNumber, integerLen, decimalLen);
                CultureInfo formatNumberToEN = new CultureInfo("en-US");
                return decimal.Parse(strVNNumber, formatNumberToEN);

            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return null;
        }
        public static int? ConvertStringNumberVNToInt(string? strVNNumber)
        {
            if (string.IsNullOrEmpty(strVNNumber?.Trim())) return null;
            try
            {
                strVNNumber = strVNNumber.Trim().Replace(".", "").Replace(",", ".");
                strVNNumber = ConvertStringNumberBiggerMaxValue(strVNNumber, 11, 0);
                var numParts = strVNNumber.Split(".");
                strVNNumber = numParts[0];
                if (0.Equals(strVNNumber))
                {
                    return 0;
                }

                CultureInfo formatNumberToEN = new CultureInfo("en-US");
                return int.Parse(strVNNumber, formatNumberToEN);

            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return null;
        }
        public static short? ConvertStringNumberVNToShort(string? strVNNumber)
        {
            if (string.IsNullOrEmpty(strVNNumber?.Trim())) return null;
            try
            {
                
                strVNNumber = strVNNumber.Trim().Replace(".", "").Replace(",", ".");
                strVNNumber = ConvertStringNumberBiggerMaxValue(strVNNumber, 5, 0);
                var numParts = strVNNumber.Split(".");
                strVNNumber = numParts[0];
                if (0.Equals(strVNNumber))
                {
                    return 0;
                }

                CultureInfo formatNumberToEN = new CultureInfo("en-US");
                return short.Parse(strVNNumber, formatNumberToEN);

            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return null;
        }

        public static bool checkLengthPartFloatNumber(string _number)
        {
            string maxInt = "999.999.999.999.999";
            string[] arrNumbers = _number.Split(',');
            var partInt = arrNumbers.Length > 0 ? arrNumbers[0] : "0";
            if (partInt.Length > maxInt.Length)
            {
                return false;
            }
            return true;
        }
        public static string GetYearVNFormated(object? dateTime)
        {
            if (dateTime == null) return "";
            try
            {
                var objDate = (DateTime) dateTime;
                return objDate.ToString("yyyy");
            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }

        public static string GetYearVNFormated(DateTime? dateTime)
        {
            if (dateTime == null) return "";
            return dateTime?.ToString("yyyy");
        }

        public static string GetYearVNFormated(string strDateTime, string orgFormat = "yyyy-MM-dd")
        {
            if (string.IsNullOrEmpty(strDateTime?.Trim())) return "";
            try
            {
                var dateTime = DateTime.ParseExact(strDateTime, orgFormat, CultureInfo.InvariantCulture);
                return GetYearVNFormated(dateTime);
            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }

        public static string GetDateVNFormated(object? dateTime)
        {
            if (dateTime == null) return "";
            try
            {
                var objDate = (DateTime)dateTime;
                return objDate.ToString("dd'/'MM'/'yyyy");
            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }

        public static string GetDateVNFormated(DateTime? dateTime)
        {
            if (dateTime == null) return "";
            return dateTime?.ToString("dd'/'MM'/'yyyy");
        }

        public static string GetDateVNFormated(string strDateTime, string orgFormat = "yyyy-MM-dd")
        {
            if (string.IsNullOrEmpty(strDateTime?.Trim())) return "";
            try
            {
                var dateTime = DateTime.ParseExact(strDateTime, orgFormat, CultureInfo.InvariantCulture);
                return GetDateVNFormated(dateTime);
            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }
        public static DateTime? ConvertDateVNFormatedToDate(string strDate)
        {
            if (string.IsNullOrEmpty(strDate?.Trim())) return null;
            
			try
			{
				strDate = strDate?.Trim().Replace("-", $"/");
				var cultureInfo = new CultureInfo("fr-FR");
				DateTime dateTime11 = DateTime.ParseExact(strDate, "dd/MM/yyyy", cultureInfo);
				return dateTime11;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				if (isDevlopment)
				{
					throw ex;
				}
			}
            return null;
        }
        public static string GetFirstDayOfThisMonth()
        {
            var firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return GetDateVNFormated(firstDay);
        }
        #region vn date time
        public static DateTime? ConvertDateTimeVNFormatedToDate(string strDate)
        {
            if (string.IsNullOrEmpty(strDate?.Trim())) return null;
            
			try
			{
				strDate = strDate.Trim().Replace("-", $"/");
				var cultureInfo = new CultureInfo("fr-FR");
				DateTime dateTime11 = DateTime.ParseExact(strDate, "HH:mm dd/MM/yyyy", cultureInfo);
				return dateTime11;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				if (isDevlopment)
				{
					throw ex;
				}
			}
            return null;
        }
        public static string GetDateTimeVNFormated(object? dateTime)
        {
            if (dateTime == null) return "";
            try
            {
                var objDate = (DateTime)dateTime;
                return objDate.ToString("HH:mm dd'/'MM'/'yyyy");
            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }

        public static string GetDateTimeVNFormated(DateTime? dateTime)
        {
            try
            {
                if (dateTime == null) return "";
                return dateTime?.ToString("HH:mm dd'/'MM'/'yyyy");
            }catch(Exception)
            {

            }
            return "";
        }
        /*
        public static string GetDateVNFormated(string strDateTime, string orgFormat = "yyyy-MM-dd")
        {
            if (string.IsNullOrEmpty(strDateTime?.Trim())) return "";
            try
            {
                var dateTime = DateTime.ParseExact(strDateTime, orgFormat, CultureInfo.InvariantCulture);
                return GetDateVNFormated(dateTime);
            }
            catch (Exception ex)
            {
                if (isDevlopment)
                {
                    throw ex;
                }
            }
            return "";
        }
        */
        public static DateTime? ConvertDateTimeVNFormatedToDateTime(string strDate)
        {
            if (string.IsNullOrEmpty(strDate?.Trim())) return null;
            
			try
			{
				strDate = strDate.Trim().Replace("-", $"/");
				var cultureInfo = new CultureInfo("fr-FR");
				DateTime dateTime11 = DateTime.ParseExact(strDate, "HH:mm dd/MM/yyyy", cultureInfo);
				return dateTime11;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				if (isDevlopment)
				{
					throw ex;
				}
			}
            return null;
        }
        #endregion


        public static string GetNameOfDonBaoHiemStatus(string contractStatus)
        {
            var _contractStatus = ConvertStringNumberVNToInt(contractStatus);
            return GetNameOfDonBaoHiemStatus(_contractStatus);
        }
        public static string GetNameOfDonBaoHiemStatus(int? contractStatus)
        {
            if (contractStatus == null) return "";
            switch (contractStatus)
            {
                case 1:
                    return "Chưa hoàn thành";
                case 2:
                    return "Chờ phê duyệt";
                case 3:
                    return "Từ chối phê duyệt";
                case 4:
                    return "Đã phê duyệt";
                case 5:
                    return "Đã thanh toán";
                case 6:
                    return "Hoàn thành";
                case 7:
                    return "Sửa đổi bổ sung";
                case 8:
                    return "Hủy hợp đồng";
                case 9:
                    return "Tái tục";
                case 10:
                    return "Yêu cầu sửa đổi bổ sung";
                case 11:
                    return "Yêu cầu hủy";
            }
            return "";
        }
        public static string GetNameOfDonBaoHiemIssueType(string issueType)
        {
            if (string.IsNullOrEmpty(issueType?.Trim())) return "";
            switch (issueType.Trim())
            {
                case "A":
                    return "Phê duyệt";
                case "N":
                    return "Cấp mới";
                case "U":
                    return "Sửa đổi bổ sung";
                case "D":
                    return "Hủy đơn";
                case "R":
                    return "Tái tục";
            }
            return "";
        }

        public static string GetNameOfSex(string sex)
        {
            if (string.IsNullOrEmpty(sex)) return "";
            if ("1".Equals(sex))
            {
                return "Nam";
            }
            else if ("2".Equals(sex))
            {
                return "Nữ";
            }
            return "";
        }
        public static string GetNameOfLoaiBaoHiem(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return "";
            if ("5201".Equals(insurProductCode))
            {
                return "Xe máy";
            }
            else if ("5101".Equals(insurProductCode))
            {
                return "Ôto";
            }
            return "";
        }
        public static bool isBaoHiemCar(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode)) return false;
            if ("5101".Equals(insurProductCode))
            {
                return true;
            }
            return false;
        }
        public static bool isBaoHiemMoto(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("5201".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }
        public static bool isBaoHiemVatChatOto(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("5106".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }
        public static bool isBaoHiemDLQuocTe(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("6504".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }
        public static bool isBaoHiemDLTrongNuoc(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("6501".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }
        public static bool isBaoHiemDLNguoiVietNamDuLichNuocNgoai(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("6503".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }
        public static bool isBaoHiemDLNguoiNuocNgoaiDuLichVietNam(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("6502".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }
        public static bool isBaoHiemNhaO(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("3104".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }

        public static bool isBaoHiemBenhHiemNgheo(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("6106".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }

        public static bool isBaoHiemBenhUngThu(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("6105".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }
        public static bool isBaoHiemBenhNhaTuNhan(string insurProductCode)
        {
            if (string.IsNullOrEmpty(insurProductCode?.Trim())) return false;
            if ("3104".Equals(insurProductCode?.Trim()))
            {
                return true;
            }
            return false;
        }

        public static bool IsCar(string vehicleCode)
        {
            if (Constants.Constants.COMMON_TYPE_CODE_CAR.Equals(vehicleCode))
            {
                return true;
            }
            return false;
        }
        public static bool IsCarMaterial(string vehicleCode)
        {
            if (Constants.Constants.COMON_TYPE_CAR_MATERIAL.Equals(vehicleCode))
            {
                return true;
            }
            return false;
        }
        public static bool IsXeMay(string vehicleCode)
        {
            if (Constants.Constants.COMMON_TYPE_CODE_MOTO.Equals(vehicleCode))
            {
                return true;
            }
            return false;
        }
        public static bool IsXeHangHoa(string vehicleCode)
        {
            if (Constants.Constants.COMMON_TYPE_CODE_HH.Equals(vehicleCode))
            {
                return true;
            }
            return false;
        }

        public static bool IsDaiLyCaNhan(string agentTypeCode)
        {
            if ("102".Equals(agentTypeCode))
            {
                return true;
            }
            return false;
        }
        public static bool IsDaiLyToChuc(string agentTypeCode)
        {
            if ("101".Equals(agentTypeCode))
            {
                return true;
            }
            return false;
        }
        public static bool IsDaiLyDonVi(string agentTypeCode)
        {
            if ("202".Equals(agentTypeCode))
            {
                return true;
            }
            return false;
        }
        public static bool IsDaiLyKhac(string agentTypeCode)
        {
            if ("103".Equals(agentTypeCode))
            {
                return true;
            }
            return false;
        }
        public static string GetTenPhanLoaiXe(string phanLoaiCode)
        {
            if (string.IsNullOrEmpty(phanLoaiCode?.Trim())) return "";
            phanLoaiCode = phanLoaiCode.Trim();
            if ("VEH_CN_M".Equals(phanLoaiCode))
            {
                return "Xe máy";
            }
            else if ("VEH_CN_C".Equals(phanLoaiCode))
            {
                return "Ô tô";
            }
            else if ("VEH_HH".Equals(phanLoaiCode))
            {
                return "Hàng hóa";
            }
            else if ("VEH_HH_CX".Equals(phanLoaiCode))
            {
                return "Hàng hóa và chủ xe là một";
            }
            else
            {
                return "";
            }
        }
        public static string GetNameOfLoaiKhachHang(string custType)
        {
            if (string.IsNullOrEmpty(custType?.Trim())) return "a";
            if ("1".Equals(custType?.Trim()))
            {
                return "Cá nhân";
            }
            else if ("2".Equals(custType?.Trim()))
            {
                return "Tổ chức";
            }
            return "";
        }

        public static bool IsKHCaNhan(string custType)
        {
            if (String.IsNullOrEmpty(custType?.Trim())) return false;
            return "1".Equals(custType.Trim());
        }
        public static bool IsKHToChuc(string custType)
        {
            if (String.IsNullOrEmpty(custType?.Trim())) return false;
            return "2".Equals(custType.Trim());
        }

        public static string GetStyleSheetMarkChanged(object? oldOjbect, object? curObject, object? oldValue, object? curValue)
        {
            try
            {
                if (oldOjbect == null || curObject == null) return "";
                return ((oldValue ?? "").ToString().Equals((curValue ?? "").ToString())) ? "" : "color: blue;";

            }
            catch (Exception e)
            {

            }
            return "";
        }


        public static int GetNumDays(DateTime? fromDate, DateTime? toDate)
        {
            try { 
                if (fromDate == null || toDate == null) return 0;
                var to_date = (DateTime)toDate;
                var from_date = (DateTime)fromDate;
                var day = (to_date.Date - from_date.Date).Days;

                if (to_date.Hour > from_date.Hour || (to_date.Hour == from_date.Hour && to_date.Minute > from_date.Minute)) day++;

                
                //TimeSpan Time = (DateTime)toDate - (DateTime)fromDate;
                // decimal travel_day =  (decimal)Time.Days + (Time.Hours > 0 ? 24 / Time.Hours : 0);
                //    return travel_day;
                

                return day;
            }
            catch (Exception e)
            {
                if (isDevlopment)
                {
                    throw e;
                }
            }
            return 0;
        }
        public static int GetNumMonths(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                if (fromDate == null || toDate == null) return 0;
                var to_date = (DateTime)toDate.Value;
                var from_date = (DateTime)fromDate.Value;
                return ((to_date.Year - from_date.Year) * 12) + (to_date.Month - from_date.Month);
            }
            catch (Exception e)
            {
                if (isDevlopment)
                {
                    throw e;
                }
            }
            return 0;
        }
        public static int GetAge(DateTime? birthDay, DateTime? toDay)
        {
            try { 
                if (birthDay == null) return 0;
                var birthDate = (DateTime)birthDay;
                var now = toDay ?? DateTime.Now;
                int age = now.Year - birthDate.Year;

                if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                    age--;

                return age;
            }
            catch (Exception e)
            {
                if (isDevlopment)
                {
                    throw e;
                }
            }
            return 0;
        }
        public static string FirstCharToUpper( string input)
        {
            if (string.IsNullOrEmpty(input?.Trim()))
            {
                return "";
            }
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static async Task<bool> CheckNgayBatDauHieuLucSearch(IJSRuntime _jsRuntime, DateTime? fromDate, DateTime? toDate, int maxMonth = 12)
        {
            if (fromDate == null || !fromDate.HasValue)
            {
                await _jsRuntime.InvokeVoidAsync("showMessage", "Thông báo", "Từ Ngày hiệu lực tìm kiếm không được để trống", "error");
                return false;
            }
            else
            {

                DateTime endDate = (DateTime)((toDate == null || !toDate.HasValue) ? DateTime.Now : toDate);
                if (CommonUtils.GetNumMonths(fromDate, endDate) > maxMonth 
                    || (CommonUtils.GetNumMonths(fromDate, endDate) == maxMonth && fromDate.Value.Day <= endDate.Day))
                {
                    await _jsRuntime.InvokeVoidAsync("showMessage", "Thông báo", $"Số tháng tìm kiếm không được lớn hơn {maxMonth} tháng", "error");
                    return false;
                }
            }
            return true;
        }
        public static bool IsHtmlEmpty(string htmlContent)
        {
            var htmlContentTmp = (htmlContent??"").Replace("<p>", "").Replace("</p>", "").Replace("&nbsp;", "").Trim();
            return string.IsNullOrEmpty(htmlContentTmp);
        }
        public static bool IsNotOverToDay(DateTime? dateTime, bool hasTime = false)
        {
            if (dateTime == null) return true;
            if (hasTime)
            {
                return DateTime.Now <= dateTime;
            }
            else
            {
                return DateTime.Now.Date <= dateTime.Value.Date;
            }
        
        }

        public static bool IsContractFromCusApp(string appType)
        {
            return "C".Equals(appType);
        }
    }
}