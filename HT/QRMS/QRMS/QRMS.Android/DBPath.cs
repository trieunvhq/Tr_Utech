using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRMS.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(QRMS.Droid.DBPath))]
namespace QRMS.Droid
{
    public class DBPath : IDBPath
    {
        public string GetDBPath()
        {
            var DBName = "QRMS.db";
            var DBPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var LocalDB = Path.Combine(DBPath, DBName);

            return LocalDB;
        }
    }
}