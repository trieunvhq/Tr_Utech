using System;
using QRMS.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WebView), typeof(CustomWebViewRenderer))]

namespace QRMS.Droid.Renderer
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Settings.BuiltInZoomControls = true;
                Control.Settings.DisplayZoomControls = false;
                Control.Settings.LoadWithOverviewMode = true;
                Control.Settings.UseWideViewPort = true;
            }
        }
    }
}
