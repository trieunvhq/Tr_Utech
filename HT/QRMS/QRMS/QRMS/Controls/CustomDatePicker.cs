using Xamarin.Forms;

namespace QRMS.Controls
{
    public class CustomDatePicker : DatePicker
    {
        public CustomDatePicker()
        {
            Format = "dd/MM/yyyy";
        }
    }
}