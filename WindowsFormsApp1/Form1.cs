using HidLibrary;
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
using static WindowsFormsApp1.HidLibraryExt;
using static WindowsFormsApp1.Log;

/// <summary>
/// 
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
/// 
/// DevicePath "\\\\?\\hid#vid_0557&pid_2405&mi_01#7&2ac3b27&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}"
///                   "hid\vid_0557&pid_2405&mi_01\7&2ac3b27&0&0000"

/// DevicePath "\\\\?\\hid#vid_0557&pid_2405&mi_01#7&19802bc4&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}"
///                   "hid\vid_0557&pid_2405&mi_01\7&19802bc4&0&0000"

///
/// Refresh: Switch A & Switch B both on Local...
/// 56:21.575 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405\6&1A3CE924&0&2" *** Switch A Local
/// 56:21.577 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_00\7&3511B330&0&0000"
/// 56:21.579 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_00\8&2ED5FABC&0&0000"
/// 56:21.580 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_01\7&3511B330&0&0001"
/// 56:21.582 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_01\8&AFEBCFA&0&0000"
/// 56:21.584 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405\6&783387F&0&2" *** Switch B Local
/// 56:21.586 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_00\7&ED64526&0&0000"
/// 56:21.588 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_00\8&1609D4C3&0&0000"
/// 56:21.590 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_01\7&ED64526&0&0001"
/// 56:21.592 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_01\8&2D9B885&0&0000"
/// Switch B Local to Remote...
/// 56:29.490 I P3A30 T0001 Form1 WndProc Remove deviceType=5
/// 56:29.493 I P3A30 T0001 Form1 WndProc Remove pDevice.dbcc_name="\\?\USB#VID_0557&PID_2405#6&783387f&0&2#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"
/// 56:32.206 I P3A30 T0001 Form1 WndProc Add    deviceType=5
/// 56:32.208 I P3A30 T0001 Form1 WndProc Add    pDevice.dbcc_name="\\?\USB#VID_0557&PID_2405#5&2cb50b5c&0&3#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"
/// Refresh: ....
/// 56:44.278 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405\6&1A3CE924&0&2" *** Switch A Local
/// 56:44.280 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_00\7&3511B330&0&0000"
/// 56:44.284 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_00\8&2ED5FABC&0&0000"
/// 56:44.285 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_01\7&3511B330&0&0001"
/// 56:44.287 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_01\8&AFEBCFA&0&0000"
/// 56:44.288 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405\5&2CB50B5C&0&3" *** Switch B Remote
/// 56:44.290 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_00\6&BF6FE7E&0&0000"
/// 56:44.292 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_00\7&6500F86&0&0000"
/// 56:44.294 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_01\6&BF6FE7E&0&0001"
/// 56:44.295 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_01\7&19802BC4&0&0000"
/// Switch A Local to Remote...
/// 56:51.423 I P3A30 T0001 Form1 WndProc Remove deviceType=5
/// 56:51.432 I P3A30 T0001 Form1 WndProc Remove pDevice.dbcc_name="\\?\USB#VID_0557&PID_2405#6&1a3ce924&0&2#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"
/// 56:54.147 I P3A30 T0001 Form1 WndProc Add    deviceType=5
/// 56:54.149 I P3A30 T0001 Form1 WndProc Add    pDevice.dbcc_name="\\?\USB#VID_0557&PID_2405#5&2cb50b5c&0&4#{a5dcbf10-6530-11d2-901f-00c04fb951ed}"
/// Refresh: ...
/// 57:06.112 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405\5&2CB50B5C&0&3" *** Switch A Remote
/// 57:06.113 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_00\6&BF6FE7E&0&0000"
/// 57:06.116 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_00\7&6500F86&0&0000"
/// 57:06.118 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_01\6&BF6FE7E&0&0001"
/// 57:06.119 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_01\7&19802BC4&0&0000"
/// 57:06.121 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405\5&2CB50B5C&0&4" *** Switch B Remote
/// 57:06.123 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_00\6&1EB0AF23&0&0000"
/// 57:06.124 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_00\7&212B029B&0&0000"
/// 57:06.126 I P3A30 T0001 Form1 FindDevice curPnpAddress = "USB\VID_0557&PID_2405&MI_01\6&1EB0AF23&0&0001"
/// 57:06.128 I P3A30 T0001 Form1 FindDevice curPnpAddress = "HID\VID_0557&PID_2405&MI_01\7&2AC3B27&0&0000"
/// 
/// </summary>

namespace WindowsFormsApp1
{
    /// <summary>
    /// Interesting: https://github.com/ryancdotorg/gub211
    /// </summary>
    public partial class Form1 : Form
    {
        private static readonly string TAG = Log.TAG(typeof(Form1));

        private const int SWITCH_GUS231_VID = 0x0557; // ATEN
        private const int SWITCH_GUS231_PID = 0x2405; // 2 port switch
        private const string SWITCH_GUS231_VERSION = "0.0.49.2"; // 0x00003102 (12546)

