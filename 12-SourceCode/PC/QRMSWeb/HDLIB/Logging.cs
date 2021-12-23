using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDLIB
{
	public class Logging
	{
        static readonly object _object = new object();
        public static void LogError(Exception ex)
        {
            lock (_object)
            {
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex);
                message += Environment.NewLine;
                if (ex.InnerException != null)
                {
                    message += string.Format("Inner Message: {0}", ex.InnerException.Message);
                }
                //message += Environment.NewLine;
                //message += string.Format("StackTrace: {0}", ex.StackTrace);
                //message += Environment.NewLine;
                //message += "-----------------------------------------------------------";
                //message += Environment.NewLine;
                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                // Replace redundance in path
                path = path.Replace("file:\\", "");
                path += "\\log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += $"\\log-{DateTime.Today.ToString("yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)}.txt";

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
        }

        public static void Log(Exception ex, string var)
        {
            lock (_object)
            {
                Exception newex = new Exception(var, ex);
                LogError(newex);
            }
        }
        public static void Log(object var)
        {
            lock (_object)
            {
                Exception newex = new Exception(var.ToString());
                LogError(newex);
            }
        }
        public static void LogMessage(string mess)
        {
            try
            {
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += mess;
                message += Environment.NewLine;
                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                // Replace redundance in path
                path = path.Replace("file:\\", "");
                path += "\\log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += $"\\log-{DateTime.Today.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)}.log";
                string logFile = string.Format("log\\Log_{0}.log", DateTime.Today.ToString("yyyyMMdd"));
                //string path = System.IO.Path.Combine(Common.GlobalVariable.LogPath, string.Format("log_{0}.log", DateTime.Now.ToString("yyyyMMdd")));
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch { }
        }
    }
}
