using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views.InputMethods;
using QRMS.Controls;
using QRMS.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor2), typeof(CustomEditorRenderer))]

namespace QRMS.Droid.Renderer
{
	public class CustomEditorRenderer : EditorRenderer
	{
		public CustomEditorRenderer(Context context) : base(context)
		{
		}
		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				var view = (CustomEditor2)Element;

				var _gradientBackground = new GradientDrawable();
				_gradientBackground.SetShape(ShapeType.Rectangle);
				_gradientBackground.SetColor(view.BackgroundColor.ToAndroid());
				_gradientBackground.SetStroke((int)DpToPixels(this.Context, (float)view.BorderThickness), view.BorderColor.ToAndroid());

				_gradientBackground.SetCornerRadius(
					DpToPixels(this.Context, Convert.ToSingle(6)));
				Control.SetBackground(_gradientBackground);
				Control.SetPadding((int)DpToPixels(this.Context, 7), (int)DpToPixels(this.Context, 7)
					, (int)DpToPixels(this.Context, 7), (int)DpToPixels(this.Context, 7));
				Control.HorizontalScrollBarEnabled = false;
				Control.VerticalScrollBarEnabled = true;

				Control.SetImeActionLabel("Done", ImeAction.Done);
				Control.ImeOptions = (ImeAction)ImeFlags.NoFullscreen
					| ImeAction.Done;
				 
			}
			//if (Control != null)
			//{
			//	Control.ImeOptions = (ImeAction)ImeFlags.NoExtractUi;
			//}
		}
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
			if (Control != null)
			{
				var view = (CustomEditor2)Element;

				var _gradientBackground = new GradientDrawable();
				_gradientBackground.SetShape(ShapeType.Rectangle);
				_gradientBackground.SetColor(view.BackgroundColor.ToAndroid());
				_gradientBackground.SetStroke((int)DpToPixels(this.Context, (float)view.BorderThickness), view.BorderColor.ToAndroid());

				_gradientBackground.SetCornerRadius(
					DpToPixels(this.Context, Convert.ToSingle(6)));
				Control.SetBackground(_gradientBackground);
				Control.SetPadding((int)DpToPixels(this.Context, 7), (int)DpToPixels(this.Context, 7)
					, (int)DpToPixels(this.Context, 7), (int)DpToPixels(this.Context, 7));
				Control.HorizontalScrollBarEnabled = false;
				Control.VerticalScrollBarEnabled = true;

				Control.SetImeActionLabel("Done", ImeAction.Done);
				Control.ImeOptions = (ImeAction)ImeFlags.NoFullscreen
					| ImeAction.Done;

			}
		}
        private float DpToPixels(Context context, float v)
		{
			DisplayMetrics metrics = context.Resources.DisplayMetrics;
			return TypedValue.ApplyDimension(ComplexUnitType.Dip, v, metrics);
		}
	}
}
