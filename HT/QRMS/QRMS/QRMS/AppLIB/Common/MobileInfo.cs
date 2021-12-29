using QRMS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QRMS.AppLIB.Common
{
    public class MobileInfo
    {
        //Lấy địa chỉ IP của thiết bị
        public static string GetIP()
        {
            try
            {
                var test = System.Net.Sockets.AddressFamily.InterNetwork;
                var ipAddress = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault();
                if (ipAddress != null)
                {
                    return ipAddress.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
                return null;
            }
        }

        //lấy thông tin thiết bị
        public static string GetDeviceInfo()
        {
            try
            {
                var deviceName = DeviceInfo.Name;
                var deviceType = DeviceInfo.DeviceType;
                var Idiom = DeviceInfo.Idiom;
                var Manufacturer = DeviceInfo.Manufacturer;
                var Model = DeviceInfo.Model;
                var Platform = DeviceInfo.Platform;
                var Version = DeviceInfo.Version;
                var VersionString = DeviceInfo.VersionString;

                return deviceName;
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().Log(ex.ToString());
                return null;
            }
        }
    }
}
