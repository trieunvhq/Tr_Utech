using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using BLL.Utils;
using HDLIB.Common;

namespace PISAS_API.Service.UploadSigle
{
    public class UploadSigle : IUploadSigle
    {
        public HttpPostedFile File {get; set;}

        public string OriginName { get; set;}

        public string ThumbName { get; set; }

        public string KeyMessage {get; set; }

        public string Message {get; set; }

        public bool Success {get; set; }

        public UploadSigle() {}

        public UploadSigle(HttpPostedFile File)
        {
            StartHandle(File);
        }

        public UploadSigle(string dataBase64, string storeRootKey, string subPath,
            string preFixFileName, string orgFileName, DateTime? dateTime, bool buildFileName)
        {
            orgFileName = orgFileName?.Trim();
            dataBase64 = dataBase64?.Trim();
            StartHandle(dataBase64, storeRootKey, subPath, preFixFileName, orgFileName, dateTime, buildFileName);
        }

        private void StartHandle(string dataBase64, 
            string storeRootKey, string subPath,
            string preFixFileName, string orgFileName, DateTime? dateTime, bool buildFileName,
            int Width = 100, int Height = 100, string Mode = "crop")
        {
            try
            {
                if (string.IsNullOrEmpty(dataBase64))
                {
                    KeyMessage = "FILE_IS_REQUIRED";  
                    Message = "Tệp tải lên bị trống";
                    Success = false;
                    return;
                }

                var path = FileHelper.BuildPath(FileHelper.GetStorePath(storeRootKey), dateTime);
                if (!string.IsNullOrEmpty(subPath?.Trim()))
                {
                    path = Path.Combine(path, subPath?.Trim());
                }
                var originName = FileHelper.BuildFileName(preFixFileName, orgFileName, true);
                var thumbName = FileHelper.BuildFileName(preFixFileName, orgFileName, true);
                var originPath = Path.Combine(path, originName);
                var thumbPath = Path.Combine(path, thumbName);

                FileHelper.StorFileFromBase64(path, originName, dataBase64);
                
                var job = new ImageResizer.ImageJob(originPath, thumbPath, new ImageResizer.Instructions($"width={Width};height={Height};mode={Mode}"));
                job.Build();

               // File = FileInput;
                OriginName = originName;
                ThumbName = thumbName;
                KeyMessage = "UPLOAD_SUCCESS";
                Message = APIMessage.UPLOAD_SUCCESS;
                Success = true;
                return;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                KeyMessage = APIErrorCode.UPLOAD_FAILED;
                Message = APIMessage.UPLOAD_FAILED;
                Success = false;
            }
        }

        [Obsolete]
        public void StartHandle(HttpPostedFile FileInput, int Width = 100, int Height = 100, string Mode = "crop")
        {
            try
            {
                if (FileInput == null || FileInput.ContentLength <= 0)
                {
                    KeyMessage = APIErrorCode.IMAGE_IS_REQUIRED;
                    Message = APIMessage.IMAGE_IS_REQUIRED;
                    Success = false;
                    return;
                }

                var ext = Path.GetExtension(FileInput.FileName);
                var originName = $"{Guid.NewGuid()}-{DateTime.Now.ToString("yyyyMMddhhmmss")}{ext}";
                var thumbName = $"{Guid.NewGuid()}-{DateTime.Now.ToString("yyyyMMddhhmmss")}{ext}";

                var path = System.Configuration.ConfigurationSettings.AppSettings["ImagePath"];
                var originPath = Path.Combine(path, originName);
                var thumbPath = Path.Combine(path, thumbName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileInput.SaveAs(originPath);

                var job = new ImageResizer.ImageJob(originPath, thumbPath, new ImageResizer.Instructions($"width={Width};height={Height};mode={Mode}"));
                job.Build();

                File = FileInput;
                OriginName = originName;
                ThumbName = thumbName;
                KeyMessage = "UPLOAD_SUCCESS";
                Message = APIMessage.UPLOAD_SUCCESS;
                Success = true;
                return;
            }
            catch(Exception ex)
            {
                Logging.LogError(ex);
                KeyMessage = APIErrorCode.UPLOAD_FAILED;
                Message = APIMessage.UPLOAD_FAILED;
                Success = false;
            }
        }
    }
}
