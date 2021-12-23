using Acr.UserDialogs;
using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Constants;
using QRMS.Helper;
using QRMS.Models;
using QRMS.Resources;
using QRMS.Views.AccountPage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QRMS.ViewModels.BH_DuLich.DL_NNNTVN
{
    public class CreateAccountPageModel : BaseViewModel
    { 
        public ObservableCollection<SexModel> GioiTinhs { get; set; } = new ObservableCollection<SexModel>();
        public ObservableCollection<ProvinceModel> TinhThanhs { get; set; } = new ObservableCollection<ProvinceModel>();
        public ObservableCollection<DistrictModel> QuanHuyens { get; set; } = new ObservableCollection<DistrictModel>();
        public ObservableCollection<WardModel> XaPhuongs { get; set; } = new ObservableCollection<WardModel>();
        public ObservableCollection<CommonValueModel> QHNguoiMuaBHs { get; set; } = new ObservableCollection<CommonValueModel>(); 
        public SexModel SelectedGioiTinh { get; set; }
        public ProvinceModel SelectedTinhThanh { get; set; }
        public DistrictModel SelectedQuanHuyen { get; set; }
        public WardModel SelectedXaPhuong { get; set; } 
        public DateTime Cust_Birthday { get; set; } = DateTime.Now; 
        private int? DistrictId = null;
        private int? WardId = null;
        private int? CommonValueId = null; 
        public ImageSource imgMatTruoc { get; set; }
        public ImageSource imgMatSau { get; set; }
        public List<ImageIDCardModel> listImage { get; set; } = new List<ImageIDCardModel>();
        public bool Loaded_Form { get; set; }
       

        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; } 
        public string IDENTITY_NO { get; set; } 
        public string CUST_ADDRESS { get; set; }
        public string IMAGE_IDCARD_FRONT { get; set; }
        public string IMAGE_IDCARD_BACK { get; set; }

        public bool IsChupAnh = false;
        public override void OnAppearing()
        {
            base.OnAppearing();

            if (IsChupAnh)
                IsChupAnh = false;
            else
            {
                if (Loaded_Form == false)
                {
                    LoadForm();
                }
                Loaded_Form = true;
            } 
        }

        public ICommand AddInsuredPersonCommand { get; set; }
        public ICommand InsuredPersonCommond { get; set; }

        private async void LoadForm()
        {
            await LoadGender();
            await LoadProvince();
            ClearForm();
        }
         

        public async Task LoadProvince()
        {
            try
            {
                var result = ProvinceAPI.GetListProvince();
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (result != null && result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            var p = TinhThanhs.FirstOrDefault(a => a.ID == item.ID);
                            if (p == null)
                            {
                                TinhThanhs.Add(new ProvinceModel()
                                {
                                    ID = item.ID,
                                    COUNTRY_ID = item.COUNTRY_ID.Value,
                                    CODE = item.CODE,
                                    NAME = item.NAME
                                });
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
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
                    var methodName = "LoadProvince";
                    var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                    var userId = FormTypeModel.UserID;
                    LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
        }

        public async Task LoadDistrict(int provinceId)
        {
            try
            {
                //Task.Run(() =>
                //{
                var result = DistrictAPI.GetListDistrict(provinceId);
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (result != null && result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            var d = QuanHuyens.FirstOrDefault(a => a.ID == item.ID);
                            if (d == null)
                            {
                                QuanHuyens.Add(new DistrictModel()
                                {
                                    ID = item.ID,
                                    PROVINCE_ID = item.PROVINCE_ID,
                                    PROVINCE_CODE = item.PROVINCE_CODE,
                                    CODE = item.CODE,
                                    NAME = item.NAME
                                });
                            }
                        }

                        if (DistrictId != null)
                        {
                            SelectedQuanHuyen = QuanHuyens.Where(a => a.ID == DistrictId).FirstOrDefault();
                        }
                    }
                });
                //});
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
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
                    var methodName = "LoadDistrict";
                    var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                    var userId = FormTypeModel.UserID;
                    LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
        }

        public async Task LoadWard(int districtId)
        {
            try
            {
                //Task.Run(() =>
                //{
                var result = WardAPI.GetListWard(districtId);
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (result != null && result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            var w = XaPhuongs.FirstOrDefault(a => a.ID == item.ID);
                            if (w == null)
                            {
                                XaPhuongs.Add(new WardModel()
                                {
                                    ID = item.ID,
                                    DISTRICT_ID = item.DISTRICT_ID,
                                    DISTRICT_CODE = item.DISTRICT_CODE,
                                    CODE = item.CODE,
                                    NAME = item.NAME
                                });
                            }
                        }

                        if (WardId != null)
                        {
                            SelectedXaPhuong = XaPhuongs.Where(a => a.ID == WardId).FirstOrDefault();
                        }
                    }
                });
                //});
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
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
                    var methodName = "LoadWard";
                    var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                    var userId = FormTypeModel.UserID;
                    LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
#if DEBUG
                    UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK");
#endif
                });
            }
        }
         
        public async Task LoadGender()
        {
            List<SexModel> listSex = new List<SexModel>();
            listSex.Add(new SexModel() { Key = "1", Value = "Nam" });
            listSex.Add(new SexModel() { Key = "2", Value = "Nữ" });
            listSex.Add(new SexModel() { Key = "3", Value = "Khác" });

            Device.BeginInvokeOnMainThread(() =>
            {
                foreach (var item in listSex)
                {
                    var s = GioiTinhs.FirstOrDefault(a => a.Key.Equals(item.Key));
                    if (s == null)
                    { 
                        GioiTinhs.Add(item);
                    }
                }
            });
        }
         
        public void ExecuteNextPage()
        {
            try
            {
                Controls.LoadingUtility.ShowAsync().ContinueWith(async a =>
                { 
                    var submit = APIHelper.PostObjectToAPIAsync<BaseModel<object>>
                                    (Constaint.ServiceAddress, Constaint.APIurl.check_exist_info,
                                    new
                                    { 
                                        EMAIL = EMAIL.ToLower(),
                                        MOBILE = PHONE.ToLower(), 
                                        IDENTITY_NO = IDENTITY_NO 
                                    });
                    _ = submit.ContinueWith(next =>
                    {
                        if (submit.Result.ErrorCode != null && submit.Result.ErrorCode != "")
                        {
                            Controls.LoadingUtility.HideAsync();
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                if (submit.Result.ErrorCode =="0")
                                { 
                                    var page = new CreatePassPage(this);
                                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(page);
                                }
                                else if (submit.Result.ErrorCode == "2")
                                {
                                    Application.Current.MainPage.DisplayAlert(AppResources.ThongBaoDangNhap1, AppResources.ThongBaoTaoTaiKhoan2, "OK");
 
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

        public void ExecuteBackPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void ExecuteShowPage()
        { 
        }

        private void ClearForm()
        {
            try
            { 
                SelectedGioiTinh = null;
                SelectedTinhThanh = null;
                SelectedQuanHuyen = null;
                SelectedXaPhuong = null;

                imgMatTruoc = "imgChupAnh.png";
                imgMatSau = "imgChupAnh.png"; 
                Cust_Birthday = DateTime.Now; 
                NAME = null;
                EMAIL = null;
                PHONE = null;
                IDENTITY_NO = null; 
                CUST_ADDRESS = null;
                IMAGE_IDCARD_FRONT = null;
                IMAGE_IDCARD_BACK = null;
                 
            }
            catch (Exception ex)
            {
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
                var methodName = "ClearForm";
                var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
#if DEBUG
                UserDialogs.Instance.AlertAsync(ex.Message, "Exception", "OK").ConfigureAwait(false);
#endif
            }
        }

        public void CAMT()
        {
            IsChupAnh = true;
            if (Device.RuntimePlatform == Device.Android)
            {
                TakePhoto2(true);
            }
            else
            {
                TakePhoto(true);
            }
        }

        public void CAMS()
        {
            IsChupAnh = true;
            if (Device.RuntimePlatform == Device.Android)
            {
                TakePhoto2(false);
            }
            else
            {
                TakePhoto(false);
            }
        }

        public void PickT()
        {
            IsChupAnh = true;
            if (Device.RuntimePlatform == Device.Android)
            {
                GetPhoto2(true);
            }
            else
            {
                GetPhoto(true);
            }
        }

        public void PickS()
        {
            IsChupAnh = true;
            if (Device.RuntimePlatform == Device.Android)
            {
                GetPhoto2(false);
            }
            else
            {
                GetPhoto(false);
            }
        }

        async void GetPhoto2(bool tt)
        { 
            try
            {
                IsChupAnh = true;
                if (await GetPhotoPermisson())
                {
                    var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                    {
                        Title = "Please pick a photo"
                    });

                    if (result != null)
                    {
                        var stream = await result.OpenReadAsync();

                        if (stream == null)
                            return;


                        if (tt)
                        {
                            imgMatTruoc = ImageSource.FromStream(() => stream);
                            byte[] bytes;
                            using (var memoryStream = new MemoryStream())
                            {
                                stream.CopyTo(memoryStream);
                                bytes = memoryStream.ToArray();
                                IMAGE_IDCARD_FRONT = Convert.ToBase64String(bytes);
                            }
                        }
                        else
                        {
                            imgMatSau = ImageSource.FromStream(() => stream);

                            byte[] bytes;
                            using (var memoryStream = new MemoryStream())
                            {
                                stream.CopyTo(memoryStream);
                                bytes = memoryStream.ToArray();
                                IMAGE_IDCARD_BACK = Convert.ToBase64String(bytes);
                            }
                        }
                    }
                }   
            }
            catch (Exception ex)
            {
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
                var methodName = "ClearForm";
                var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
            }
        }

        async void TakePhoto2(bool tt)
        {
            try
            {
                IsChupAnh = true;
                if (await GetCameraPermisson())
                {
                    var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Please pick a photo"
                    });

                    if (result != null)
                    {
                        var stream = await result.OpenReadAsync();

                        if (stream == null)
                            return;


                        if (tt)
                        {
                            imgMatTruoc = ImageSource.FromStream(() => stream);
                            byte[] bytes;
                            using (var memoryStream = new MemoryStream())
                            {
                                stream.CopyTo(memoryStream);
                                bytes = memoryStream.ToArray();
                                IMAGE_IDCARD_FRONT = Convert.ToBase64String(bytes);
                            }
                        }
                        else
                        {
                            imgMatSau = ImageSource.FromStream(() => stream);

                            byte[] bytes;
                            using (var memoryStream = new MemoryStream())
                            {
                                stream.CopyTo(memoryStream);
                                bytes = memoryStream.ToArray();
                                IMAGE_IDCARD_BACK = Convert.ToBase64String(bytes);
                            }
                        }
                    }
                }    

            }
            catch (Exception ex)
            {
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
                var methodName = "ClearForm";
                var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
            }
        } 
        private async Task TakePhoto(bool tt)
        {
            try
            {
                IsChupAnh = true;
                if (await GetCameraPermisson())
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                    {
                        return;
                    }
                    var _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        MaxWidthHeight = 500,
                        PhotoSize = PhotoSize.MaxWidthHeight,
                        CompressionQuality = 50,
                        CustomPhotoSize = 50,
                        Directory = "Anh",
                        Name = "Image.jpg"
                    });

                    if (_mediaFile == null)
                        return;

                    if (tt)
                    {
                        var a = _mediaFile.Path;
                        imgMatTruoc = ImageSource.FromStream(() =>
                        {
                            return _mediaFile.GetStream();
                        });

                        using (var memoryStream = new MemoryStream())
                        {
                            _mediaFile.GetStream().CopyTo(memoryStream);
                            byte[] byteImageArray = memoryStream.ToArray();
                            IMAGE_IDCARD_FRONT = Convert.ToBase64String(byteImageArray);
                            //try
                            //{
                            //    var imageName = "Image_IDCard_Front_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            //    ImageIDCardModel obj = new ImageIDCardModel();
                            //    obj.ImgType = "1";
                            //    obj.ImgName = imageName;
                            //    obj.ImgData = byteImageArray;
                            //    listImage.Add(obj);
                            //}
                            //catch (Exception ex)
                            //{
                            //    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                            //}
                        }
                    }
                    else
                    {
                        imgMatSau = ImageSource.FromStream(() =>
                        {
                            return _mediaFile.GetStream();
                        });

                        using (var memoryStream = new MemoryStream())
                        {
                            _mediaFile.GetStream().CopyTo(memoryStream);
                            byte[] byteImageArray = memoryStream.ToArray();
                            IMAGE_IDCARD_BACK = Convert.ToBase64String(byteImageArray);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
                var methodName = "ClearForm";
                var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
            }
        }
        private async Task GetPhoto(bool tt)
        {
            try
            {
                IsChupAnh = true;
                if (await GetPhotoPermisson())
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        return;
                    }
                    var _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        MaxWidthHeight = 500,
                        PhotoSize = PhotoSize.MaxWidthHeight,
                        CompressionQuality = 50,
                        CustomPhotoSize = 50
                    });
                    //MediaFile _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    //{
                    //    PhotoSize = PhotoSize.Small
                    //});
                    if (_mediaFile == null)
                        return;

                    if (tt)
                    {
                        var a = _mediaFile.Path;
                        imgMatTruoc = ImageSource.FromStream(() =>
                        {
                            return _mediaFile.GetStream();
                        });

                        using (var memoryStream = new MemoryStream())
                        {
                            _mediaFile.GetStream().CopyTo(memoryStream);
                            byte[] byteImageArray = memoryStream.ToArray();
                            IMAGE_IDCARD_FRONT = Convert.ToBase64String(byteImageArray);
                            //try
                            //{
                            //    var imageName = "Image_IDCard_Front_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            //    ImageIDCardModel obj = new ImageIDCardModel();
                            //    obj.ImgType = "1";
                            //    obj.ImgName = imageName;
                            //    obj.ImgData = byteImageArray;
                            //    listImage.Add(obj);
                            //}
                            //catch (Exception ex)
                            //{
                            //    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                            //}
                        }
                    }
                    else
                    {
                        imgMatSau = ImageSource.FromStream(() =>
                        {
                            return _mediaFile.GetStream();
                        });

                        using (var memoryStream = new MemoryStream())
                        {
                            _mediaFile.GetStream().CopyTo(memoryStream);
                            byte[] byteImageArray = memoryStream.ToArray();
                            IMAGE_IDCARD_BACK = Convert.ToBase64String(byteImageArray);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
                var methodName = "ClearForm";
                var actionName = $"{namespaceInFile}.{className}.{methodName}()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(token, appType, osType, actionName, ex.ToString(), userId);
            }
        }

        private async Task<bool> GetCameraPermisson()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
            }
            return status == PermissionStatus.Granted;
        }

        private async Task<bool> GetPhotoPermisson()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Photos>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Photos>();
            }
            return status == PermissionStatus.Granted;
        }
    }
}
