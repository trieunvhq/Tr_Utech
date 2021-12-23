using System;
using System.Threading.Tasks;
using Android.Content;
using QRMS.Droid.Renderer;
using QRMS.interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShareAndroid))]
namespace QRMS.Droid.Renderer
{
    class ShareAndroid : IShare
    {
        private readonly Context context;
        public ShareAndroid()
        {
            context = Android.App.Application.Context;
        }
        public Task Show(string title, string message, string filePath)
        {
            var uri = Android.Net.Uri.Parse("file://" + filePath);
            var contentType = "application/pdf";
            var intent = new Intent(Intent.ActionSend);
            intent.PutExtra(Intent.ExtraStream, uri);
            intent.PutExtra(Intent.ExtraText, string.Empty);
            intent.PutExtra(Intent.ExtraSubject, message ?? string.Empty);
            intent.SetType(contentType);
            var chooserIntent = Intent.CreateChooser(intent, title ?? string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            context.StartActivity(chooserIntent);

            return Task.FromResult(true);
        }
    }
}
