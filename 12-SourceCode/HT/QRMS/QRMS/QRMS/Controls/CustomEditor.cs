using Xamarin.Forms;
namespace QRMS.Controls
{
    public class CustomEditor : Editor
    {
        public static readonly BindableProperty BorderColorProperty =
             BindableProperty.Create(
                 nameof(BorderColor),
                 typeof(Color),
                 typeof(CustomEditor), Color.Gray);
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        [System.Obsolete]
        public static readonly BindableProperty BorderWitdhProperty =
           BindableProperty.Create(
               nameof(BorderWitdh),
               typeof(double),
               typeof(CustomEditor), Device.OnPlatform<double>(0, 0, 0));

        [System.Obsolete]
        public double BorderWitdh
        {
            get => (double)GetValue(BorderWitdhProperty);
            set => SetValue(BorderWitdhProperty, value);
        }

        [System.Obsolete]
        public static readonly BindableProperty CornerRadiusProperty =
           BindableProperty.Create(
               nameof(CornerRadius),
               typeof(double),
               typeof(CustomEditor),

               Device.OnPlatform<double>(0, 0, 0));

        [System.Obsolete]
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        public static readonly BindableProperty IsCurvedConrnerEnableProperty =
           BindableProperty.Create(
               nameof(IsCurvedConrnerEnable),
               typeof(bool),
               typeof(CustomEditor), true);
        public bool IsCurvedConrnerEnable
        {
            get => (bool)GetValue(IsCurvedConrnerEnableProperty);
            set => SetValue(IsCurvedConrnerEnableProperty, value);
        }
    }
}
