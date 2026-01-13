using System.Management;
using ws_scanner.Application.Interfaces;

namespace ws_scanner.Infrastructure.Device
{
    public class DeviceService : IDeviceService
    {
        public bool IsWebcamConnected()
        {
            return GetCameraCount() > 0;
        }

        public int GetCameraCount()
        {
            return GetCameraNames().Count;
        }

        public List<string> GetCameraNames()
        {
            var cameras = new List<string>();

            try
            {
                using var searcher = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_PnPEntity"
                );

                foreach (ManagementObject device in searcher.Get())
                {
                    var name = device["Name"]?.ToString() ?? "";
                    var pnpClass = device["PNPClass"]?.ToString()?.ToLower() ?? "";

                    if (pnpClass == "camera" || pnpClass == "image")
                    {
                        cameras.Add(name);
                    }
                }
            }
            catch
            {
                // ignore → return empty list
            }

            return cameras;
        }
    }
}
