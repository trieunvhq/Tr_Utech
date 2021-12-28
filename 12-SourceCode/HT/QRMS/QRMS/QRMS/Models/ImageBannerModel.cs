using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.Models
{
    public class ImageBannerModel
    {
        public int Position { get; set; }
        public string ImageBase64 { get; set; }
    }

    public class ImageModel
    {
        public int Position { get; set; }
        public ImageSource ImageBase64 { get; set; }
    }
}
