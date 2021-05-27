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

        internal static bool TryGetCSGOWindow() => GetActiveWindowTitle()?.Equals(Information.PROCESS_FULLNAME) != null;

        internal static bool TryGetActiveWindowByName(string name) => GetActiveWindowTitle()?.Equals(name) != null;
    }
}
