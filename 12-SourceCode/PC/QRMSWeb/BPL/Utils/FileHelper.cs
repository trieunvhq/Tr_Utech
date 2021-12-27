using HDLIB.Common;
using System;
//using System.Drawing;
using System.IO;
using System.Linq;

namespace BLL.Utils
{
    public class FileHelper
    {
        public static string StorFileFromBase64(string path, string fileName, string dataBase64)
        {
            dataBase64 = dataBase64?.Trim();
            path = path?.Trim();
            fileName = fileName?.Trim();
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(fileName)) return "";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var outFilePath = Path.Combine(path, fileName);
            string dataImage = "";
            if (!string.IsNullOrEmpty(dataBase64))
            {
                var idx = dataBase64.LastIndexOf("base64,") + 7;
                if (idx >= 7 && idx < dataBase64.Length)
                {
                    dataImage = dataBase64.Substring(idx, dataBase64.Length - idx);
                }
                else
                {
                    dataImage = dataBase64;
                }
            }

            var bytes = Convert.FromBase64String(dataImage);
            using (var imageFile = new FileStream(outFilePath, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }
            //File.WriteAllBytes(outFilePath, bytes);
            return fileName;

        }
        public static void DeleteFileLike(string folder, string text, string extend)
        {
            if (string.IsNullOrEmpty(text)) { return; }
            string[] files = System.IO.Directory.GetFiles(folder, $"*.{extend.Trim('.')}");
            files = files.Where(a => a.ToLower().Contains(text.ToLower())).ToArray();
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }
        }
        public static string DeleteFile(string storeRootKey, string subPath,
            string preFixFileName, string fileName, DateTime? dateTime, bool buildFileName)
        {

            try
            {
                preFixFileName = preFixFileName?.Trim();
                fileName = fileName?.Trim();
                var path = FileHelper.BuildPath(FileHelper.GetStorePath(storeRootKey), dateTime);
                if (!string.IsNullOrEmpty(subPath?.Trim()))
                {
                    path = Path.Combine(path, subPath?.Trim());
                }
                if (string.IsNullOrEmpty(fileName)) return null;

                try
                {
                    string file_name = FileHelper.BuildFileName(preFixFileName, fileName, buildFileName);

                    var outFilePath = Path.Combine(path, fileName);
                    if (File.Exists(outFilePath))
                    {
                        File.Delete(outFilePath);
                    }
                    return $"{file_name.Trim()}";
                }
                catch (Exception e)
                {
                    Logging.LogError(e);
                    //throw e;
                }
                return "";
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw ex;
            }
        }
        public static string GetDataBase64FromFile(string path, string fileName, bool isImage)
        {
            path = path?.Trim();
            fileName = fileName?.Trim();
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(path))
            {
                return "";
            }
            string full_path = Path.Combine(path, fileName);

