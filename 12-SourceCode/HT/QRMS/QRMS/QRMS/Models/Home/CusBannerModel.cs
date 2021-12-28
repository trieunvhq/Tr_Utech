using System;
using System.Collections.Generic;

namespace QRMS.Models.Home
{
    public class CusBannerModel
    {
        public int Position { get; set; }
        public List<ImageCusBannerModel> lstImage { get; set; }
    }
}
