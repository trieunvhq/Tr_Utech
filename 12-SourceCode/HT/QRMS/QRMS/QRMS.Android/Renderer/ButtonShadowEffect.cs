using System;
using System.Linq;
using QRMS.Droid.Renderer;
using QRMS.interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(ButtonShadowEffect), "ButtonShadowEffect")]
namespace QRMS.Droid.Renderer
{
    public class ButtonShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control as Android.Widget.Button;
                var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
                if (effect != null)
                {
                    float radius = effect.Radius;
                    float distanceX = effect.DistanceX;
                    float distanceY = effect.DistanceY;
                    Android.Graphics.Color color = effect.Color.ToAndroid();
                    control.SetShadowLayer(radius, distanceX, distanceY, color);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