            try
            {
                if (File.Exists(full_path))
                {
                    if (isImage)
                    {
                        /*
                        using (Image image = Image.FromFile(full_path))
                        {
                            using (MemoryStream m = new MemoryStream())
                            {
                                image.Save(m, image.RawFormat);
                                byte[] imageBytes = m.ToArray();
                                return Convert.ToBase64String(imageBytes);
                            }
                        }
                        */
                    }
                    else
                    {
                        var files = System.IO.File.ReadAllBytes(full_path);
                        return Convert.ToBase64String(files);
                    }
                }
                else
                {
                    Logging.LogMessage("khong tim thay image file: " + full_path);
                }
            }
            catch (Exception ex)
            {
                Logging.LogMessage("Co loi get image file: " + path + "\\" + fileName);
                Logging.LogError(ex);
                throw;
            }
            return "";
        }

        public static string GetDataBase64(string storeRootKey, string subPath, string fileName, DateTime? dateTime, bool isImageFile)
        {

            var path = FileHelper.BuildPath(FileHelper.GetStorePath(storeRootKey), dateTime);
            if (!string.IsNullOrEmpty(subPath?.Trim()))
            {
                path = Path.Combine(path, subPath?.Trim());
            }
            return FileHelper.GetDataBase64FromFile(path, fileName, isImageFile);
        }
        public static string StoreDataBase64(string base64Content, string storeRootKey, string subPath,
            string preFixFileName, string fileName, DateTime? dateTime, bool buildFileName)
        {

            try
            {
                base64Content = base64Content?.Trim();
                preFixFileName = preFixFileName?.Trim();
                fileName = fileName?.Trim();
                var path = FileHelper.BuildPath(FileHelper.GetStorePath(storeRootKey), dateTime);
                if (!string.IsNullOrEmpty(subPath?.Trim()))
                {
                    path = Path.Combine(path, subPath?.Trim());
                }
                if (string.IsNullOrEmpty(base64Content) || string.IsNullOrEmpty(fileName)) return null;

                try
                {
                    string file_name = fileName;
                    if (buildFileName)
                    {
                        file_name = FileHelper.BuildFileName(preFixFileName, fileName, buildFileName);


                    }
                    file_name = FileHelper.StorFileFromBase64(path, file_name, base64Content);
                    return $"{file_name.Trim()}";
                }
                catch (Exception e)
                {
                    Logging.LogError(e);
                    throw e;
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw ex;
            }
        }


        public static string BuildPath(string rootPath, DateTime? date)
        {
            var retPath = rootPath?.Trim();
            if (string.IsNullOrEmpty(retPath)) return retPath;
            if (date != null)
            {
                retPath = Path.Combine(retPath, date.Value.ToString("yyyy"));
                retPath = Path.Combine(retPath, date.Value.ToString("MM"));
                retPath = Path.Combine(retPath, date.Value.ToString("dd"));
            }
            if (!Directory.Exists(retPath))
            {
                Directory.CreateDirectory(retPath);
            }
            return retPath;
        }

        public static string BuildFileName(string preFixFileName, string fileName, bool buildFileName)
        {
            string file_name = fileName;
            if (buildFileName)
            {
                string ext = "";
                if (!string.IsNullOrEmpty(fileName))
                {
                    ext = Path.GetExtension(fileName);
                }
                file_name = $"{Guid.NewGuid()}-{DateTime.Now.ToString("yyyyMMddhhmmss")}{ext}";
            }
            if (!string.IsNullOrEmpty(preFixFileName?.Trim()) && !string.IsNullOrEmpty(file_name))
            {
                file_name = preFixFileName + file_name;
            }
            return file_name;
        }

        public static string GetStorePath(string keyPath)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return System.Configuration.ConfigurationSettings.AppSettings[keyPath];
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public static string GetLinkFile(string host, string controller, string fileName, DateTime? date)
        {
            try
            {
                if (!string.IsNullOrEmpty(host))
                {
                    return string.Format("{0}/{1}?fileName={2}&date={3}", host, controller, fileName, date?.ToString("yyyy/MM/dd"));
                }
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public static string GetFilePath(string storeRootKey, string subPath, DateTime? dateTime)
        {
            try
            {
                var path = FileHelper.BuildPath(FileHelper.GetStorePath(storeRootKey), dateTime);
                if (!string.IsNullOrEmpty(subPath?.Trim()))
                {
                    path = Path.Combine(path, subPath?.Trim());
                }

                return path;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public static bool MoveFileUpload(string srcStoreRootKey, string srcSubPath,
            string desStoreRootKey, string desSubPath,
            string fileName, DateTime? dateTime)
        {
            try
            {
                srcStoreRootKey = srcStoreRootKey?.Trim();
                srcSubPath = srcSubPath?.Trim();
                desStoreRootKey = desStoreRootKey?.Trim();
                desSubPath = desSubPath?.Trim();
                fileName = fileName?.Trim();
                if (string.IsNullOrEmpty(fileName)) return false;

                var srcPath = FileHelper.GetStorePath(srcStoreRootKey);
                if (!string.IsNullOrEmpty(srcSubPath?.Trim()))
                {
                    srcPath = Path.Combine(srcPath, srcSubPath?.Trim());
                }
                if (!Directory.Exists(srcPath))
                {
                    throw new Exception("Thư mục [" + srcPath + "] nguồn không tìm thấy thư mục nguồn");
                }
                var sourceFile = Path.Combine(srcPath, fileName);
                if (File.Exists(sourceFile))
                {
                    var desPath = FileHelper.BuildPath(FileHelper.GetStorePath(desStoreRootKey), dateTime);
                    if (!string.IsNullOrEmpty(desSubPath?.Trim()))
                    {
                        desPath = Path.Combine(desPath, desSubPath?.Trim());
                    }
                    if (!Directory.Exists(desPath))
                    {
                        Directory.CreateDirectory(desPath);
                    }
                    var destFile = Path.Combine(desPath, fileName);

                    File.Copy(sourceFile, destFile, true);
                    File.Delete(sourceFile);
                }
                else
                {
                    throw new Exception("File [" + sourceFile + "] nguồn không tồn tại");
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                return false;
            }

            return true;
        }
    }
}

