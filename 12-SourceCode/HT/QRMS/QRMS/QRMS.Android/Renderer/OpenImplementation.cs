using System;
using Android.Content;
using QRMS.Droid.Renderer;
using QRMS.interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(OpenImplementation))]
namespace QRMS.Droid.Renderer
{
    public class OpenImplementation : IOpenManager
    {
        public void openMail(string urlMail)
        {
            Intent intent = new Intent(Intent.ActionMain);
            intent.AddCategory(Intent.CategoryAppEmail);
            intent.AddFlags(ActivityFlags.NewTask);
            //FLAG_ACTIVITY_NEW_TASK
            Android.App.Application.Context.StartActivity(intent);
        }
    }
}
