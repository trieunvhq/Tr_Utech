using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;

[assembly: ExportRenderer(typeof(QRMS.Helper.CustomEntryLogin), typeof(QRMS.Droid.Renderer.CustomEntryLoginRenderer))]
namespace QRMS.Droid.Renderer
{
    public class CustomEntryLoginRenderer : EntryRenderer
    {
        public CustomEntryLoginRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                this.Control.SetBackground(null);
                Control.Gravity = GravityFlags.Center;
                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}