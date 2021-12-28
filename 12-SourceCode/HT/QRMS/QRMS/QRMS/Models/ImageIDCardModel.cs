using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Models
{
    public class ImageIDCardModel
    {
        public string ImgType { get; set; }
        public string ImgName { get; set; }
        public byte[] ImgData { get; set; }
    }
}
