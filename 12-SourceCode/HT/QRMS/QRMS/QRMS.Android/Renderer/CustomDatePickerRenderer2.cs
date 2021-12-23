using Android.Content;
using QRMS.Controls;
using QRMS.Droid.Renderer;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker2), typeof(CustomDatePickerRenderer2))]
namespace QRMS.Droid.Renderer
{
    public class CustomDatePickerRenderer2 : DatePickerRenderer
    {
        public CustomDatePickerRenderer2(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetBackground(null);
                Control.SetPadding(0, 0, 0, 0);
                //Control.Text = "DD/MM/YYYY";
                //Control.SetTextColor(Color.FromHex("#ACACB1").ToAndroid());
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == DatePicker.DateProperty.PropertyName)
            {
                Control.SetTextColor(Color.FromHex("#17233D").ToAndroid());
            }
        }
    }
}