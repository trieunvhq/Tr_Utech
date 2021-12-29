 
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Controls
{
    public class SwitchCustom : CheckableImage
    {
        private const string UNCHECKED_IMAGE_SOURCE = "switch2.png";
        private const string CHECKED_IMAGE_SOURCE = "switch1.png";

        public SwitchCustom() : base(CHECKED_IMAGE_SOURCE, UNCHECKED_IMAGE_SOURCE)
        {
            Size = 32;
        }
    }
}