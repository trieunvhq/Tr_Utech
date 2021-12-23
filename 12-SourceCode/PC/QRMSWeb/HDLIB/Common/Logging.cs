using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HDLIB.Common
{
    public class Logging
    {
        static readonly object _object = new object();

        public static void LogError(Exception ex)
        {
            Console.WriteLine(ex.Message);

            //lock (_object)
            //{
            bool lockWasTaken = false;
            try
            {
                lock (_object)
                {
                    lockWasTaken = true;
                    var builder = new StringBuilder();
                    builder.AppendLine(string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                    builder.AppendLine("----------------------------------------------------------------------");
                    builder.AppendLine(string.Format("Message: {0}", ex.Message));
                    builder.AppendLine(string.Format("StackTrace: {0}", ex.StackTrace));
                    builder.AppendLine("----------------------------------------------------------------------");
                    if (ex.InnerException != null)
                    {
                        builder.AppendLine("-------------- INNER ---------------------------------------------");
                        builder.AppendLine(string.Format("Message: {0}", ex.InnerException.Message));
                        builder.AppendLine(string.Format("StackTrace: {0}", ex.InnerException.StackTrace));
                        builder.AppendLine("------------------------------------------------------------------");
                    }

                    string message = builder.ToString();
                    writeToFile(message);

                }

                lockWasTaken = false;
            }
            catch (Exception ex1)
            {
                System.Console.WriteLine(ex1.Message);
            }
            finally
            {
                if (lockWasTaken)
                {
                    System.Threading.Monitor.Exit(_object);
                }
            }
            //}
        }

        #region trung cmt 29/3/2021

        //public static void LogMessage(string mess)
        //{
        //    try
        //    {
        //        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        //        builder.AppendLine(Environment.NewLine;
        //        builder.AppendLine(mess;
        //        builder.AppendLine(Environment.NewLine;
        //        string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        //        // Replace redundance in path
        //        path = path.Replace("file:\\", "");
        //        path += "\\log";
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        DateTime now = DateTime.Today.Date;
        //        //path += $"\\log-{DateTime.Today.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)}.log";
        //        path += String.Format("$\\log-{0}.log", now);
        //        string logFile = string.Format("log\\Log_{0}.log", DateTime.Today.ToString("yyyyMMdd"));
        //        //string path = System.IO.Path.Combine(Common.GlobalVariable.LogPath, string.Format("log_{0}.log", DateTime.Now.ToString("yyyyMMdd")));
        //        using (StreamWriter writer = new StreamWriter(path, true))
        //        {
        //            writer.WriteLine(message);
        //            writer.Close();
        //        }
        //    }
        //    catch { }
        //}
        #endregion

        public static void LogMessage(string mess)
        {
            try
            {
                var builder = new StringBuilder();
                builder.AppendLine(string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                builder.AppendLine("--------------------------------------------------------------------");
                builder.AppendLine(mess);
                builder.AppendLine("--------------------------------------------------------------------");

                string message = builder.ToString();
                /*string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                // Replace redundance in path
                path = path.Replace("file:\\", "").Replace("\\bin", ""); ;
                path += "\\log";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path += $"\\log-{DateTime.Today.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)}.log";

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }*/
                writeToFile(message);
            }
            catch { }
        }
        private static void writeToFile(string message)
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            // Replace redundance in path
            path = path.Replace("file:\\", "").Replace("\\bin", "");
            path += "\\log";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var log_file = path + $"\\log-{DateTime.Today.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)}.log";
            if (!File.Exists(log_file))
            {
                File.CreateText(log_file).Dispose();
            }
            if (File.Exists(log_file) && FileUtils.WaitUnlockFile(log_file))
            {
                using (StreamWriter writer = new StreamWriter(log_file, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }

            }
        }

        public void LogMessage(object obj)
        {
            JObject objJSON = JObject.FromObject(obj);
            LogMessage(objJSON.ToString());
        }

        public static void LogErrorTesting(Exception ex)
        {
            //lock (_object)
            //{
            bool lockWasTaken = false;
            try
            {
                lock (_object)
                {
                    lockWasTaken = true;
                    var builder = new StringBuilder();
                    builder.AppendLine(string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                    builder.AppendLine("----------------------------------------------------------------------");
                    builder.AppendLine(string.Format("Message: {0}", ex.Message));
                    builder.AppendLine(string.Format("StackTrace: {0}", ex.StackTrace));
                    builder.AppendLine("----------------------------------------------------------------------");
                    if (ex.InnerException != null)
                    {
                        builder.AppendLine("-------------- INNER ---------------------------------------------");
                        builder.AppendLine(string.Format("Message: {0}", ex.InnerException.Message));
                        builder.AppendLine(string.Format("StackTrace: {0}", ex.InnerException.StackTrace));
                        builder.AppendLine("------------------------------------------------------------------");
                    }

                    string message = builder.ToString();
                    string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    // Replace redundance in path
                    path = path.Replace("file:\\", ""); //.Replace("\\bin", "");
                    path += "\\log";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var log_file = path + $"\\log-{DateTime.Today.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)}.log";
                    if (!File.Exists(log_file))
                    {
                        File.CreateText(log_file).Dispose();
                    }
                    if (File.Exists(log_file) && FileUtils.WaitUnlockFile(log_file))
                    {
                        using (StreamWriter writer = new StreamWriter(log_file, true))
                        {
                            writer.WriteLine(message);
                            writer.Close();
                        }

                    }

                }

                lockWasTaken = false;
            }
            catch (Exception ex1)
            {
                System.Console.WriteLine(ex1.Message);
            }
            finally
            {
                if (lockWasTaken)
                {
                    System.Threading.Monitor.Exit(_object);
                }
            }
            //}
        }
    }
}

