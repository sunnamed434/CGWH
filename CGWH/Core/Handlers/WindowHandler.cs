using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CGWH.Core.Handlers
{
    internal class WindowHandler
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();


        [DllImport("user32.dll")]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);



        internal static string GetActiveWindowTitle()
        {
            byte value = byte.MaxValue;

            StringBuilder builder = new StringBuilder(value);

            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, builder, value) > 0) return builder.ToString();

            return null;
        }

        internal static bool TryGetCSGOWindow()
        {
            string result = GetActiveWindowTitle();

            if (result != null && result.Equals(Information.PROCESS_FULLNAME)) return true;

            return false;
        }

        internal static bool TryGetActiveWindowByName(string name)
        {
            string result = string.Empty;

            if ((result = GetActiveWindowTitle()) != null && result.Equals(name)) return true;

            return false;
        }
    }
}
