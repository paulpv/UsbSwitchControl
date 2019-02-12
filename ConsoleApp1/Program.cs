using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using HidLibrary;

namespace ConsoleApp1
{
    class Program
    {
        private const int VendorId = 0x0557;
        private const int ProductId = 0x2405;
        private static HidDevice _device;
        private static bool _attached;

        static void Main(string[] args)
        {
            _device = HidDevices.Enumerate(VendorId, ProductId).FirstOrDefault();
            if (_device == null)
            {
                Console.WriteLine("Could not find GUB201 contoller; Press any key to exit...");
                Console.ReadKey();
                return;
            }

            _device.Inserted += DeviceAttachedHandler;
            _device.Removed += DeviceRemovedHandler;

            _device.MonitorDeviceEvents = true;

            Console.WriteLine("GUB201 contoller attached; Press any key to switch...");
            Console.ReadKey();
            Switch();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void DeviceAttachedHandler()
        {
            _attached = true;
            //Console.WriteLine("GUB201 contoller attached.");
        }

        private static void DeviceRemovedHandler()
        {
            _attached = false;
            Console.WriteLine("GUB201 contoller detached.");
        }

        private static void Switch()
        {
            if (_device == null || !_attached)
            {
                return;
            }

            _device.OpenDevice();
            _device.WriteReport(new HidReport(2, new HidDeviceData(new byte[] { 0x02, 0x23 }, HidDeviceData.ReadStatus.Success)));
            _device.CloseDevice();
        }
    }
}
