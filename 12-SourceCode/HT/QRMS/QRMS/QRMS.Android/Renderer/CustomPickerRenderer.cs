using Android.Content;
using QRMS.Controls;
using QRMS.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace QRMS.Droid.Renderer
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetBackground(null);
                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}