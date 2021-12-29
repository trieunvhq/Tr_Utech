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

[assembly: ExportRenderer(typeof(QRMS.Helper.DatepickerCustoms), typeof(QRMS.Droid.Renderer.DatepickerCustomsRenderer))]
namespace QRMS.Droid.Renderer
{
    public class DatepickerCustomsRenderer : DatePickerRenderer
    {
        public DatepickerCustomsRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                this.Control.SetBackground(null);
                Control.Gravity = GravityFlags.CenterVertical;
                Control.SetPadding(30, 0, 20, 0);
            }
        }
    }
}