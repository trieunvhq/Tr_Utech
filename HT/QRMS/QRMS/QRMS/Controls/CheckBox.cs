using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Controls
{
    public class CheckBox : CheckableImage
    {
        private const string UNCHECKED_IMAGE_SOURCE = "checkbox_unchecked.png";
        private const string CHECKED_IMAGE_SOURCE = "checkbox_checked.png";

        public CheckBox() : base(CHECKED_IMAGE_SOURCE, UNCHECKED_IMAGE_SOURCE)
        {
            Size = 16.88;
        }
    }
}