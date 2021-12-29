using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage), typeof(QRMS.Droid.Renderer.MyPageRenderer))]
namespace QRMS.Droid.Renderer
{
    public class MyPageRenderer : PageRenderer
    {
        public MyPageRenderer(Context context) : base(context)
        {
        }

        //protected override void OnAttachedToWindow()
        //{
        //    var myValue = Preferences.Get("ModelAnimation", false);
        //    if (myValue)
        //    {
        //        var metrics = Resources.DisplayMetrics;
        //        Android.Views.Animations.Animation translateAnimation = new TranslateAnimation(metrics.WidthPixels, 0, 0, 0);
        //        translateAnimation.Duration = 500;
        //        Animation = translateAnimation;
        //        translateAnimation.Start();
        //    }

        //    base.OnAttachedToWindow();
        //}
    }
}