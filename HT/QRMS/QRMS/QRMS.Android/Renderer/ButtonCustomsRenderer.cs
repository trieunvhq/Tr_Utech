using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using QRMS.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(QRMS.Helper.ButtonCustoms), typeof(QRMS.Droid.Renderer.ButtonCustomsRenderer))]
namespace QRMS.Droid.Renderer
{
    public class ButtonCustomsRenderer : ButtonRenderer
    {
        //public ButtonCustomsRenderer(Context context) : base(context)
        //{
        //}

        //protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        //{
        //    base.OnElementChanged(e);

        //    if (this.Control != null)
        //    {
        //        Control.TextAlignment = Android.Views.TextAlignment.Center;
        //        Control.SetAllCaps(false);
        //    }
        //}

        public ButtonCustomsRenderer(Context context) : base(context)
        {
            _context = context;
        }
        private Context _context;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetAllCaps(false);

                var caller = e.NewElement as ButtonCustoms;
                if (caller.GradientOrientation == ButtonCustoms.GradientOrientationStates.Vertical)
                {
                    var gradient = new GradientDrawable(GradientDrawable.Orientation.TopBottom, new[] {
                        caller.StartColor.ToAndroid().ToArgb(),
                        caller.EndColor.ToAndroid().ToArgb()
                    });
                    gradient.SetStroke((int)caller.BorderWidth, caller.BorderColor.ToAndroid());

                    gradient.SetCornerRadius(DpToPixels(_context, caller.CornerRadius));
                    Control.SetBackground(gradient);
                    var num = caller.IsEnabled ? 105f : 100f;
                    Control.Elevation = num;
                    Control.TranslationZ = num;
                }
                else
                {

                    var gradient = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new[] {
                        caller.StartColor.ToAndroid().ToArgb(),
                        caller.EndColor.ToAndroid().ToArgb()
                    });

                    gradient.SetStroke((int)caller.BorderWidth, caller.BorderColor.ToAndroid());

                    gradient.SetCornerRadius(DpToPixels(_context, caller.CornerRadius));
                    Control.SetBackground(gradient);
                    var num = caller.IsEnabled ? 105f : 100f;
                    Control.Elevation = num;
                    Control.TranslationZ = num;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}