        private readonly HidDeviceId SWITCH_HID_DEVICE_ID = new HidDeviceId(SWITCH_GUS231_VID, SWITCH_GUS231_PID, new Version(SWITCH_GUS231_VERSION));

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DevicesRefresh();
            Start(Handle);
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
                                                /*
                                                deviceName = CleanUpDeviceInterfaceName(deviceName);
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
                //Log.PrintLine(TAG, LogLevel.Information, $"FindDevice curPnpAddress={Utils.Quote(curPnpAddress)}");

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

        private void buttonDevicesRefresh_Click(object sender, EventArgs e)
        {
            DevicesRefresh();
        }

        private void DevicesRefresh()
        {
            DevicesRefresh(
                textBoxDevicesFilter.Text,
                comboBoxDevicesPrimaryLocal,
                comboBoxDevicesPrimaryRemote,
                comboBoxDevicesSecondaryLocal,
                comboBoxDevicesSecondaryRemote);
        }

        private void DevicesRefresh(string devicesFilter, params ComboBox[] comboBoxes)
        {
            devicesFilter = devicesFilter.ToLower();

            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.BeginUpdate();
                comboBox.Items.Clear();
            }

            var devices = HidLibraryExt.Enumerate(SWITCH_HID_DEVICE_ID);

            foreach (var device in devices)
            {
                var devicePath = device.DevicePath;
                //Log.PrintLine(TAG, LogLevel.Information, $"DevicesRefresh devicePath={Utils.Quote(devicePath)}");

                if (!devicePath.ToLower().Contains(devicesFilter))
                {
                    continue;
                }

                Log.PrintLine(TAG, LogLevel.Information, $"DevicesRefresh devicePath={Utils.Quote(devicePath)}");
                foreach (ComboBox comboBox in comboBoxes)
                {
                    comboBox.Items.Add(devicePath);
                    switch (comboBox.Name)
                    {
                        case "comboBoxDevicesPrimaryLocal":
                            if (devicePath == SelectedDevicePrimaryLocal)
                            {
                                comboBox.SelectedItem = devicePath;
                            }
                            break;
                        case "comboBoxDevicesPrimaryRemote":
                            if (devicePath == SelectedDevicePrimaryRemote)
                            {
                                comboBox.SelectedItem = devicePath;
                            }
                            break;
                        case "comboBoxDevicesSecondaryLocal":
                            if (devicePath == SelectedDeviceSecondaryLocal)
                            {
                                comboBox.SelectedItem = devicePath;
                            }
                            break;
                        case "comboBoxDevicesSecondaryRemote":
                            if (devicePath == SelectedDeviceSecondaryRemote)
                            {
                                comboBox.SelectedItem = devicePath;
                            }
                            break;
                    }
                }
            }

            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.EndUpdate();
            }
        }

        private void comboBoxDevicesAny_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null)
            {
                return;
            }

            string selectedValue = comboBox.SelectedItem as string;

            switch(comboBox.Name)
            {
                case "comboBoxDevicesPrimaryLocal":
                    SelectedDevicePrimaryLocal = selectedValue;
                    break;
                case "comboBoxDevicesPrimaryRemote":
                    SelectedDevicePrimaryRemote = selectedValue;
                    break;
                case "comboBoxDevicesSecondaryLocal":
                    SelectedDeviceSecondaryLocal = selectedValue;
                    break;
                case "comboBoxDevicesSecondaryRemote":
                    SelectedDeviceSecondaryRemote = selectedValue;
                    break;
            }
        }

        private string SelectedDevicePrimaryLocal
        {
            get
            {
                var deviceId = Properties.Settings.Default.SelectedDevicePrimaryLocal;
                //Log.PrintLine(TAG, LogLevel.Information, $"SelectedDevicePrimaryLocal get deviceId={Utils.Quote(deviceId)}");
                return deviceId;
            }
            set
            {
                var deviceId = value;
                //Log.PrintLine(TAG, LogLevel.Information, $"SelectedDevicePrimaryLocal set deviceId={Utils.Quote(deviceId)}");
                var settings = Properties.Settings.Default;
                settings.SelectedDevicePrimaryLocal = deviceId;
                settings.Save();
            }
        }

        private string SelectedDevicePrimaryRemote
        {
            get
            {
                var deviceId = Properties.Settings.Default.SelectedDevicePrimaryRemote;
                //Log.PrintLine(TAG, LogLevel.Information, $"SelectedDevicePrimaryRemote get deviceId={Utils.Quote(deviceId)}");
                return deviceId;
            }
            set
            {
                var deviceId = value;
                //Log.PrintLine(TAG, LogLevel.Information, $"SelectedDevicePrimaryRemote set deviceId={Utils.Quote(deviceId)}");
                var settings = Properties.Settings.Default;
                settings.SelectedDevicePrimaryRemote = deviceId;
                settings.Save();
            }
        }

        private string SelectedDeviceSecondaryLocal
        {
            get
            {
                var deviceId = Properties.Settings.Default.SelectedDeviceSecondaryLocal;
                //Log.PrintLine(TAG, LogLevel.Information, $"SelectedDeviceSecondaryLocal get deviceId={Utils.Quote(deviceId)}");
                return deviceId;
            }
            set
            {
                var deviceId = value;
                //Log.PrintLine(TAG, LogLevel.Information, $"SelectedDeviceSecondaryLocal set deviceId={Utils.Quote(deviceId)}");
                var settings = Properties.Settings.Default;
                settings.SelectedDeviceSecondaryLocal = deviceId;
                settings.Save();
            }
        }

        private string SelectedDeviceSecondaryRemote
        {
            get
            {
                var deviceId = Properties.Settings.Default.SelectedDeviceSecondaryRemote;
                //Log.PrintLine(TAG, LogLevel.Information, $"SelectedDeviceSecondaryRemote get deviceId={Utils.Quote(deviceId)}");
                return deviceId;
            }
            set
            {
                var deviceId = value;
                //Log.PrintLine(TAG, LogLevel.Information, $"SelectedDeviceSecondaryRemote set deviceId={Utils.Quote(deviceId)}");
                var settings = Properties.Settings.Default;
                settings.SelectedDeviceSecondaryRemote = deviceId;
                settings.Save();
            }
        }

        private void buttonDevicesReset_Click(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;
            settings.SelectedDevicePrimaryLocal = null;
            settings.SelectedDevicePrimaryRemote = null;
            settings.SelectedDeviceSecondaryLocal = null;
            settings.SelectedDeviceSecondaryRemote = null;
            settings.Save();
            DevicesRefresh();
        }

        private void buttonDeviceAnySwitch_Click(object sender, EventArgs e)
        {
            //var devicePath = comboBoxDevicesPrimaryRemote.SelectedItem as string;
            //var hidDevice = new HidDevice(deviceName);
            //var device = HidDevices.GetDevice(devicePath);
            var devices = HidLibraryExt.Enumerate(SWITCH_HID_DEVICE_ID);
            // DevicePath "\\\\?\\hid#vid_0557&pid_2405&mi_01#7&2ac3b27&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}"
            // DevicePath "\\\\?\\hid#vid_0557&pid_2405&mi_01#7&19802bc4&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}"
            foreach (var device in devices)
            {
                SendCmdToSwitch(device, Gub231SwitchCommands.Switch);
            }
        }

        private enum Gub231SwitchCommands
        {
            Cancel = 0x10,   // CMD_CANCEL
            Switch = 0x11,   // CMD_SWITCH
            Lock = 0x21,     // CMD_LOCK
            Unlock = 0x20,   // CMD_UNLOCK
            KeepAlive = 0x40 // CMD_KEEP_ALIVE ?
        }

        /// <summary>
        /// Reverse engineering GUB231 USwitch.exe
        /// Look for method "SendCmdToSwitch"
        /// Using x32dbg
        /// Add plugin "API Break" https://github.com/0ffffffffh/Api-Break-for-x64dbg/releases
        /// Set breakpoint in:
        ///     kernel32.dll WriteFile
        ///     kernel32.dll OutputDebugStringW
        /// 00AD46E0 | 55                       | push ebp                                          | possibly "GetPortInformaiton" (sic)
        /// 00AD5180 | 55                       | push ebp                                          | possibly enumerating GUB231 devices...
        /// 00AD5777 | 8B0D 10B0AE00            | mov ecx,dword ptr ds:[AEB010]                     | 00AEB010:&L"Ver %d.%d.%d%d%d"
        /// 00AD5B70 | 55                       | push ebp                                          | SendCmdToSwitch...
        /// ...
        /// 00AD623C | 8B1D 1842AE00            | mov ebx,dword ptr ds:[&Shell_NotifyIconW>]        |
        /// 00AD62EE | FF15 7C42AE00            | call dword ptr ds:[&UnregisterDeviceNotification  |
        /// 00AD6FFA | 68 9872AE00              | push uswitch.AE7298                               | AE7298:L"SendCmdToSwitch(CMD_CANCEL) 0x10.case CMD_CANCEL"
        /// 00AD7043 | 68 3072AE00              | push uswitch.AE7230                               | AE7230:L"SendCmdToSwitch(CMD_SWITCH) 0x11.case CMD_SWITCH"
        /// 00AD7069 | 68 D071AE00              | push uswitch.AE71D0                               | AE71D0:L"SendCmdToSwitch(CMD_LOCK) 0x21.case CMD_LOCK"
        /// 00AD70A6 | 68 6871AE00              | push uswitch.AE7168                               | AE7168:L"SendCmdToSwitch(CMD_UNLOCK) 0x20.case CMD_UNLOCK"
        /// 00AD7812 | 68 2078AE00              | push uswitch.AE7820                               | AE7820:L"IDM_NEW_SWITCH   SendCmdToSwitch(index, CMD_SWITCH)"
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cmd"></param>
        private void SendCmdToSwitch(HidDevice device, Gub231SwitchCommands cmd)
        {
            Log.PrintLine(TAG, LogLevel.Information, $"SendCmdToSwitch device.DevicePath={Utils.Quote(device.DevicePath)} cmd={cmd}");
            device.OpenDevice();
            device.WriteReport(new HidReport(2, new HidDeviceData(new byte[] { 0x02, (byte)cmd }, HidDeviceData.ReadStatus.Success)));
            device.CloseDevice();
        }
    }
}
