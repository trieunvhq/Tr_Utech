 
using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Models.QMK;
using QRMS.Views.AccountPage;
using Xamarin.Forms;

namespace QRMS.ViewModels.Account
{
    public class BaoMatPageModel : BaseViewModel
    {
        public bool IsSuDungMK { get; set; } = true;
        public bool IsVanTay{ get; set; } 
        public bool IsKhuonMat { get; set; }


        public bool Is2 { get; set; }
        public bool Is3 { get; set; }
        public GridLength row { get; set; }
        public override void OnAppearing()
        {
            base.OnAppearing();
            if(MySettings.IsP_V_K==0)
            {
                Is2 = false;
                Is3 = false;
                row = 0;
            }
            else if (MySettings.IsP_V_K == 1)
            {
                Is2 = true;
                Is3 = false;
                row = 40;
            }
            else if (MySettings.IsP_V_K == 2)
            {
                Is2 = false;
                Is3 = true; 
                row = 0;
            }
            LoadData();
        }
        public void ExecuteBackPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task LoadData()
        {
            try
            {
                await Controls.LoadingUtility.ShowAsync().ContinueWith(ab =>
                { 
                    var submit = APIHelper.PostObjectToAPIAsync<BaseModel<CusCustomerInfoModel>>
                                        (Constaint.ServiceAddress, Constaint.APIurl.getbyaccountid,
                                        new
                                        {
                                            ACCOUNT_ID = FormTypeModel.UserID 
                                        });
                    _ = submit.ContinueWith(next =>
                    {
                        if (submit.Result.ErrorCode != null && submit.Result.ErrorCode != "")
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();

                                if (submit.Result.RespondCode == "0" || submit.Result.ErrorCode == "0")
                                {
                                    if(submit.Result.data.USE_PASSWORD!=null && submit.Result.data.USE_PASSWORD=="Y")
                                    {
                                        IsSuDungMK = true;
                                        MySettings.IsPass = true;
                                    }
                                    else
                                    {
                                        IsSuDungMK = false;
                                        MySettings.IsPass = false;
                                    }
                                    //
                                    if (submit.Result.data.USE_FINGER != null && submit.Result.data.USE_FINGER == "Y")
                                    {
                                        IsVanTay = true;
                                        MySettings.IsVanTay = true;
                                    }
                                    else
                                    {
                                        IsVanTay = false;
                                        MySettings.IsVanTay = false;
                                    }
                                    //
                                    if (submit.Result.data.USE_FACE != null && submit.Result.data.USE_FACE == "Y")
                                    {
                                        IsKhuonMat = true;
                                        MySettings.IsKhuonMat = true;
                                    }
                                    else
                                    {
                                        IsKhuonMat = false;
                                        MySettings.IsKhuonMat = false;
                                    }
                                }
                                else
                                {
                                    Application.Current.MainPage.DisplayAlert("ErrorCode: " + submit.Result.ErrorCode, submit.Result.Message, "OK");
                                }
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();
                            });
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.HideAsync();

                    DependencyService.Get<ILogger>().Log(ex.ToString());
                    // Log ex to db
                    var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                    var FCMTockenValue = String.Empty;
                    if (FCMToken)
                    {
                        FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                    }
                    var token = FCMTockenValue;
                    var appType = Constaint.App_Type.Agent;
                    var osType = Device.OS.ToString();
                    var namespaceInFile = GetType().Namespace;
                    var className = GetType().Name;
                    var methodName = "ExecuteNextPage";
                    var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                    var userId = FormTypeModel.UserID;
                    LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
        }
        public async Task ExecuteNextPage(int tt)
        {
            try
            {
                await Controls.LoadingUtility.ShowAsync().ContinueWith(ab =>
                {
                    string Setting_ = "";
                    string value_ = ""; ;
                    if (tt==1)
                    {
                        if (IsSuDungMK)
                            value_ = "Y";
                        else
                            value_ = "N";
                        Setting_ = "USE_PASSWORD";
                    }    
                    else if (tt==2)
                    {
                        if (IsVanTay)
                            value_ = "Y";
                        else
                            value_ = "N";
                        Setting_ = "USE_FINGER";
                    }
                    else if (tt == 3)
                    {
                        if (IsKhuonMat)
                            value_ = "Y";
                        else
                            value_ = "N";
                        Setting_ = "USE_FACE";
                    }
                    var submit = APIHelper.PostObjectToAPIAsync<BaseModel<object>>
                                        (Constaint.ServiceAddress, Constaint.APIurl.toggleaccountsetting,
                                        new
                                        {
                                            AccountId = FormTypeModel.UserID,
                                            Setting = Setting_,
                                            Value = value_,
                                        //CUST_BYER_TYPE
                                    });
                    _ = submit.ContinueWith(next =>
                    {
                        if (submit.Result.ErrorCode != null && submit.Result.ErrorCode != "")
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();

                                if (submit.Result.RespondCode == "0" || submit.Result.ErrorCode == "0")
                                {
                                    if (tt == 1)
                                    {
                                        if (IsSuDungMK)
                                            MySettings.IsPass = true;
                                        else
                                            MySettings.IsPass = false; 
                                    }
                                    else if (tt == 2)
                                    {
                                        if (IsVanTay)
                                            MySettings.IsVanTay = true;
                                        else
                                            MySettings.IsVanTay = false;
                                    }
                                    else if (tt == 3)
                                    {
                                        if (IsKhuonMat)
                                            MySettings.IsKhuonMat = true;
                                        else
                                            MySettings.IsKhuonMat = false;
                                    }
                                }
                                else
                                {
                                    Application.Current.MainPage.DisplayAlert("ErrorCode: " + submit.Result.ErrorCode, submit.Result.Message, "OK");
                                }
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Controls.LoadingUtility.HideAsync();
                            });
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Controls.LoadingUtility.HideAsync();

                    DependencyService.Get<ILogger>().Log(ex.ToString());
                    // Log ex to db
                    var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                    var FCMTockenValue = String.Empty;
                    if (FCMToken)
                    {
                        FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                    }
                    var token = FCMTockenValue;
                    var appType = Constaint.App_Type.Agent;
                    var osType = Device.OS.ToString();
                    var namespaceInFile = GetType().Namespace;
                    var className = GetType().Name;
                    var methodName = "ExecuteNextPage";
                    var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                    var userId = FormTypeModel.UserID;
                    LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
        }
         
    }
}
