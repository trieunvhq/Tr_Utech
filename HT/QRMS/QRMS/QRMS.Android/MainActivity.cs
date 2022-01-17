using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using QRMS.Constants;
using Firebase;
using FFImageLoading.Forms.Droid; 
using Android.Util;
using Android;
using Plugin.Fingerprint;
using Xamarin.Forms;
using QRMS.interfaces;
using QRMS.Droid.Renderer;

namespace QRMS.Droid
{
    [Activity(Label = "QRMS", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme",
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Touchscreen, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Activity FormsContext { get; set; }
        readonly string[] permissions =
        {
            Android.Manifest.Permission.Internet,
            Android.Manifest.Permission.ReadExternalStorage,
            Android.Manifest.Permission.WriteExternalStorage,
            Android.Manifest.Permission.MediaContentControl,
            Android.Manifest.Permission.AccessCoarseLocation,
            Android.Manifest.Permission.AccessFineLocation,
        };
        const int RequestID = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            FormsContext = this;
            CachedImageRenderer.Init(true);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
           // AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            base.OnCreate(savedInstanceState);
            FirebaseApp.InitializeApp(this);
            global::Xamarin.Forms.Forms.SetFlags(new string[] { "RadioButton_Experimental", "CollectionView_Experimental", "IndicatorView_Experimental" });


            DependencyService.Register<IBarcodeReader, BarcodeReaderWrapper_Droid>();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init(true);
            var ignore = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);
            Rg.Plugins.Popup.Popup.Init(this);

            //Get ScreenHeight & ScreenWidth Device
            App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);

            #region Lxt UI
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.P)
            {
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            }

            var height = 0;
            int resourceId = Resources.GetIdentifier("navigation_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                height = Resources.GetDimensionPixelSize(resourceId);
            }
            MySettings.Height_KeySoft = (height / Resources.DisplayMetrics.Density);
            MySettings.w_QRMS = Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density;
            MySettings.h_QRMS = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;
            #endregion

            CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);
            //RequestPermissions(permissions, RequestID);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode == 202)
            {
                Log.Info("Biometric", "Received response for Biometric Permission request");

                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    Log.Info("Biometric", "Biomtric permission has now been granted");

                }
                else
                {
                    Log.Info("Biometric", "Biometric permission is not granted");
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
                    {
                        string[] reruiredPermission = new string[] { Manifest.Permission.UseBiometric };
                    }
                    else
                    {
                        string[] reruiredPermission = new string[] { Manifest.Permission.UseFingerprint };
                    }
                }
            }
        }
    }
}