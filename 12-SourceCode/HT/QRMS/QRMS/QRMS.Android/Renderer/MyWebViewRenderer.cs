
using System.ComponentModel;
using Android.Content;
using QRMS.Controls;
using QRMS.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AutoWebView), typeof(MyWebViewRenderer))]
namespace QRMS.Droid.Renderer
{
    public class MyWebViewRenderer : WebViewRenderer
    {
        public MyWebViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.Settings.DefaultFontSize = 14;
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            } 
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.SetWebViewClient(new ExtendedWebViewClient(Element as AutoWebView));
        }

        class ExtendedWebViewClient : Android.Webkit.WebViewClient
        {
            AutoWebView xwebView;
            public ExtendedWebViewClient(AutoWebView webView)
            {
                xwebView = webView;
            }

            async public override void OnPageFinished(Android.Webkit.WebView view, string url)
            {
                try
                {
                    if (xwebView != null)
                    {
                        int i = 10;
                        while (view.ContentHeight == 0 && i-- > 0) // wait here till content is rendered
                            await System.Threading.Tasks.Task.Delay(100);
                        xwebView.HeightRequest = view.ContentHeight;
                        // Here use parent to find the ViewCell, you can adjust the number of parents depending on your XAML
                        (xwebView.Parent.Parent as ViewCell).ForceUpdateSize();
                    }
                }
                catch { }

                base.OnPageFinished(view, url);
            }
        }
    }
}