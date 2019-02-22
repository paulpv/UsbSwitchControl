using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Log
    {
        public enum LogLevel
        {
            Fatal = 1,
            Error = 2,
            Warning = 4,
            Information = 8,
            Debug = 16,
            Verbose = 32,
        }

        private static string GetShortClassName(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                return "null";
            }
            return className.Substring(className.LastIndexOf('.') + 1);
        }

        private static string GetShortClassName(Object o)
        {
            Type t = (o == null) ? null : o.GetType();
            return GetShortClassName(t);
        }

        private static string GetShortClassName(Type c)
        {
            string className = (c == null) ? null : c.Name;
            return GetShortClassName(className);
        }

        public static string TAG(object o)
        {
            return TAG((o == null) ? null : //
                o.GetType() //
                );
        }

        public static string TAG(Type c)
        {
            return GetShortClassName(c);
        }

        public static void PrintLine(string tag, LogLevel level, string format)
        {
            PrintLine(tag, level, format, null as Exception);
        }

        public static void PrintLine(string tag, LogLevel level, string format, params object[] args)
        {
            PrintLine(tag, level, string.Format(format, args));
        }

        public static void PrintLine(string tag, LogLevel level, string message, Exception e)
        {
            DateTime dt = DateTime.Now;
#if SILVERLIGHT
            // NOTE: Process ID is not available in Silverlight
            int pid = -1;
#else
            int pid = Process.GetCurrentProcess().Id;
#endif
            int tid = Thread.CurrentThread.ManagedThreadId;

            StringBuilder sb = new StringBuilder()
                        .Append(dt.ToString("mm:ss.fff")) //
                        .Append(' ').Append(level.ToString()[0]) //
                        .Append(" P").Append(pid.ToString("X4")) //
                        .Append(" T").Append(tid.ToString("X4")) //
                        .Append(' ').Append(tag) //
                        .Append(' ').Append(message);
            if (e != null)
            {
                sb.Append(": exception=").Append(e.ToString()).Append('\n').Append(e.Message);
            }

            Debug.WriteLine(sb.ToString());
        }
    }
}
