using System;
using System.IO;
using QRMS.Droid.Renderer;
using QRMS.interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndroid))]
namespace QRMS.Droid.Renderer
{
    public class SaveAndroid : ISave
    {
        public string Save(MemoryStream stream,string str)
        {
            string root = null;
            string fileName = "SavedDocument.pdf";
            //Get the root folder of the application
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            //Create a new folder with name Syncfusion
            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();
            //Create a new file with the name fileName in the folder Syncfusion
            Java.IO.File file = new Java.IO.File(myDir, fileName);
            string filePath = file.Path;
            //If the file already exists delete it
            if (file.Exists()) file.Delete();
            Java.IO.FileOutputStream outs = new Java.IO.FileOutputStream(file);
            //Save the input stream to the created file
            outs.Write(stream.ToArray());
            var ab = file.Path;
            outs.Flush();
            outs.Close();
            return filePath;
        }
    }
}
