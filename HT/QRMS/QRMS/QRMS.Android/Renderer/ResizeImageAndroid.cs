
using System;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using QRMS.Droid.Renderer;
using QRMS.interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(ResizeImageAndroid))]
namespace QRMS.Droid.Renderer
{
	public class ResizeImageAndroid : ResizeImage
	{
		public byte[] Resize(byte[] imageData, float width, float height)
		{
			// Load the bitmap
			Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
			Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

			using (MemoryStream ms = new MemoryStream())
			{
				resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
				return ms.ToArray();
			}
		}
	}
}
