﻿  
using Honeywell.AIDC.CrossPlatform; 
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants; 
using QRMS.Models;
using QRMS.Models.NK;
using QRMS.Views;
using QRMS.Views.CK;
using System;
using System.Collections.Generic; 
using System.Threading; 
using Xamarin.Forms; 

namespace QRMS.ViewModels
{
    public class CKDC_CPPageModel : BaseViewModel
    {
        public CKDC_CPPage _NK_CPPage;

        public bool IsTat { get; set; } = false;
        public bool IsQuet { get; set; } = false;

        public bool IsThongBao { get; set; } = true;
        public string ThongBao { get; set; } = "";
        public string ThoiGian { get; set; } = "";
        public Color Color { get; set; } = Color.Red;
        public DateTime? _Date { get; set; }
        public string _WarehouseCode { get; set; }
        public string _WarehouseName { get; set; }
        public string _No { get; set; }


        public string _WarehouseCode_To { get; set; }
        public string _WarehouseName_To { get; set; }


        public CKDC_CPPageModel()
        {
        }

        public override void OnAppearing()
        {
            Color = Color.Red;
            IsThongBao = true;
            ThongBao = "Bạn hãy scan phiếu chuyển kho";
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
                var result = APIHelper.PostObjectToAPIAsync<BaseModel<List<PhieuChuyenKhoModel>>>
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
                        _No = result.Result.data[0].TransferOrderNo;
                        str_ = "1";
                        _Date = result.Result.data[0].InstructionDate;//2
                        str_ = "2";
                        _WarehouseCode = result.Result.data[0].WarehouseCode_From;//3
                        str_ = "3";
                        _WarehouseName = result.Result.data[0].WarehouseName_From;//4
                        str_ = "4";
                        _WarehouseCode_To = result.Result.data[0].WarehouseCode_To;//3
                        str_ = "3";
                        _WarehouseName_To = result.Result.data[0].WarehouseName_To;//4
                        str_ = "4";

                        // 
                        Color = Color.Blue;
                        str_ = "5";
                        IsThongBao = true;
                        str_ = "6";
                        ThongBao = "Thành công";
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
                        ThongBao = "Mã phiếu không tồn tại!";
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
                    ThongBao = "Mã phiếu không tồn tại!";

                    MySettings.InsertLogs(0, DateTime.Now, "LoadModels", ex.Message, "CKDC_CPPageModel", MySettings.UserName);
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
                        ThongBao = "Phiếu không tồn tại!";
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
