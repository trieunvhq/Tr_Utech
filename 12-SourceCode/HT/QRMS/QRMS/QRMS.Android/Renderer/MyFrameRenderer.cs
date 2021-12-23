using System;
using Android.Content;
using QRMS.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Frame), typeof(MyFrameRenderer))]
namespace QRMS.Droid.Renderer
{
    public class MyFrameRenderer : Xamarin.Forms.Platform.Android.FastRenderers.FrameRenderer
    {
        public MyFrameRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            CardElevation = 10;

            SetOutlineSpotShadowColor(Xamarin.Forms.Color.Gray.ToAndroid());
        }
    }
}
