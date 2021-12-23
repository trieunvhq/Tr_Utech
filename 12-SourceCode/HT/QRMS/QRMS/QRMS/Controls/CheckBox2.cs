using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.Controls
{
    public class CheckBox2 : CheckableImage
    {
        private const string UNCHECKED_IMAGE_SOURCE = "checkbox_unchecked.png";
        private const string CHECKED_IMAGE_SOURCE = "checkbox2_checkmark.png";

        public CheckBox2() : base(CHECKED_IMAGE_SOURCE, UNCHECKED_IMAGE_SOURCE)
        {
            Size = 16.88;
        }
    }
}