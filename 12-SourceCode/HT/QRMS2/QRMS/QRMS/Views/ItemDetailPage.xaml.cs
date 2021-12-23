using QRMS.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace QRMS.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}