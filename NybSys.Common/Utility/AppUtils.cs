using NybSys.Common.Extension;
using NybSys.Common.Utility;
using System.Text;
using UAParser;

namespace NybSys.Common
{
    public static class AppUtils
    {
        public static VmBrowserInfo GetBrowserInfo(ClientInfo clientInfo)
        {
            if(clientInfo != null)
            {
                VmBrowserInfo browserInfo = new VmBrowserInfo()
                {
                    BrowserName = GetBrowserInfo(clientInfo.UserAgent),
                    DeviceName = GetDeviceInfo(clientInfo.Device),
                    OSName = GetOperatingSystemInfo(clientInfo.OS)
                };

                return browserInfo;
            }

            return new VmBrowserInfo();
        }

        private static string GetDeviceInfo(Device device)
        {
            StringBuilder sb = new StringBuilder();

            if(!string.IsNullOrEmpty(device.Brand))
            {
                sb.Append(device.Brand + " ");
            }

            if (!string.IsNullOrEmpty(device.Family))
            {
                if(device.Family.EqualsWithLower("Other"))
                {
                    sb.Append("Unknown ");
                }
                else
                {
                    sb.Append(device.Family + " ");
                }
            }

            if (!string.IsNullOrEmpty(device.Model))
            {
                sb.Append(device.Model);
            }

            return sb.ToString();
        }

        private static string GetBrowserInfo(UserAgent userAgent)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(userAgent.Family))
            {
                sb.Append(userAgent.Family + " ");
            }


            if (!string.IsNullOrEmpty(userAgent.Major))
            {
                sb.Append(userAgent.Major);
            }

            return sb.ToString();
        }

        private static string GetOperatingSystemInfo(OS os)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(os.Family))
            {
                sb.Append(os.Family + " ");
            }


            if (!string.IsNullOrEmpty(os.Major))
            {
                sb.Append(os.Major);
            }

            return sb.ToString();
        }
    }
}
