using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QRMSWeb.Helper
{
    public class ImageHelper
    {
        public static Image Base64ToImage(string base64String)
        {
            Image image = null;
            try
            {

                byte[] bytes = Convert.FromBase64String(FixBase64ForImage(base64String));

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
            }
            catch (Exception e)
            {

            }
            return image;

        }
        public static string Base64WithContentType(string base64String)
        {
            /*
            try { 
            var image = Base64ToImage(base64String);
            if (image != null)
            {
                string contentType = "";
                if ("png".Equals(image.RawFormat.ToString()))
                {
                    contentType = "image/png";
                } else if ("png".Equals(image.RawFormat.ToString()))
                {
                    contentType = "image/png";
                }    
                return $"data:{contentType};base64,{base64String}";
            }
            }catch(Exception e)
            {

            }
            */
            if (string.IsNullOrEmpty(base64String))
            {
                return "";
            }
            if (base64String.Contains(";base64,")) {
                return base64String;
            } else { 
                return $"data:;base64,{base64String}";
            }
        }
        public static string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", String.Empty); sbText.Replace(" ", String.Empty);
            return sbText.ToString();
        }
    }
}
