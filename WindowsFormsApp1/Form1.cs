using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.Log;

/// <summary>
/// Press Switch A from Remote to Local...
/// 25:13.661 I P3A98 T0001 Form1 WndProc Remove deviceType=5
/// 25:13.662 I P3A98 T0001 Form1 WndProc Remove pDevice.dbcc_name="\\?\USB#VID_0557&PID_2405#5&2cb50b5c&0&4#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"
/// 25:15.806 I P3A98 T0001 Form1 WndProc Add    deviceType=5
/// 25:15.807 I P3A98 T0001 Form1 WndProc Add    pDevice.dbcc_name="\\?\USB#VID_0557&PID_2405#6&1a3ce924&0&2#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"
/// Press Switch B from Local to Remote...
/// 25:34.156 I P3A98 T0001 Form1 WndProc Remove deviceType=5
/// 25:34.157 I P3A98 T0001 Form1 WndProc Remove pDevice.dbcc_name="\\?\USB#VID_0557&PID_2405#6&783387f&0&2#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"
/// 25:36.877 I P3A98 T0001 Form1 WndProc Add    deviceType=5
/// 25:36.877 I P3A98 T0001 Form1 WndProc Add    pDevice.dbcc_name="\\?\USB#VID_0557&PID_2405#5&2cb50b5c&0&3#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"
/// </summary>

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static readonly string TAG = Log.TAG(typeof(Form1));

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Start(Handle);

            comboBoxDevices.SelectedIndex = 0;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Stop();
        }

        public enum ChangeType
        {
            Add,
            Remove
        }

        private IntPtr notificationHandle;

        public bool Start(IntPtr windowHandle)
        {
            Log.PrintLine(TAG, LogLevel.Information, $"Start(windowHandle={windowHandle})");
            if (notificationHandle != IntPtr.Zero)
            {
                return false;
            }

            DEV_BROADCAST_DEVICEINTERFACE dbi = new DEV_BROADCAST_DEVICEINTERFACE
            {
                dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE,
                dbcc_classguid = GUID_DEVINTERFACE_USB_DEVICE,
            };

            dbi.dbcc_size = Marshal.SizeOf(dbi);
            IntPtr buffer = Marshal.AllocHGlobal(dbi.dbcc_size);
            Marshal.StructureToPtr(dbi, buffer, true);

            notificationHandle = RegisterDeviceNotification(windowHandle, buffer, 0);

            Marshal.FreeHGlobal(buffer);

            return true;
        }

        public void Stop()
        {
            Log.PrintLine(TAG, LogLevel.Information, "Stop()");
            if (notificationHandle == IntPtr.Zero)
            {
                return;
            }
            UnregisterDeviceNotification(notificationHandle);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            //Log.PrintLine(TAG, LogLevel.Information, $"WndProc m={m}");
            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    {
                        var wParam = m.WParam.ToInt32();
                        //Log.PrintLine(TAG, LogLevel.Information, $"WndProc WM_DEVICECHANGE wParam=0x{wParam:X8}");

                        /*
                        var serialPorts = SerialPort.GetPortNames().OrderBy(name => name);
                        foreach (var serialPort in serialPorts)
                        {
                            Log.PrintLine(TAG, LogLevel.Information, $"WndProc WM_DEVICECHANGE serialPort={serialPort}");
                        }
                        */

                        switch (wParam)
                        {
                            /*
                            case DBT_DEVNODES_CHANGED:
                                {
                                    Log.PrintLine(TAG, LogLevel.Information, "WndProc DBT_DEVNODES_CHANGED");
                                    break;
                                }
                                */
                            case DBT_DEVICEARRIVAL:
                            case DBT_DEVICEREMOVECOMPLETE:
                                {
                                    ChangeType changeType;
                                    switch (wParam)
                                    {
                                        case DBT_DEVICEARRIVAL:
                                            changeType = ChangeType.Add;
                                            break;
                                        case DBT_DEVICEREMOVECOMPLETE:
                                            changeType = ChangeType.Remove;
                                            break;
                                        default:
                                            return;
                                    }
                                    var lParam = m.LParam;
                                    DEV_BROADCAST_HDR pHdr = (DEV_BROADCAST_HDR)Marshal.PtrToStructure(lParam, typeof(DEV_BROADCAST_HDR));
                                    var deviceType = pHdr.dbch_DeviceType;
                                    Log.PrintLine(TAG, LogLevel.Information, $"WndProc {changeType} deviceType={deviceType}");
                                    switch (deviceType)
                                    {
                                        case DBT_DEVTYP_DEVICEINTERFACE:
                                            {
                                                //Log.PrintLine(TAG, LogLevel.Information, $"WndProc {changeType} devType=DBT_DEVTYP_DEVICEINTERFACE");
                                                DEV_BROADCAST_DEVICEINTERFACE pDevice = (DEV_BROADCAST_DEVICEINTERFACE)Marshal.PtrToStructure(lParam, typeof(DEV_BROADCAST_DEVICEINTERFACE));
                                                var deviceName = pDevice.dbcc_name;
                                                Log.PrintLine(TAG, LogLevel.Information, $"WndProc {changeType} pDevice.dbcc_name={Utils.Quote(deviceName)}");
                                                deviceName = CleanUpDeviceInterfaceName(deviceName);
                                                /*
                                                if (DeviceNamesSubscribed.Contains(deviceName))
                                                {
                                                    OnUsbChange?.Invoke(this, new UsbChangeEventArgs(changeType, deviceName));
                                                }
                                                */
                                                break;
                                            }
                                        case DBT_DEVTYP_PORT:
                                            {
                                                //Log.PrintLine(TAG, LogLevel.Information, $"WndProc {changeType} devType=DBT_DEVTYP_PORT");
                                                DEV_BROADCAST_PORT pPort = (DEV_BROADCAST_PORT)Marshal.PtrToStructure(lParam, typeof(DEV_BROADCAST_PORT));
                                                var portName = pPort.dbcp_name;
                                                Log.PrintLine(TAG, LogLevel.Information, $"WndProc {changeType} pPort.dbcp_name={Utils.Quote(portName)}");
                                                /*
                                                if (SerialPortsSubscribed.Contains(portName))
                                                {
                                                    OnSerialPortChange?.Invoke(this, new SerialPortChangeEventArgs(changeType, portName));
                                                }
                                                */
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
        }

        private static string CleanUpDeviceInterfaceName(string deviceName)
        {
            // Remove null-terminated data from the string
            int pos = deviceName.IndexOf((char)0);
            if (pos != -1)
            {
                deviceName = deviceName.Substring(0, pos);
            }

            string temp;

            temp = @"\\?\USB#";
            if (deviceName.StartsWith(temp))
            {
                deviceName = @"USB\" + deviceName.Substring(temp.Length);
            }

            temp = "#" + GUID_DEVINTERFACE_USB_DEVICE.ToString("B");
            if (deviceName.EndsWith(temp))
            {
                deviceName = deviceName.Substring(0, deviceName.Length - temp.Length);
            }

            deviceName = deviceName.Replace("#", @"\");

            // \\?\USB#VID_046D&PID_085C#7F1799DF#{a5dcbf10-6530-11d2-901f-00c04fb951ed}
            // USB\VID_046D&PID_085C\7F1799DF

            return deviceName;
        }

        public static bool FindDevice(string deviceName)
        {
            if (!deviceName.StartsWith("\""))
            {
                deviceName = $"\"{deviceName}";
            }
            if (!deviceName.EndsWith("\""))
            {
                deviceName = $"{deviceName}\"";
            }

            ManagementObjectCollection usbDeviceAddressInfo = QueryMi(@"Select * from Win32_USBControllerDevice");

            foreach (var device in usbDeviceAddressInfo)
            {
                string curPnpAddress = (string)device.GetPropertyValue("Dependent");

                // split out the address portion of the data; note that this includes escaped backslashes and quotes
                curPnpAddress = curPnpAddress.Split(new String[] { "DeviceID=" }, 2, StringSplitOptions.None)[1];

                curPnpAddress = curPnpAddress.Replace("\\\\", "\\");

                //Log.PrintLine(TAG, LogLevel.Information, $"FindDevice curPnpAddress={Utils.Quote(curPnpAddress)}");

                if (curPnpAddress == deviceName)
                {
                    return true;
                }
            }

            return false;
        }

        public static ManagementObjectCollection QueryMi(string query)
        {
            ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection result = managementObjectSearcher.Get();
            managementObjectSearcher.Dispose();
            return result;
        }

        #region Native

        //
        // https://docs.microsoft.com/en-us/windows/desktop/devio/wm-devicechange
        //

        private const int WM_DEVICECHANGE = 0x0219;     // device change event      
        private const int DBT_DEVICEARRIVAL = 0x8000;     // system detected a new device        
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;     // "A device or piece of media has been removed."
        private const int DBT_DEVNODES_CHANGED = 0x0007;     // "A device has been added to or removed from the system."
        private const int DBT_DEVTYP_OEM = 0x00000000; // "OEM- or IHV-defined device type. This structure is a DEV_BROADCAST_OEM structure."
        private const int DBT_DEVTYP_VOLUME = 0x00000002; // "Logical volume. This structure is a DEV_BROADCAST_VOLUME structure."
        private const int DBT_DEVTYP_PORT = 0x00000003; // "Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure."
        private const int DBT_DEVTYP_DEVICEINTERFACE = 0x00000005; // "Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure."
        private const int DBT_DEVTYP_HANDLE = 0x00000006; // "File system handle. This structure is a DEV_BROADCAST_HANDLE structure."

        /// <summary>
        /// http://www.pinvoke.net/default.aspx/Structures.DEV_BROADCAST_HDR
        /// https://docs.microsoft.com/en-us/windows/desktop/api/dbt/ns-dbt-dev_broadcast_hdr
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct DEV_BROADCAST_HDR
        {
            public uint dbch_Size;
            public uint dbch_DeviceType;
            public uint dbch_Reserved;
        }

        /// <summary>
        /// http://www.pinvoke.net/default.aspx/Structures.DEV_BROADCAST_PORT
        /// https://docs.microsoft.com/en-us/windows/desktop/api/dbt/ns-dbt-dev_broadcast_port_w
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct DEV_BROADCAST_PORT
        {
            public int dbcp_size;
            public int dbcp_devicetype;
            public int dbcp_reserved;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string dbcp_name;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/dbt/ns-dbt-dev_broadcast_deviceinterface_w
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct DEV_BROADCAST_DEVICEINTERFACE
        {
            public int dbcc_size;
            public int dbcc_devicetype;
            public int dbcc_reserved;
            public Guid dbcc_classguid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string dbcc_name;

            public static readonly int Size = Marshal.SizeOf(typeof(DEV_BROADCAST_DEVICEINTERFACE));
        }

        //
        // https://docs.microsoft.com/en-us/windows-hardware/drivers/install/guid-devinterface-usb-device
        //
        private static readonly Guid GUID_DEVINTERFACE_USB_DEVICE = new Guid("a5dcbf10-6530-11d2-901f-00c04fb951ed"); // USB devices

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);

        [DllImport("user32.dll")]
        private static extern bool UnregisterDeviceNotification(IntPtr handle);

        #endregion Native

        private void buttonDevices_Click(object sender, EventArgs e)
        {
            comboBoxDevices.BeginUpdate();
            comboBoxDevices.Items.Clear();

            ManagementObjectCollection usbDeviceAddressInfo = QueryMi(@"Select * from Win32_USBControllerDevice");

            foreach (var device in usbDeviceAddressInfo)
            {
                string curPnpAddress = (string)device.GetPropertyValue("Dependent");

                // split out the address portion of the data; note that this includes escaped backslashes and quotes
                curPnpAddress = curPnpAddress.Split(new String[] { "DeviceID=" }, 2, StringSplitOptions.None)[1];

                curPnpAddress = curPnpAddress.Replace("\\\\", "\\");

                Log.PrintLine(TAG, LogLevel.Information, $"FindDevice curPnpAddress={Utils.Quote(curPnpAddress)}");

                comboBoxDevices.Items.Add(curPnpAddress);
            }

            comboBoxDevices.SelectedIndex = 0;

            comboBoxDevices.EndUpdate();
        }
    }
}
