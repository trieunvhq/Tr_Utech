//using System;
//using Android.App;
//using Android.Runtime;
//using VpnHood.Client.App;
//using VpnHood.Client.Device;

//namespace QRMS.Droid
//{
//    public class AndroidApp : Application, IAppProvider
//    {
//        public static AndroidApp Current
//        {
//            get;
//            private set;
//        }
//        private VpnHoodApp VpnHoodApp
//        {
//            get;
//            set;
//        }
//        public IDevice Device
//        {
//            get;
//        }

//        public string OperatingSystemInfo => throw new NotImplementedException();

//        public AndroidApp(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
//        {
//            Device = new AndroidDevice();
//        }
//        public override void OnCreate()
//        {
//            base.OnCreate();
//            VpnHoodApp = VpnHoodApp.Init(this, new AppOptions { });
//            Current = this;
//        }
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                VpnHoodApp?.Dispose();
//                VpnHoodApp = null;
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
