using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static WindowsFormsApp1.Log;

namespace WindowsFormsApp1
{
    public class Utils
    {
        private static readonly string TAG = Log.TAG(typeof(Utils));

        public static string MachineName
        {
            get
            {
                return Environment.MachineName;
                // For debugging purposes only:
                //return "NIGHT55";
                //return "KNIGHT55";
            }
        }

        public static string Quote(string value)
        {
            return value == null ? "null" : $"\"{value}\"";
        }

        public static string ToString(IEnumerable<string> values)
        {
            var sb = new StringBuilder();

            var it = values.GetEnumerator();
            while (it.MoveNext())
            {
                var value = it.Current;
                sb.Append(Quote(value));
                sb.Append(",");
                /*
                if (it.MoveNext())
                {
                    sb.Append(",");
                }
                else
                {
                    break;
                }
                */
            }
            return sb.ToString();

        }

        public static void WindowPositionSet(Form form, bool isMaximized, Point windowPosition)
        {
            if (isMaximized)
            {
                form.WindowState = FormWindowState.Maximized;
            }
            else if (Screen.AllScreens.Any(screen => screen.WorkingArea.Contains(windowPosition)))
            {
                form.StartPosition = FormStartPosition.Manual;
                form.DesktopLocation = windowPosition;
                form.WindowState = FormWindowState.Normal;
            }
        }

        public static void SetWindowPosition(Form form, bool isMaximized, Rectangle windowPosition)
        {
            if (isMaximized)
            {
                form.WindowState = FormWindowState.Maximized;
            }
            else if (Screen.AllScreens.Any(screen => screen.WorkingArea.IntersectsWith(windowPosition)))
            {
                form.StartPosition = FormStartPosition.Manual;
                form.DesktopBounds = windowPosition;
                form.WindowState = FormWindowState.Normal;
            }
        }

        public static Process[] GetProcessesByName(string processName)
        {
            return Process.GetProcessesByName(processName);
        }

        public static bool IsProcessRunning(string processName)
        {
            return GetProcessesByName(processName).Length > 0;
        }

        public static Process ProcessStart(string processPath, string arguments = null, string stopProcessName = null)
        {
            if (!string.IsNullOrEmpty(stopProcessName))
            {
                ProcessStop(stopProcessName);
            }

            processPath = Environment.ExpandEnvironmentVariables(processPath);

            var workingDirectory = new FileInfo(processPath).Directory.FullName;
            workingDirectory = new FileInfo(processPath).DirectoryName;
            workingDirectory = Path.GetDirectoryName(processPath);

            var processStartInfo = new ProcessStartInfo(processPath, arguments)
            {
                WorkingDirectory = workingDirectory
            };

            return Process.Start(processStartInfo);
        }

        public static string ProcessStop(string processName)
        {
            if (string.IsNullOrEmpty(processName))
            {
                return null;
            }

            string processFilePath = null;
            foreach (var process in GetProcessesByName(processName))
            {
                processFilePath = ProcessStop(process);
            }
            return processFilePath;
        }

        public static string ProcessStop(Process process)
        {
            if (process == null)
            {
                return null;
            }

            string processFilePath = process.MainModule.FileName;

            try
            {
                if (!process.HasExited)
                {
                    if (process.MainWindowHandle == IntPtr.Zero || !process.CloseMainWindow())
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception e)
            {
                Log.PrintLine(TAG, LogLevel.Information, $"ProcessStop Exception", e);
            }

            return processFilePath;
        }

        /*
        private const string chromeExecutablePath = @"%ProgramFiles(x86)%\Google\Chrome\Application\chrome.exe";

        public Process StartChrome(string url, Rectangle rect, string profileName)
        {
            url = HttpUtility.UrlEncode(url);
            var arguments = "--app=\"data:text/html,<html><body><script>";
            if (rect != Rectangle.Empty)
            {
                arguments += $"window.moveTo({rect.X}, {rect.Y});window.resizeTo({rect.Width}, {rect.Height});";
            }
            arguments += $"window.location='{url}';</script></body></html >\"";
            if (!string.IsNullOrEmpty(profileName))
            {
                arguments += $" --profile-directory=\"{profileName}\"";
            }
            return ProcessStart(chromeExecutablePath, arguments);
        }
        */
    }
}