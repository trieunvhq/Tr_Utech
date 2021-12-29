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

[assembly: ExportRenderer(typeof(QRMS.Helper.BorderlessEntry), typeof(QRMS.Droid.Renderer.BorderlessEntryRenderer))]
namespace QRMS.Droid.Renderer
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer() : base(Application.Context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                this.Control.SetBackground(null);
                Control.Gravity = GravityFlags.CenterVertical;
                Control.SetPadding(30, 0, 20, 0);

                if (this.Element.Keyboard == Keyboard.Numeric)
                    this.Control.KeyListener = Android.Text.Method.DigitsKeyListener.GetInstance("1234567890,.");
            }
        }
    }
}