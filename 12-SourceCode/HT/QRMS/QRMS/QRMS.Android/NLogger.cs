using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRMS.Droid;
using QRMS.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(NLogger))]
namespace QRMS.Droid
{
    public class NLogger : ILogger
    {
        public void Log(string ex)
        {
            var sdcardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var FilePath = System.IO.Path.Combine(sdcardPath, "FileLogQRMS.txt");

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(FilePath, true))
            {
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += System.Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += System.Environment.NewLine;
                message += ex;
                message += System.Environment.NewLine;
                message += string.Format("Message: {0}", message);
                message += System.Environment.NewLine;
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}