using Android.Content;
using QRMS.Controls;
using QRMS.Droid.Renderer;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomTimePicker), typeof(CustomTimePickerRenderer))]
namespace QRMS.Droid.Renderer
{
    public class CustomTimePickerRenderer : TimePickerRenderer
    {
        public CustomTimePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetBackground(null);
                Control.SetPadding(0, 0, 0, 0);
                //Control.Text = "HH:mm";
                //Control.SetTextColor(Color.FromHex("#ACACB1").ToAndroid());
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
            {
                Control.SetTextColor(Color.FromHex("#17233D").ToAndroid());
            }
        }
    }
}