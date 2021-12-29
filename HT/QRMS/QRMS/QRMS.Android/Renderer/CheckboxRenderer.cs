using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(QRMS.Helper.Checkbox), typeof(QRMS.Droid.Renderer.CheckboxRenderer))]
namespace QRMS.Droid.Renderer
{
    public class CheckboxRenderer : CheckBoxRenderer
    {
        public CheckboxRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.CheckBox> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                Control.ButtonTintList = ColorStateList.ValueOf(Android.Graphics.Color.Rgb(244, 154, 14));
            }
        }
    }
}