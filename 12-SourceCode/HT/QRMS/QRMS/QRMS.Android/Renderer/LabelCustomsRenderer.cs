using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(QRMS.Helper.LabelCustoms), typeof(QRMS.Droid.Renderer.LabelCustomsRenderer))]
namespace QRMS.Droid.Renderer
{
    public class LabelCustomsRenderer : LabelRenderer
    {
        public LabelCustomsRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    Control.JustificationMode = Android.Text.JustificationMode.InterWord;
            }
        }
    }
}