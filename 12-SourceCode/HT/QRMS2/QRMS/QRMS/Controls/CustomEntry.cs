using Xamarin.Forms;

namespace QRMS.Controls
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(CustomEntry), 0);
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(CustomEntry), 0);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry), Color.Transparent);
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(CustomEntry), new Thickness());

        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public int BorderThickness
        {
            get { return (int)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }
    }
}
