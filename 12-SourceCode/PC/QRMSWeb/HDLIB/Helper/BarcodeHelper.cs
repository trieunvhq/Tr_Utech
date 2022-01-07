using BarcodeLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB
{
	public class BarcodeHelper
	{
		public static Image CreateBarcode(string code)
		{
			Barcode barcodeAPI = new Barcode();
			// Define basic settings of the image
			int imageWidth = 140;
			int imageHeight = 120;
			Color foreColor = Color.Black;
			Color backColor = Color.White;
			//barcodeAPI.Alignment = AlignmentPositions.LEFT;
			//barcodeAPI.RotateFlipType = RotateFlipType.RotateNoneFlipNone;

			// Generate the barcode with your settings
			var barcodeImage = barcodeAPI.Encode(TYPE.CODE128, code, foreColor, backColor, imageWidth, imageHeight);
			//ImageConverter ic = new ImageConverter();
			//byte[] buffer = (byte[])ic.ConvertTo(barcodeImage, typeof(byte[]));
			//var result = Convert.ToBase64String(buffer, Base64FormattingOptions.InsertLineBreaks);
			//var _saveBarcode = (Bitmap)new ImageConverter().ConvertFrom(Convert.FromBase64String(result));
			//_saveBarcode.Save("E:/barcodetest.png");
			return barcodeImage;
		}

		public static string CreateBarcodeImgAndSave(string code)
		{
			Barcode barcodeAPI = new Barcode();
			// Define basic settings of the image
			int imageWidth = 180;
			int imageHeight = 120;
			Color foreColor = Color.Black;
			Color backColor = Color.White;
			barcodeAPI.Alignment = AlignmentPositions.LEFT;
			//barcodeAPI.RotateFlipType = RotateFlipType.RotateNoneFlipNone;

			// Generate the barcode with your settings
			var barcodeImage = barcodeAPI.Encode(TYPE.CODE128, code, foreColor, backColor, imageWidth, imageHeight);
			ImageConverter ic = new ImageConverter();
			byte[] buffer = (byte[])ic.ConvertTo(barcodeImage, typeof(byte[]));
			var result = Convert.ToBase64String(buffer, Base64FormattingOptions.InsertLineBreaks);
			//var _saveBarcode = (Bitmap)new ImageConverter().ConvertFrom(Convert.FromBase64String(result));
			//_saveBarcode.Save("E:/barcodetest.png");
			
			return result;
		}

		public static string CreateBarcodeImg(string code)
		{
			Barcode barcodeAPI = new Barcode();
			// Define basic settings of the image
			int imageWidth = 240;
			int imageHeight = 100;
			Color foreColor = Color.Black;
			Color backColor = Color.White;
			barcodeAPI.Alignment = AlignmentPositions.LEFT;

			// Generate the barcode with your settings
			var barcodeImage = barcodeAPI.Encode(TYPE.CODE128, code, foreColor, backColor, imageWidth, imageHeight);
			ImageConverter ic = new ImageConverter();
			byte[] buffer = (byte[])ic.ConvertTo(barcodeImage, typeof(byte[]));
			var result =  Convert.ToBase64String(buffer, Base64FormattingOptions.InsertLineBreaks);
			var imgType = "jpg";
			var htmlPictureTag = $"data:image/{imgType.ToString().ToLower()};base64,{result}";
			//var x = (Bitmap)new ImageConverter().ConvertFrom(Convert.FromBase64String(result));
			//x.Save("E:/barcodetest.png");
			return htmlPictureTag;

		}
	}
}
