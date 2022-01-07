using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using QRCoder;

namespace HDLIB
{
    public class QRCodeHelper
    {
        private static QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
        public static Bitmap GetQRCodeBitmap(string Text)
        {
            try
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(Text, QRCodeGenerator.ECCLevel.L);
                QRCode qrCode = new QRCode(qrCodeData);
                return qrCode.GetGraphic(4, Color.Black, Color.White, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetQRCodeBase64ForImgTag(string Text)
        {
            try
            {
                var imgType = Base64QRCode.ImageType.Png;
                string qrCodeImageAsBase64 = GetQRCodeBase64(Text,(int)imgType);
                var htmlPictureTag = $"data:image/{imgType.ToString().ToLower()};base64,{qrCodeImageAsBase64}";
                return htmlPictureTag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetQRCodeBase64(string Text, int type = 2)
        {
            try
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(Text, QRCodeGenerator.ECCLevel.L);
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                string qrCodeImageAsBase64 = qrCode.GetGraphic(4, Color.Black, Color.White, false, (Base64QRCode.ImageType)type);
                qrCodeData.Dispose();
                qrCode.Dispose();
                return qrCodeImageAsBase64;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		
	}
}
