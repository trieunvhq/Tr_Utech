using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace WebAPI.Helper
{
    public class FileHelper
    {
        public const string IMG_REPORT_PATH = "/Reports";
        public const string IMG_QR_PATH = "/Image/QR";
        public const string IMG_Barcode_PATH = "/Image/BarcodeForPartcode";
        public const string IMG_Barcode_PATH2 = "/Image/BarcodeForQuantity";
        public const string IMG_AVA_PATH = "/Image/Avatar";
        public const string IMG_LOGO_PATH = "/Image/Logo";
        public const string IMG_PICTURE_PATH = "/Image/Pictures";
        public const string IMPORT_TEMPLATE_PATH = "/Files/ImportTemplate";
        public const string EXPORT_TEMPLATE_PATH = "/Files/ExportTemplate";
        public enum ImportTemplateFile
        {
            ImportDeliveryInstructionItem,
            ImportMDIOrder,
            ImportPurchaseOrder,
            ImportSaleOrderItem,
            ImportWorkOrderMaterial,
            ImportTransferInstruction
        }
        // Private
        private const string IMG_TEMP_PATH = "/Image/Temp";

        public static string ImportFilePath(string template)
        {
            try
            {
                if (Enum.IsDefined(typeof(ImportTemplateFile), template))
                    return HttpContext.Current.Server.MapPath(IMPORT_TEMPLATE_PATH) + "\\" + template + ".xlsx";
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string ExportFilePath(string template)
        {
            try
            {
                return HttpContext.Current.Server.MapPath(EXPORT_TEMPLATE_PATH) + "\\" + template + ".xlsx";
            }
            catch
            {
                return string.Empty;
            }
        }
        public static void DeleteAllFileInFolder(string path)
        {
            string[] filePaths = Directory.GetFiles(path);
            for (int i = 0; i < filePaths.Length; i++)
            {
                System.IO.File.Delete(filePaths[i]);
            }
        }
        public static void DeleteFiles(params string[] fileNames)
        {
            for (int i = 0; i < fileNames.Length; i++)
            {
                if (File.Exists(fileNames[i]))
                {
                    System.IO.File.Delete(fileNames[i]);
                }
            }
        }
        public static string SaveFile(string path, HttpPostedFileBase fileData)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss.fff") + "_" + fileData.FileName.Replace(" ", "_");

            if (fileData.FileName.Length > 63)
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss.fff") + "_" + fileData.FileName.Replace(" ", "_").Substring(fileData.FileName.Length - 63); ;
            }

            if (fileData != null && fileData.ContentLength > 0)
            {
                string pathCV = HttpContext.Current.Server.MapPath(path) + "\\" + fileName;
                fileData.SaveAs(pathCV);
                return fileName;
            }

            return null;
        }
        public static string SaveQRImg(string path, System.Drawing.Bitmap imgData)
        {
            Guid guid = Guid.NewGuid();
            string fileName = guid.ToString() + "_" + "QR.png";

            if (imgData != null)
            {
                string pathQR = HttpContext.Current.Server.MapPath(path) + "\\" + fileName;
                Int32 w = imgData.Width;
                Int32 h = imgData.Height;
                Bitmap _result = new Bitmap(120, 120);
                using (Graphics g = Graphics.FromImage(_result))
                {
                    g.DrawImage(imgData, 0, 0, 120, 120);
                }
                _result.Save(pathQR);
                _result.Dispose();
                return fileName;
            }
            return null;
        }
        public static string SaveBarcodeImg(string path, string imgData)
        {
            //Guid guid = Guid.NewGuid();
            //string fileName = guid.ToString() + "_" + "PartCode.png";

            //if (imgData != null)
            //{
            //	string pathQR = HttpContext.Current.Server.MapPath(path) + "\\" + fileName;

            //	Int32 w = imgData.Width;
            //	Int32 h = imgData.Height;
            //	Bitmap _result = new Bitmap(200, 100);
            //	using (Graphics g = Graphics.FromImage(_result))
            //	{
            //		g.DrawImage(imgData, 0, 0, 200, 100);
            //	}
            //	_result.Save(pathQR);
            //	_result.Dispose();
            //	return fileName;
            //}
            if (imgData != null)
            {
                var base64Convert = Convert.FromBase64String(imgData);
                var _saveBarcode = (Bitmap)new ImageConverter().ConvertFrom(base64Convert);
                Guid guid = Guid.NewGuid();
                string fileName = guid.ToString() + "_" + "PartCode.jpg";
                string pathQR = HttpContext.Current.Server.MapPath(path) + "\\" + fileName;
                _saveBarcode.Save(pathQR, ImageFormat.Jpeg);
                _saveBarcode.Dispose();
                return fileName;
            }
            return null;
        }

        public static string SaveBarcodeImg2(string path, System.Drawing.Bitmap imgData)
        {
            Guid guid = Guid.NewGuid();
            string fileName = guid.ToString() + "_" + "Quantity.jpg";

            if (imgData != null)
            {
                string pathQR = HttpContext.Current.Server.MapPath(path) + "\\" + fileName;

                Int32 w = imgData.Width;
                Int32 h = imgData.Height;
                Bitmap _result = new Bitmap(w, h);
                using (Graphics g = Graphics.FromImage(_result))
                {
                    g.DrawImage(imgData, 0, 0, w, h);
                }
                _result.Save(pathQR);
                _result.Dispose();
                return fileName;
            }
            return null;
        }

        public static string SaveQRImg(string path, string imgData)
        {
            Guid guid = Guid.NewGuid();
            string fileName = guid.ToString() + "_" + "QR.png";
            if (!string.IsNullOrEmpty(imgData))
            {
                string pathQR = HttpContext.Current.Server.MapPath(path) + "\\" + fileName;
                return fileName;
            }
            return null;
        }
        public static string SaveFileImage(string path, HttpPostedFileBase fileData)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss.fff") + "_" + fileData.FileName.Replace(" ", "_");

            if (fileData.FileName.Length > 63)
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss.fff") + "_" + fileData.FileName.Substring(fileData.FileName.Length - 63); ;
            }

            string pathImage = HttpContext.Current.Server.MapPath(IMG_TEMP_PATH) + "\\" + fileName;
            if (fileData != null && fileData.ContentLength > 0)
            {
                try
                {
                    //Save file to Temp folder
                    string temp_path = HttpContext.Current.Server.MapPath("~/" + IMG_TEMP_PATH);
                    if (!Directory.Exists(temp_path))
                    {
                        Directory.CreateDirectory(temp_path);
                    }

                    fileData.SaveAs(pathImage);

                    //ReSave file Image to Real folder
                    int maxImageWidth = 1600;
                    int maxImageHeight = 1024;
                    WebImage myIm = new WebImage(pathImage);
                    if (myIm.Width > maxImageWidth || myIm.Height > maxImageHeight)
                    {
                        myIm.Resize(maxImageWidth, maxImageHeight, true, true);
                        myIm.Crop(1, 1, 0, 0);
                    }

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                    {
                        Directory.CreateDirectory(path);
                    }

                    myIm.Save(Path.Combine(HttpContext.Current.Server.MapPath(path), fileName));

                    //string pathCV = HttpContext.Current.Server.MapPath(path) + "\\" + fileName;
                    //fileData.SaveAs(pathCV);
                    return fileName;
                }
                catch (Exception ex)
                {
                    return string.Empty;
                    throw ex;
                }
                finally
                {
                    // delete tempfile in server
                    if (System.IO.File.Exists(pathImage))
                    {
                        try
                        {
                            System.IO.File.Delete(pathImage);
                        }
                        catch
                        {
                        }
                    }
                }

            }

            return null;
        }
        public static DataTable ConvertListToDataTable<T>(List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = props[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[props.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}