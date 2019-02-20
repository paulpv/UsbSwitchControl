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

        // GUB201: Product Version 0.1
        //   VendorId   0x00000557
        //   ProductId  0x00002405
        //   Version    0x00000001 (1) == 0.1
        // GUB231: Product Version 49.2
        //   VendorId   0x00000557
        //   ProductId  0x00002405
        //   Version    0x00003102 (12546) == 49.2

        private string DeviceModel;

        private static string _deviceModel;
        private static HidDevice _device1;
        private static HidDevice _device2;
        private static bool _attached;

        static void Prompt(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
        static void Exit(string message)
        {
            Prompt(message);
            Environment.Exit(1);
        }

        static void Main(string[] args)
        {
            var devices = HidDevices.Enumerate(VendorId, ProductId);
            if (devices.Count() == 0)
            {
                Exit("Could not find USB A/B switch contoller; Press any key to exit...");
                return;
            }

            foreach(var device in devices)
            {
                switch (device.Attributes.Version)
                {
                    case 0x00003102:
                        if (_deviceModel == null)
                        {
                            _deviceModel = "GUB231";
                        }
                        if (_device1 == null)
                        {
                            _device1 = device;
                        }
                        else if (_device2 == null)
                        {
                            _device2 = device;
                        }
                        break;
                    case 0x00000001:
                        // GUB201
                        break;
                }
            }

            if (_device1 == null)
            {
                Exit("Could not find USB A/B switch contoller; Press any key to exit...");
                return;
            }

            _device1.Inserted += DeviceAttachedHandler;
            _device1.Removed += DeviceRemovedHandler;
            _device1.MonitorDeviceEvents = true;

            Prompt($"{_deviceModel} contoller attached; Press any key to switch...");

            Switch();

            Exit("Press any key to exit...");
        }

        private static void DeviceAttachedHandler()
        {
            _attached = true;
            //Console.WriteLine($"{_deviceModel} contoller attached.");
        }

        private static void DeviceRemovedHandler()
        {
            _attached = false;
            Console.WriteLine($"{_deviceModel} contoller detached.");
        }

        private static void Switch()
        {
            if (_device1 == null || !_attached)
            {
                return;
            }

            _device1.OpenDevice();
            //_device.WriteReport(new HidReport(2, new HidDeviceData(new byte[] { 0x02, 0x23 }, HidDeviceData.ReadStatus.Success))); // GUB201
            _device1.WriteReport(new HidReport(2, new HidDeviceData(new byte[] { 0x02, 0x11 }, HidDeviceData.ReadStatus.Success))); // GUB231
            _device1.CloseDevice();
        }
    }
}
