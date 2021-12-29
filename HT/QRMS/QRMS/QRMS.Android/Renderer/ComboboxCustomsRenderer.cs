using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRMS.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(QRMS.Helper.ComboboxCustoms), typeof(QRMS.Droid.Renderer.ComboboxCustomsRenderer))]
namespace QRMS.Droid.Renderer
{
    public class ComboboxCustomsRenderer : PickerRenderer
    {
        public ComboboxCustomsRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                //this.Control.SetBackground(null);
                //Control.Gravity = GravityFlags.CenterVertical;
                //Control.SetPadding(15, 0, 15, 0);

                this.Control.SetBackground(null);
                Control.Gravity = GravityFlags.CenterVertical;
                Control.SetPadding(30, 0, 20, 0);



                //ComboboxCustoms view = (ComboboxCustoms)Element;
                //var gd = new GradientDrawable();
                //gd.SetColor(view.BackgroundColor.ToAndroid());
                //gd.SetCornerRadius(Context.ToPixels(view.CornerRadius));
                //gd.SetStroke((int)Context.ToPixels(view.BorderThickness), view.BorderColor.ToAndroid());
                //Control.SetBackground(gd);

                //var padTop = (int)Context.ToPixels(view.Padding.Top);
                //var padBottom = (int)Context.ToPixels(view.Padding.Bottom);
                //var padLeft = (int)Context.ToPixels(view.Padding.Left);
                //var padRight = (int)Context.ToPixels(view.Padding.Right);

                //Control.SetPadding(padLeft, padTop, padRight, padBottom);
            }
        }
    }
}