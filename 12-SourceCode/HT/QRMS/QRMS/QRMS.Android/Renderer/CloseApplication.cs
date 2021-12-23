using System;
using Android.App;
using QRMS.Droid.Renderer;
using QRMS.interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace QRMS.Droid.Renderer
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}
