using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HDLIB.Common
{
    public class FileUtils
    {
        public static Boolean IsFileLocked(string file_path)
        {

            FileStream stream = null;

            try
            {
                var file = new FileInfo(file_path);
                //Don't change FileAccess to ReadWrite, 
                //because if a file is in readOnly, it fails.
                stream = file.Open
                (
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None
                );
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public static Boolean WaitUnlockFile(string file_path, int time = 30)
        {
            var count = 0;
            while (count < 30 && FileUtils.IsFileLocked(file_path))
            {
                Thread.Sleep(100);
                count++;
            }
            return !(count == 30 && FileUtils.IsFileLocked(file_path));
        }
    }
}
