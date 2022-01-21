 
using Acr.UserDialogs;
using Honeywell.AIDC.CrossPlatform;
using Newtonsoft.Json;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.interfaces;
using QRMS.Models; 
using QRMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class CK_LayPhieuPageModel : BaseViewModel
    {
        public CK_LayPhieuPage _CK_LayPhieuPage;

        public bool IsTat { get; set; } = false;
        public bool IsQuet { get; set; } = false;

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public Color Color { get; set; } = Color.Red;
        public DateTime? InstructionDate { get; set; }
        public string WarehouseCode_From { get; set; }
        public string WarehouseName_From { get; set; }
        public string WarehouseCode_To { get; set; }
        public string WarehouseName_To { get; set; }
        public string TransferOrderNo { get; set; }



        public CK_LayPhieuPageModel()
        {
        }

        public override void OnAppearing()
        {
            Color = Color.Red;
            IsThongBao = true;
            ThongBao = "Bạn hãy scan phiếu nhập kho";
            //
            try
            {
                CloseBarcodeReader();
            }
            catch
            {

            }
            OpenBarcodeReader();
            base.OnAppearing();
        }



        public bool isDangQuet = false;
        public void ScanComplate(string BarcodeScan)
        {
            string str_ = "0";
            try
            {
                var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<TransferInstructionBPLModel>>>
                                                 (Constaint.ServiceAddress, Constaint.APIurl.gettransferinstruction,
                                                 new
                                                 {
                                                     BarcodeScan = BarcodeScan
                                                 });
                if (result != null && result.Result != null && result.Result.data != null
                    && result.Result.data.Count > 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TransferOrderNo = result.Result.data[0].TransferOrderNo;//1str_ = "0";
                        str_ = "1";
                        InstructionDate = result.Result.data[0].InstructionDate;//2
                        str_ = "2";
                        WarehouseCode_From = result.Result.data[0].WarehouseCode_From;//3
                        str_ = "3";
                        WarehouseName_From = result.Result.data[0].WarehouseName_From;//4
                        str_ = "4";

                        WarehouseCode_To = result.Result.data[0].WarehouseCode_To;//3
                        str_ = "3";
                        WarehouseName_To = result.Result.data[0].WarehouseName_To;//4
                        str_ = "4";
                        // 
                        Color = Color.Blue;
                        str_ = "5";
                        IsThongBao = true;
                        str_ = "6";
                        ThongBao += "Thành công";
                        str_ = "7";
                        //CloseBarcodeReader();
                        //OpenBarcodeReader();
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Color = Color.Red;
                        IsThongBao = true;
                        ThongBao += "Mã phiếu không tồn tại!. ErrorCode: " + result.Result.ErrorCode
                            + ". Message:" + result.Result.Message;
                    });
                    //CloseBarcodeReader();
                    //OpenBarcodeReader();
                }
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //CloseBarcodeReader();
                    //OpenBarcodeReader();
                    Color = Color.Red;
                    IsThongBao = true;
                    ThongBao += str_ + ".Mã phiếu không tồn tại. ex: " + ex.Message;

                    MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "CK_LayPhieuPageModel", MySettings.UserName);
                });
            }
        }
        //
        #region BarcodeReader Action
        private BarcodeReader mSelectedReader = null;
        private SynchronizationContext mUIContext = SynchronizationContext.Current;

        public async void OpenBarcodeReader()
        {
            mSelectedReader = GetBarcodeReader();
            if (!mSelectedReader.IsReaderOpened)
            {
                BarcodeReader.Result result = await mSelectedReader.OpenAsync();

                if (result.Code == BarcodeReader.Result.Codes.SUCCESS ||
                    result.Code == BarcodeReader.Result.Codes.READER_ALREADY_OPENED)
                {
                    Color = Color.Blue;
                    IsThongBao = true;
                    ThongBao = "1";
                }
                else
                {
                    Color = Color.Red;
                    IsThongBao = true;
                    ThongBao = "2";
                    await Application.Current.MainPage.DisplayAlert("Error", "OpenAsync failed, Code:" + result.Code +
                        " Message:" + result.Message, "OK");
                }
            }
            else
            {
                Color = Color.Red;
                IsThongBao = true;
                ThongBao = "2";
            }
        }

        public BarcodeReader GetBarcodeReader()
        {
            BarcodeReader reader = new BarcodeReader();

            reader.BarcodeDataReady += MBarcodeReader_BarcodeDataReady;

            return reader;
        }

        private void MBarcodeReader_BarcodeDataReady(object sender, Honeywell.AIDC.CrossPlatform.BarcodeDataArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    mUIContext.Post(_ =>
                    {
                        Color = Color.Blue;
                        IsThongBao = true;
                        ThongBao = "Data: " + e.Data;
                        ScanComplate(e.Data);
                    }
                        , null);
                });
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Thông báo", "MBarcodeReader_BarcodeDataReady Error", "OK");
            }
        }

        public async void CloseBarcodeReader()
        {
            isDangQuet = false;
            if (mSelectedReader != null && mSelectedReader.IsReaderOpened)
            {
                BarcodeReader.Result result = await mSelectedReader.CloseAsync();
                if (result.Code != BarcodeReader.Result.Codes.SUCCESS)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "CloseAsync failed, Code:" + result.Code +
                        " Message:" + result.Message, "OK");
                }
            }
        }
        #endregion
    }
}
